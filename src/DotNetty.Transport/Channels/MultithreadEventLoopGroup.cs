// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DotNetty.Transport.Channels
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using DotNetty.Common.Concurrency;

    /// <summary>
    /// <see cref="IEventLoopGroup"/> backed by a set of <see cref="SingleThreadEventLoop"/> instances.
    /// </summary>
    public sealed class MultithreadEventLoopGroup : IEventLoopGroup
    {
        static readonly int DefaultEventLoopThreadCount = Environment.ProcessorCount * 2;
        static readonly Func<IEventLoopGroup, IEventLoop> DefaultEventLoopFactory = group => new SingleThreadEventLoop(group);

        readonly IEventLoop[] eventLoops;
        int requestId;

        /// <summary>多线程事件循环组,创建一个新实例 <see cref="MultithreadEventLoopGroup"/>.</summary>
        public MultithreadEventLoopGroup()
            : this(DefaultEventLoopFactory, DefaultEventLoopThreadCount)
        {
        }

        /// <summary>多线程事件循环组,创建一个新实例 <see cref="MultithreadEventLoopGroup"/>.</summary>
        public MultithreadEventLoopGroup(int eventLoopCount)
            : this(DefaultEventLoopFactory, eventLoopCount)
        {
        }

        /// <summary>多线程事件循环组,创建一个新实例<see cref="MultithreadEventLoopGroup"/>.</summary>
        public MultithreadEventLoopGroup(Func<IEventLoopGroup, IEventLoop> eventLoopFactory)
            : this(eventLoopFactory, DefaultEventLoopThreadCount)
        {
        }


        /// <summary>创建一个新实例 <see cref="MultithreadEventLoopGroup"/>.</summary>
        /// <param name="eventLoopFactory">事件循环工厂</param>
        /// <param name="eventLoopCount">事件循环的计数</param>
        public MultithreadEventLoopGroup(Func<IEventLoopGroup, IEventLoop> eventLoopFactory, int eventLoopCount)
        {
            this.eventLoops = new IEventLoop[eventLoopCount];
            var terminationTasks = new Task[eventLoopCount];
            for (int i = 0; i < eventLoopCount; i++)
            {
                IEventLoop eventLoop;
                bool success = false;
                try
                {
                    eventLoop = eventLoopFactory(this);
                    success = true;
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("failed to create a child event loop.", ex);
                }
                finally
                {
                    if (!success)
                    {
                        Task.WhenAll(this.eventLoops
                                .Take(i)
                                .Select(loop => loop.ShutdownGracefullyAsync()))
                            .Wait();
                    }
                }

                this.eventLoops[i] = eventLoop;
                terminationTasks[i] = eventLoop.TerminationCompletion;
            }
            this.TerminationCompletion = Task.WhenAll(terminationTasks);
        }

        /// <inheritdoc />
        public Task TerminationCompletion { get; }

        /// <inheritdoc />
        public IEventLoop GetNext()
        {
            int id = Interlocked.Increment(ref this.requestId);
            return this.eventLoops[Math.Abs(id % this.eventLoops.Length)];
        }

        /// <inheritdoc />
        IEventExecutor IEventExecutorGroup.GetNext() => this.GetNext();

        /// <inheritdoc />
        public Task ShutdownGracefullyAsync()
        {
            foreach (IEventLoop eventLoop in this.eventLoops)
            {
                eventLoop.ShutdownGracefullyAsync();
            }
            return this.TerminationCompletion;
        }

        /// <inheritdoc />
        public Task ShutdownGracefullyAsync(TimeSpan quietPeriod, TimeSpan timeout)
        {
            foreach (IEventLoop eventLoop in this.eventLoops)
            {
                eventLoop.ShutdownGracefullyAsync(quietPeriod, timeout);
            }
            return this.TerminationCompletion;
        }
    }
}