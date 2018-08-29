using System;
using System.Text;
using RabbitMQ.Client;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection()) //创建连接
            {
                using (var channel = connection.CreateModel()) //创建信道
                {
                    

                    Console.Read();
                }
            }
        }
    }
}
