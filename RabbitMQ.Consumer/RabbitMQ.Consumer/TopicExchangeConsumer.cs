using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public static class TopicExchangeConsumer
    {
        /// <summary>
        /// only the main difference is the the key is a pattern 
        /// </summary>
        /// <param name="channel"></param>
        public static void Consume(IModel channel)
        {
            // 1 - declare an exchange
            channel.ExchangeDeclare("my-Topic-exchange", type: ExchangeType.Topic);

            // declare a queue

            channel.QueueDeclare("my-Topicexchange-queue",
                durable: true,
                exclusive: false,
                autoDelete: false, arguments: null);

            // bind the queue to the exchange
            // queuename  / exchangename /  key ==>here the key is a pattern
            // this means that this consumer is listening to all producer the start with account.
            channel.QueueBind("my-Topicexchange-queue", "my-Topic-exchange", "account.*");

            /// create the consumer
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            //start to consume
            channel.BasicConsume("my-Topicexchange-queue", true, consumer);
            Console.WriteLine("consumer started");
            Console.ReadLine();
        }
    }
}
