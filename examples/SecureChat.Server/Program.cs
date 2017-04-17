// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace SecureChat.Server
{
    using System;
    using System.IO;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using DotNetty.Codecs;
    using DotNetty.Handlers.Logging;
    using DotNetty.Handlers.Tls;
    using DotNetty.Transport.Bootstrapping;
    using DotNetty.Transport.Channels;
    using DotNetty.Transport.Channels.Sockets;
    using Examples.Common;

    class Program
    {
        static async Task RunServerAsync()
        {
            //在控制台命令中启用日志
            ExampleHelper.SetConsoleLogger();
            //多线程事件循环组,创建一个新实例,老板组
            var bossGroup = new MultithreadEventLoopGroup(1);
            //多线程事件循环组,创建一个新实例,工作组
            var workerGroup = new MultithreadEventLoopGroup();

            //字符串编码
            var STRING_ENCODER = new StringEncoder();
            //字符串解码
            var STRING_DECODER = new StringDecoder();
            //安全聊天服务器处理程序
            var SERVER_HANDLER = new SecureChatServerHandler();

            X509Certificate2 tlsCertificate = null;
            if (ServerSettings.IsSsl)
            {
                // 创建 X.509 证书 
                tlsCertificate = new X509Certificate2(Path.Combine(ExampleHelper.ProcessDirectory, "dotnetty.com.pfx"), "password");
            }
            try
            {
                //服务器启动
                var bootstrap = new ServerBootstrap();
                bootstrap
                    .Group(bossGroup, workerGroup)
                    //服务器套接字通道
                    .Channel<TcpServerSocketChannel>()
                    .Option(ChannelOption.SoBacklog, 100)
                    //Handler用于服务请求 “信息”日志级别。
                    .Handler(new LoggingHandler(LogLevel.INFO))
                    //设置{@链接channelhandler }这是用来服务请求{@链接通道}的。
                    .ChildHandler(
                    new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;
                        if (tlsCertificate != null)
                        {
                            //添加协议到最后
                            pipeline.AddLast(TlsHandler.Server(tlsCertificate));
                        }
                        //添加基于行分隔符的帧解码器到最后
                        pipeline.AddLast(new DelimiterBasedFrameDecoder(8192, Delimiters.LineDelimiter()));
                        pipeline.AddLast(STRING_ENCODER, STRING_DECODER, SERVER_HANDLER);
                    }));
                // 创建异步通道并绑定异步端口
                IChannel bootstrapChannel = await bootstrap.BindAsync(ServerSettings.Port);

                Console.ReadLine();
                //关闭异步
                await bootstrapChannel.CloseAsync();
            }
            finally
            {
                // 等待提供的所有 System.Threading.Tasks.Task 对象完成执行过程
                Task.WaitAll(bossGroup.ShutdownGracefullyAsync(), workerGroup.ShutdownGracefullyAsync());
            }
        }

        static void Main() => RunServerAsync().Wait();//1
    }
}