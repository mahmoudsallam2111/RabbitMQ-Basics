using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public static class DirectExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            // 1 - declare an exchange
            channel.ExchangeDeclare("my-direct-exchange", type: ExchangeType.Direct);

            // declare a queue

            channel.QueueDeclare("my-directexchange-queue",
                durable: true,
                exclusive: false,
                autoDelete: false, arguments: null);

            // bind the queue to the exchange
            // queuename  / exchangename /  key ==>must be the exact key of the producer
            channel.QueueBind("my-directexchange-queue", "my-direct-exchange", "account.init");

            /// create the consumer
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            //start to consume
            channel.BasicConsume("my-directexchange-queue", true, consumer);
            Console.WriteLine("consumer started");
            Console.ReadLine();
        }
    }
}
