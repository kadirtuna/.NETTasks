using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json.Serialization;

//Establishing a connection
ConnectionFactory factory = new();
factory.Uri = new("amqps://pmntnffl:bGXgIbPypteSjyPk3X63IXO8-J7Wk3t5@vulture.rmq.cloudamqp.com/pmntnffl");

//Activating the connection and opening a channel
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Declaring the queue
channel.QueueDeclare(queue: "example-queue", exclusive: false);

//Consuming data from the queue
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue", false, consumer);
consumer.Received += (sender, e) =>
{
    var message = JsonConvert.DeserializeObject<string>(Encoding.UTF8.GetString(e.Body.Span));
    //Console.WriteLine(Encoding.UTF8.GetString(message));
    Console.WriteLine(message);
};

Console.Read();