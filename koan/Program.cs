using System;
using Koan.Messaging;
using Koan.Node;

namespace Koan
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = new InMemoryBus("OutputBus");
            var controller = new NodeController(bus);
            var nodeQueue = new NodeQueueHandler(controller, "Main Queue");


            // Hello world service
            var service = new HelloWorldService(nodeQueue);
            bus.Subscribe<SystemMessage.SystemInit>(service);
            bus.Subscribe<SystemMessage.StartShutdown>(service);
            bus.Subscribe<HelloWorldMessage.Hi>(service);


            // TIMER
            var timer = new TimerService(new ThreadBasedScheduler(new RealTimeProvider()));
            bus.Subscribe<TimerMessage.Schedule>(timer);


            Console.WriteLine("Starting everything. Press enter to initiate shutdown");

            nodeQueue.Start();

            nodeQueue.Publish(new SystemMessage.SystemInit());
            Console.ReadLine();
            nodeQueue.Publish(new SystemMessage.StartShutdown());
            Console.ReadLine();
        }
    }

    

    
}
