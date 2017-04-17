// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DotNetty.Transport.Channels
{
    using System.Threading.Tasks;
    using DotNetty.Common.Concurrency;

    /// <summary>
    /// <see cref="IEventExecutor"/> 专门处理分配的 I/O 操作 I/O  <see cref="IChannel"/>s.
    /// </summary>
    public interface IEventLoop : IEventExecutor
    {
        /// <summary>
        /// Parent <see cref="IEventLoopGroup"/>.
        /// </summary>
        new IEventLoopGroup Parent { get; }

        /// <summary>
        /// Registers provided <see cref="IChannel"/> with this <see cref="IEventLoop"/>.
        /// </summary>
        /// <param name="channel"><see cref="IChannel"/> to register.</param>
        /// <returns><see cref="Task"/> for completion of registration.</returns>
        Task RegisterAsync(IChannel channel);
    }
}