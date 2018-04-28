using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbitmq_Send
{
    class Program
    {
        static void Main(string[] args)
        {
            SendMessage();


        }



        private static void SendMessage()
        {
            using (var connetion = connetionFactory.CreateConnection())
            {
                var channel = connetion.CreateModel();
                channel.BasicQos(1, 0, false);
                channel.ExchangeDeclare("wssExchange", ExchangeType.Direct,false,false,null);
                channel.QueueDeclare("wssQueue", false, false, false, null);
                channel.QueueBind("wssQueue", "wssExchange", "", null);

                //设置消息属性 
                var properties = channel.CreateBasicProperties();
                properties.DeliveryMode=2; //消息不受 服务器重启的影响

                //发送消息
                for (int i = 0; i < 10; i++)
                {
                    string msg = $"这是消息{i},当前时间为{DateTime.Now.ToShortTimeString()}";
                    var mesBytes = Encoding.UTF8.GetBytes(msg);
                    channel.BasicPublish("wssExchange", "wssQueue", properties, mesBytes);
                }
            }

        }










        static ConnectionFactory  connetionFactory = new ConnectionFactory
        {
            HostName = "111.231.82.198",
            Port = 5672,
            UserName = "admin",
            Password = "wzl19961017",
            Protocol = Protocols.AMQP_0_9_1,
            RequestedFrameMax = UInt32.MaxValue,
            RequestedHeartbeat = UInt16.MaxValue, //心跳时间
            AutomaticRecoveryEnabled = true, //自动重连  心跳重连
        };
    }
}
