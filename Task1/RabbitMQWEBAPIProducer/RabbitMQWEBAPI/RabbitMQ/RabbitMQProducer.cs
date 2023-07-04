using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace RabbitMQWEBAPI.Producer.RabbitMQ
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        readonly IConnection _connection;
        readonly IModel _channel;

        public RabbitMQProducer()
        {
            //Establishing a connection
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://pmntnffl:bGXgIbPypteSjyPk3X63IXO8-J7Wk3t5@vulture.rmq.cloudamqp.com/pmntnffl");

            //Activating the connectiong and opening a channel
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            //Creating a queue 
            _channel.QueueDeclare(queue: "example-queue", exclusive: false);
        }

        public void SendProductMessage<T>(T message)
        {
            var json = JsonConvert.SerializeObject(message);
            byte[] body = Encoding.UTF8.GetBytes(json);
            _channel.BasicPublish(exchange: "", routingKey: "example-queue", body: body);

        }
    }
}
