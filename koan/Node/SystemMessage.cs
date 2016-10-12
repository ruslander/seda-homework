using System;
using Koan.Messaging;

namespace Koan.Node
{
    public static class SystemMessage
    {
        public class SystemInit : Message { }

        public class SystemStart : Message { }

        public class BecomeShutDown : Message { }

        public class StartShutdown : Message { }

        public class ServiceShutdown : Message
        {
            public readonly string ServiceName;

            public ServiceShutdown(string serviceName)
            {
                if (string.IsNullOrWhiteSpace(serviceName))
                    throw new ArgumentOutOfRangeException("serviceName");
                ServiceName = serviceName;
            }
        }
    }
}