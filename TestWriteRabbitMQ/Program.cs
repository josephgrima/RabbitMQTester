using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace TestWriteRabbitMQ
{
    class Program
    {
        private static string _messageQueueName = "Queue.priceAlerts.NotificationPusherService.Debug";
        static void Main(string[] args)
        {
           SendPriceAlert();
        }

        private static void SendPriceAlert()
        {           
            var factory = new ConnectionFactory()
            {
                HostName = "192.168.1.3",
                UserName = "joey",
                Password = "joey"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
               
                channel.QueueDeclare(queue: _messageQueueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: true,
                                     arguments: null);

                string message = "{\"AlertMessage\":null,\"InstrumentCode\":\"2046251\",\"LatestPrice\":10.0,\"ChangeTime\":\"0001-01-01T00:00:00\",\"PriceThreshold\":97.1300,\"AlertRuleType\":255,\"AccountHolderId\":23047,\"AlertRuleId\":3,\"InstrumentName\":\"APPLE                                             \",\"InstrumentPriceDecimals\":2,\"InstrumentPriceMultiplier\":1,\"AlertGuid\":\"a637d41b-b16c-449d-b849-cd6631a56beb\",\"AlertId\":0,\"Created\":\"2016-08-01T12:16:26.8889151Z\",\"AlertType\":1,\"IsRead\":false}";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: _messageQueueName,
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }            
        }


        private static void SendHelloWorld()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "192.168.1.3",
                UserName = "joey",
                Password = "joey"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
