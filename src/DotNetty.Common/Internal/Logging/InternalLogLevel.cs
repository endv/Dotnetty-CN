// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DotNetty.Common.Internal.Logging
{
    /// <summary>内部日志级别 The log level that {@link IInternalLogger} can log at.</summary>
    public enum InternalLogLevel
    {
        /// <summary>
        ///   “跟踪”日志级别。  'TRACE' log level.
        /// </summary>
        TRACE,

        /// <summary>
        /// “调试”日志级别。  'DEBUG' log level.
        /// </summary>
        DEBUG,

        /// <summary>
        /// “信息”日志级别。    'INFO' log level.
        /// </summary>
        INFO,

        /// <summary>
        ///  警告日志级别。   'WARN' log level.
        /// </summary>
        WARN,

        /// <summary>
        ///  “错误”日志级别。   'ERROR' log level.
        /// </summary>
        ERROR
    }
}