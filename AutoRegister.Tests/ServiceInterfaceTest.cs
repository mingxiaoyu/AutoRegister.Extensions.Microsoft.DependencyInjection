using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace AutoRegister.Tests
{
    public class ServiceInterfaceTest
    {
        [ServiceInterfaceAttribute(ServiceLifetime.Scoped)]
        interface ITestService
        {
        }

        class TestServiceOne : ITestService
        {
        }

        class TestServiceTwo : ITestService
        {
        }

        [Fact]
        public void ServicesInterfacesTest()
        {
            IServiceCollection services = new ServiceCollection();
            ServiceCollectionExtensions.AddServicesInterfaces(services, typeof(ServiceImplementationTest).GetTypeInfo().Assembly);
            var provider = services.BuildServiceProvider();

            var interfaces = provider.GetServices<ITestService>();
            Assert.True(interfaces.Count() == 2);
        }

        [ServiceInterfaceAttribute(ServiceLifetime.Scoped)]
        public interface IGenericServicesInterfaces<T> { }

        public class IGenericServicesInterfaces1 : IGenericServicesInterfaces<string>
        {

        }
        public class IGenericServicesInterfaces2 : IGenericServicesInterfaces<int>
        {

        }

        [Fact]
        public void AddServicesInterfacesGenericTest()
        {
            IServiceCollection services = new ServiceCollection();
            ServiceCollectionExtensions.AddServicesInterfaces(services, typeof(ServiceImplementationTest).GetTypeInfo().Assembly);
            var provider = services.BuildServiceProvider();

            var strInterface = provider.GetServices<IGenericServicesInterfaces<string>>();
            Assert.True(strInterface.Count() == 1);

            var intInterface = provider.GetServices<IGenericServicesInterfaces<int>>();
            Assert.True(intInterface.Count() == 1);

        }
    }
}
