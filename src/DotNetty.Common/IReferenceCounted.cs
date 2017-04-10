// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DotNetty.Common
{
    /// <summary>
    /// 引用计数的可复用对象的接口
    /// </summary>
    public interface IReferenceCounted
    {
        /// <summary>
        /// 返回此对象的引用计数
        /// </summary>
        int ReferenceCount { get; }

        /// <summary>
        /// 保留 引用计数 +1
        /// </summary>
        IReferenceCounted Retain();

        /// <summary>
        ///  增加引用计数   <see cref="increment" />.
        /// </summary>
        IReferenceCounted Retain(int increment);

        /// <summary>
        ///   触摸 为调试目的，记录当前对象的访问位置。
        ///   如果这个对象是确定被泄露，信息记录这一操作 
        /// </summary>
        /// <returns></returns>
        IReferenceCounted Touch();

        /// <summary>
        ///     记录该对象的当前访问的位置，用于调试目的额外的任意信息。
        ///     如果该对象被确定为泄漏，此操作记录的信息将通过 资源泄漏检测器 <see cref="ResourceLeakDetector" /> 提供给您
        ///     
        /// </summary>
        IReferenceCounted Touch(object hint);

        /// <summary>
        ///   减少引用计数1和释放该对象的引用计数达到0时。
        /// </summary>
        /// <returns>true ：当且仅当引用计数为0，此对象已经被释放</returns>
        bool Release();

        /// <summary>
        ///  减少引用计数，释放该对象如果引用计数为0时 <see cref="decrement" />  
        /// </summary>
        /// <returns>true 当且仅当引用计数为0，此对象已被释放</returns>
        bool Release(int decrement);
    }
}