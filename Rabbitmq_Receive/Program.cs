using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbitmq_Receive
{
    class Program
    {
        static void Main(string[] args)
        {
            var connetion = connetionFactory.CreateConnection();
            Receive();
        }

        private static void Receive()
        {
            try
            {
                using (var connetion = connetionFactory.CreateConnection())
                {
                    var channel = connetion.CreateModel();

                    

                    channel.ExchangeDeclare("wssExchange", ExchangeType.Direct, false, false, null);
                    channel.QueueDeclare("wssQueue", false, false, false, null);
                    channel.QueueBind("wssQueue", "wssExchange", "", null);
                    Console.WriteLine("连接成功");
                    var subscription = new Subscription(channel, "wssQueue", false);
                    Console.WriteLine("等待消息");
                    while (channel.IsOpen)
                    {
                        BasicDeliverEventArgs eventArgs;
                        var success = subscription.Next(2000, out eventArgs);
                        if (success == false) continue;
                        var msgBytes = eventArgs.Body;
                        var message = Encoding.UTF8.GetString(msgBytes);
                        Console.WriteLine(message);
                        channel.BasicAck(eventArgs.DeliveryTag, false);
                      
                    }
                }
            }catch(Exception ex)
            {
                Console.Write(ex);
            }
        }
        
        static ConnectionFactory connetionFactory = new ConnectionFactory
        {
            HostName = "111.231.82.198",
            Port = 5672,
            UserName = "admin",
            Password = "wzl19961017",
            Protocol = Protocols.DefaultProtocol,
            RequestedFrameMax = UInt32.MaxValue,
            RequestedHeartbeat = UInt16.MaxValue, //心跳时间
            AutomaticRecoveryEnabled = true, //自动重连  心跳重连
        };
    }
}
