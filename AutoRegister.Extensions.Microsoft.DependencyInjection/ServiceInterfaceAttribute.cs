using System;

namespace Microsoft.Extensions.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class ServiceInterfaceAttribute : Attribute
    {
        public ServiceLifetime ServiceLifetime { get; private set; }

        public ServiceInterfaceAttribute(ServiceLifetime serviceLifetime)
        {
            ServiceLifetime = serviceLifetime;
        }
    }
}