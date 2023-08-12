using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Producer
{
    public static class FanoutExchangeProducer
    {
        /// <summary>
        /// if u do not care about any route key and header , fanout can be the the correct choice
        /// </summary>
        /// <param name="channel"></param>
        public static void Publish(IModel channel)
        {
            // declare a exchange

            channel.ExchangeDeclare("my-Fanout-exchange", type: ExchangeType.Fanout);

            var count = 0;
            while (true)
            {

                var message = new { name = "producer_mahmoud", message = $"hello!!!!!  Count :{count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

              


                channel.BasicPublish("my-Fanout-exchange", "anything", null, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
