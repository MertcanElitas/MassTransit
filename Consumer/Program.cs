using MassTransit;
using Shared;
using System;
using System.Threading.Tasks;

namespace Consumer
{

    public class Message : IMessage
    {
        public string Text { get; set; }
    }
    public class MessageConsumer : IConsumer<IMessage>
    {
        public async Task Consume(ConsumeContext<IMessage> context)
        {
            Console.WriteLine($"Consumer1 den Gelen mesaj : {context.Message.Text}");
        }

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

                factory.ReceiveEndpoint(queue, endpoint => endpoint.Consumer<MessageConsumer>());
            });
            await bus.StartAsync();
            Console.ReadLine();
            await bus.StopAsync();
        }
    }
}
