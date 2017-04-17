// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DotNetty.Handlers.Logging
{
    using DotNetty.Common.Internal.Logging;
    /// <summary>
    /// 日志级别
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// “微量”日志级别。
        /// </summary>
        TRACE = InternalLogLevel.TRACE,

        /// <summary>
        /// “DEBUG”日志级别。
        /// </summary>
        DEBUG = InternalLogLevel.DEBUG,

        /// <summary>
        /// “信息”日志级别。
        /// </summary>
        INFO = InternalLogLevel.INFO,

        /// <summary>
        /// 警告日志级别。
        /// </summary>
        WARN = InternalLogLevel.WARN,

        /// <summary>
        /// “错误”日志级别。
        /// </summary>
        ERROR = InternalLogLevel.ERROR,
    }
}