using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAutoRegister(this IServiceCollection services, params Assembly[] assemblies)
        {
            if (assemblies == null || assemblies.Length == 0) throw new ArgumentNullException(nameof(assemblies));
            AddServicesClasses(services, assemblies);
            AddServicesInterfaces(services, assemblies);
        }

        internal static void AddServicesClasses(IServiceCollection services, params Assembly[] assembliesToScan)
        {
            assembliesToScan = assembliesToScan as Assembly[] ?? assembliesToScan.ToArray();

            var implementationTypes = assembliesToScan.GetAllTypesOfAttribute<ServiceImplementationAttribute>();

            foreach (var implementationType in implementationTypes)
            {
                var attributes = implementationType.GetCustomAttributes<ServiceImplementationAttribute>();
                foreach (var attribute in attributes)
                {
                    var interfaceType = attribute.InterfaceType ?? implementationType.AsType();

                    services.Add(
                        new ServiceDescriptor(
                            interfaceType,
                            implementationType.AsType(),
                            attribute.ServiceLifetime
                            ));
                }
            }
        }

        internal static void AddServicesInterfaces(IServiceCollection services, params Assembly[] assembliesToScan)
        {
            assembliesToScan = assembliesToScan as Assembly[] ?? assembliesToScan.ToArray();

            var interfaceTypes = assembliesToScan.GetAllTypesOfAttribute<ServiceInterfaceAttribute>();

            foreach (var interfaceType in interfaceTypes)
            {
                var attribute = interfaceType.GetCustomAttribute<ServiceInterfaceAttribute>();
                var types = assembliesToScan.GetImplementingTypesOf(interfaceType);

                foreach (var type in types)
                {
                    services.Add(
                           new ServiceDescriptor(
                            interfaceType.IsGenericType ? type.AsType().GetInterface(interfaceType.Name) : interfaceType.AsType(),
                            type.AsType(),
                            attribute.ServiceLifetime
                          ));

                }

            }
        }
    }
}
