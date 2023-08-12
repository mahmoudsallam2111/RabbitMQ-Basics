using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public static  class FanoutExchangeConsumer
    {
        /// <summary>
        /// here the consumer get the message regardless the key
        /// </summary>
        /// <param name="channel"></param>
        public static void Consume(IModel channel)
        {
            // 1 - declare an exchange
            channel.ExchangeDeclare("my-Fanout-exchange", type: ExchangeType.Fanout);

            // declare a queue

            channel.QueueDeclare("my-Fanoutexchange-queue",
                durable: true,
                exclusive: false,
                autoDelete: false, arguments: null);

  
            // bind the queue to the exchange
            // queuename  / exchangename /  key ==> is empty  //  key is not matter here 
            // the consumer start getting message if it register to the queue
            channel.QueueBind("my-Fanoutexchange-queue", "my-Fanout-exchange", string.Empty);



            /// create the consumer
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            //start to consume
            channel.BasicConsume("my-Fanoutexchange-queue", true, consumer);
            Console.WriteLine("consumer started");
            Console.ReadLine();
        }
    }
}
