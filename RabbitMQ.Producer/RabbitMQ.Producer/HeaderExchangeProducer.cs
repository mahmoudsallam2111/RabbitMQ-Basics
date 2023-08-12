using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Producer
{
    public static class HeaderExchangeProducer
    {
        public static void Publish(IModel channel)
        {
            // declare a exchange

            channel.ExchangeDeclare("my-Header-exchange", type: ExchangeType.Headers);

            var count = 0;
            while (true)
            {

                var message = new { name = "producer_mahmoud", message = $"hello!!!!!  Count :{count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                // create a propert to set the header ==>must me the same as consumer
                var property = channel.CreateBasicProperties();
                property.Headers = new Dictionary<string, object> { { "mykey", "myvalue" } };


                channel.BasicPublish("my-Header-exchange", "account.anything" , property , body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
