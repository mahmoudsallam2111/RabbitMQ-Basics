using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    public static class QueueProducer
    {
        public static void Publish(IModel channel)
        {
            // declare a queue

            channel.QueueDeclare("my-queue",
                durable: true,
                exclusive: false,
                autoDelete: false, arguments: null);

            var count = 0;
            while (true)
            {

                var message = new { name = "producer_mahmoud", message =$"hello!!!!!  Count :{count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish("", "my-queue", null, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
