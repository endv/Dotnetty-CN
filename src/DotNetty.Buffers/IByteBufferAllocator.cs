// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DotNetty.Buffers
{
    /// <summary>
    /// 字节缓冲区分配接口 用Helios内部反应I/O分配  <see cref="IByteBuffer" />  实例线程安全的接口
    /// Thread-safe interface for allocating <see cref="IByteBuffer" /> instances for use inside Helios reactive I/O
    /// </summary>
    public interface IByteBufferAllocator
    {
        IByteBuffer Buffer();

        IByteBuffer Buffer(int initialCapacity);

        IByteBuffer Buffer(int initialCapacity, int maxCapacity);

        CompositeByteBuffer CompositeBuffer();

        CompositeByteBuffer CompositeBuffer(int maxComponents);
    }
}