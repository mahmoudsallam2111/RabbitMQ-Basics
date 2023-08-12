using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public static class QueueCoonsumer
    {
        public static void Consume(IModel channel)
        {
            // declare a queue

            channel.QueueDeclare("my-queue",
                durable: true,
                exclusive: false,
                autoDelete: false, arguments: null);
            /// create the consumer
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume("my-queue", true, consumer);
            Console.WriteLine("consumer started");
            Console.ReadLine();
        }
    }
}
