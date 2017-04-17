// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DotNetty.Transport.Channels
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using DotNetty.Buffers;
    using DotNetty.Common.Utilities;

    public interface IChannel : IAttributeMap, IComparable<IChannel>
    {
        IChannelId Id { get; }

        IByteBufferAllocator Allocator { get; }

        IEventLoop EventLoop { get; }

        IChannel Parent { get; }

        bool Open { get; }

        bool Active { get; }

        bool Registered { get; }

        /// <summary>
        ///     Return the <see cref="ChannelMetadata" /> of the <see cref="IChannel" /> which describe the nature of the
        ///     <see cref="IChannel" />.
        /// </summary>
        ChannelMetadata Metadata { get; }

        EndPoint LocalAddress { get; }

        EndPoint RemoteAddress { get; }

        bool IsWritable { get; }

        IChannelUnsafe Unsafe { get; }
        /// <summary>
        /// 管道
        /// </summary>
        IChannelPipeline Pipeline { get; }

        /// <summary>
        /// 配置
        /// </summary>
        IChannelConfiguration Configuration { get; }

        /// <summary>
        /// 关闭完成
        /// </summary>
        Task CloseCompletion { get; }

        /// <summary>
        /// 注销异步
        /// </summary>
        /// <returns></returns>
        Task DeregisterAsync();

        /// <summary>
        /// 绑定异步
        /// </summary>
        /// <param name="localAddress">本地地址</param>
        /// <returns></returns>
        Task BindAsync(EndPoint localAddress);

        /// <summary>
        /// 异步连接
        /// </summary>
        /// <param name="remoteAddress"></param>
        /// <returns></returns>
        Task ConnectAsync(EndPoint remoteAddress);

        /// <summary>
        /// 异步连接
        /// </summary>
        /// <param name="remoteAddress"></param>
        /// <param name="localAddress"></param>
        /// <returns></returns>
        Task ConnectAsync(EndPoint remoteAddress, EndPoint localAddress);

        Task DisconnectAsync();

        Task CloseAsync();

        // todo: make these available through separate interface to hide them from public API on channel

        IChannel Read();

        Task WriteAsync(object message);

        IChannel Flush();

        Task WriteAndFlushAsync(object message);
    }
}