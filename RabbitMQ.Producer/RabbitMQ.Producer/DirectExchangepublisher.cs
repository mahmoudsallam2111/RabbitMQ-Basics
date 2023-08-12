using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace RabbitMQ.Producer
{
    public static class DirectExchangepublisher
    {
        public static void Publish(IModel channel)
        {
            // declare a exchange

            channel.ExchangeDeclare("my-direct-exchange", type:ExchangeType.Direct);

            var count = 0;
            while (true)
            {

                var message = new { name = "producer_mahmoud", message = $"hello!!!!!  Count :{count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish("my-direct-exchange", "account.init", null, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
