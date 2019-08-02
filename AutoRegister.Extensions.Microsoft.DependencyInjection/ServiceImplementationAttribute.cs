using System;

namespace Microsoft.Extensions.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ServiceImplementationAttribute : Attribute
    {
        public ServiceLifetime ServiceLifetime { get; private set; }
        public Type InterfaceType { get; private set; }

        public ServiceImplementationAttribute(ServiceLifetime serviceLifetime, Type interfaceType = null)
        {
            ServiceLifetime = serviceLifetime;
            InterfaceType = interfaceType;
        }
    }
}