﻿using MassTransit;
using Shared;
using System;
using System.Threading.Tasks;

namespace Producer
{
    public class Message : IMessage
    {
        public string Text { get; set; }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            string rabbitMqUri = "amqps://kkthkxhj:5SPxQ50N3KWmm7qA2EyQH_d1RBg5YQiH@beaver.rmq.cloudamqp.com/kkthkxhj";
            string queue = "test-queue";
            string userName = "kkthkxhj";
            string password = "5SPxQ50N3KWmm7qA2EyQH_d1RBg5YQiH";

            var bus = Bus.Factory.CreateUsingRabbitMq(factory =>  
            {
                factory.Host(rabbitMqUri, configurator =>
                {
                    configurator.Username(userName);
                    configurator.Password(password);
                });
            });

            await Task.Run(async () =>
            {
                while (true)
                {
                    Console.Write("Mesaj yaz : ");
                    Message message = new Message
                    {
                        Text = Console.ReadLine()
                    };
                    if (message.Text.ToUpper() == "C")
                        break;
                    await bus.Publish<IMessage>(message);
                    Console.WriteLine("");
                }
            });
        }
    }
}
