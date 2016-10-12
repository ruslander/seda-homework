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
            var inputQueue = new QueuedHandler(controller, "Main Queue");


            // Hello world service
            var app = new HelloWorldService(inputQueue);
            bus.Subscribe<SystemMessage.SystemInit>(app);
            bus.Subscribe<SystemMessage.StartShutdown>(app);
            bus.Subscribe<HelloWorldMessage.Hi>(app);


            // TIMER
            var timer = new TimerService(new ThreadBasedScheduler(new RealTimeProvider()));
            bus.Subscribe<TimerMessage.Schedule>(timer);


            Console.WriteLine("Starting everything. Press enter to initiate shutdown");

            inputQueue.Start();

            inputQueue.Publish(new SystemMessage.SystemInit());
            Console.ReadLine();
            inputQueue.Publish(new SystemMessage.StartShutdown());
            Console.ReadLine();
        }
    }

    

    
}
