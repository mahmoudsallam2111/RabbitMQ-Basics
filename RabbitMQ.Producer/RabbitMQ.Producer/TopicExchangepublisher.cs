using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Producer
{
    public static class TopicExchangepublisher
    {
        public static void Publish(IModel channel)
        {
            // declare a exchange

            channel.ExchangeDeclare("my-Topic-exchange", type: ExchangeType.Topic);

            var count = 0;
            while (true)
            {

                var message = new { name = "producer_mahmoud", message = $"hello!!!!!  Count :{count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish("my-Topic-exchange", "account.anything", null, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
