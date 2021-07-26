using Core.Models.Entities.QueueManagements;
using QueueManagement;
using QueueManagement.Helpers;
using System;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Receiver.Consumers;
using System.Threading.Tasks;

namespace Receiver
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting connection to message queue");
            var queueConfiguration = new QueueManagementConfiguration
            {
                Exchange = "Message",
                Heartbeat = 15,
                Username = "guest",
                Password = "guest",
                Port = 5672,
                PrefetchCount = 1,
                PrefetchSize = 0,
                QueueName = "Message",
                ServerUrl = "localhost",
                VirtualHost = "/"
            };

            await CreateHostBuilder(args).Build().RunAsync();
            //var receiverQueueHelper = new ReceiverMessageHelper(queueConfiguration, new RabbitManagementAdapter(), new RabbiManagementHelper());

            Console.WriteLine("Connection with message queue already ready!");
            string value = "";
            while (value != "F")
            {
                Console.WriteLine("Press F key to stop the listener process and close the application");

                value = Console.ReadLine();

                value = value?.ToUpper();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<MessageConsumer>();

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.ConfigureEndpoints(context);
                        });

                    });

                    services.AddMassTransitHostedService();
                });
    }
}
