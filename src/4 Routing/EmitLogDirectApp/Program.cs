﻿using System;
using System.Linq;
using System.Text;

namespace EmitLogDirectApp
{
   using RabbitMQ.Client;

    class EmitLogDirect
    {
        public static void Main(string[] args)
        {
            //连接工厂
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection()) //创建连接
            {
                using (var channel = connection.CreateModel()) //创建信道
                {
                    //声明一个direct类型的交换器。
                    //direct类型的交换器，通过路由规则与匹配的队列进行绑定。
                    //它会把消息路由到那些BindingKey和RoutingKey完全匹配的队列中。
                    channel.ExchangeDeclare(exchange: "direct_logs", //交换器名称
                                            type: "direct"); //交换器类型

                    var severity = (args.Length > 0) ? args[0] : "info";
                    var message = (args.Length > 1)
                                  ? string.Join(" ", args.Skip(1).ToArray())
                                  : "Hello World!";

                    var body = Encoding.UTF8.GetBytes(message);

                    //发送消息
                    channel.BasicPublish(exchange: "direct_logs", //交换器名称
                                         routingKey: severity, //路由键，交换器根据路由键将消息存储到相应的队列之中。
                                         basicProperties: null, //消息的基本属性集。
                                         body: body); //消息体（payload），真正需要发送的消息。

                    Console.WriteLine(" [x] Sent '{0}':'{1}'", severity, message);
                }
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
