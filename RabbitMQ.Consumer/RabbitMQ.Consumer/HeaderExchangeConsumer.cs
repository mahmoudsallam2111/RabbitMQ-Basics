using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public static class HeaderExchangeConsumer
    {
        
        public static void Consume(IModel channel)
        {
            // 1 - declare an exchange
            channel.ExchangeDeclare("my-Header-exchange", type: ExchangeType.Headers);

            // declare a queue

            channel.QueueDeclare("my-Headerexchange-queue",
                durable: true,
                exclusive: false,
                autoDelete: false, arguments: null);

            /// create a header
            /// this must match the header exist in the producer
            var header = new Dictionary<string, object> { { "mykey", "myvalue" } };

            // bind the queue to the exchange
            // queuename  / exchangename /  key ==> is empty  // header 
            channel.QueueBind("my-Headerexchange-queue", "my-Header-exchange", string.Empty , header);

        

            /// create the consumer
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            //start to consume
            channel.BasicConsume("my-Headerexchange-queue", true, consumer);
            Console.WriteLine("consumer started");
            Console.ReadLine();
        }
    }
}
