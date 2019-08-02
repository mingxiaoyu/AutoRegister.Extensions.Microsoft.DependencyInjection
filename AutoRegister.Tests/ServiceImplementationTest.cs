using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace AutoRegister.Tests
{
    public class ServiceImplementationTest
    {
        interface ITestService
        {
        }

        [ServiceImplementation(ServiceLifetime.Scoped, typeof(ITestService))]
        class TestService : ITestService
        {
        }
        class UnLoadTestService : ITestService
        {
        }

        [Fact]
        public void ServiceImplementationForInterfaceTest()
        {
            IServiceCollection services = new ServiceCollection();
            ServiceCollectionExtensions.AddServicesClasses(services, typeof(ServiceImplementationTest).GetTypeInfo().Assembly);
            var provider = services.BuildServiceProvider();
            var testServices = provider.GetServices<ITestService>();
            var testService = provider.GetService<ITestService>();

            Assert.True(testServices.Count() == 1);
            Assert.True(testService.GetType() == typeof(TestService));

        }

        [ServiceImplementation(ServiceLifetime.Scoped)]
        class SelfTest
        {

        }

        [Fact]
        public void ServiceImplementationForClassTest()
        {
            IServiceCollection services = new ServiceCollection();
            ServiceCollectionExtensions.AddServicesClasses(services, typeof(ServiceImplementationTest).GetTypeInfo().Assembly);
            var provider = services.BuildServiceProvider();
            var selfTests = provider.GetServices<SelfTest>();
            Assert.True(selfTests.Count() == 1);
        }


        interface IGeneric<T> { }

        [ServiceImplementation(ServiceLifetime.Scoped, typeof(IGeneric<>))]
        class GenericTest<T> : IGeneric<T>
        {

        }
        [Fact]
        public void ServiceImplementationForGenericTest()
        {
            IServiceCollection services = new ServiceCollection();
            ServiceCollectionExtensions.AddServicesClasses(services, typeof(ServiceImplementationTest).GetTypeInfo().Assembly);
            var provider = services.BuildServiceProvider();


            var generic = provider.GetService<IGeneric<UnLoadTestService>>();
            Assert.True(generic.GetType() == typeof(GenericTest<UnLoadTestService>));

        }


        interface IMultipelOne
        {

        }
        interface IMultipelTwo
        {

        }

        [ServiceImplementation(ServiceLifetime.Scoped, typeof(IMultipelOne))]
        [ServiceImplementation(ServiceLifetime.Scoped, typeof(IMultipelTwo))]
        class Multipe : IMultipelOne, IMultipelTwo
        {
        }

        [Fact]
        public void ServiceImplementationForMultipeTest()
        {
            IServiceCollection services = new ServiceCollection();
            ServiceCollectionExtensions.AddServicesClasses(services, typeof(ServiceImplementationTest).GetTypeInfo().Assembly);
            var provider = services.BuildServiceProvider();


            var one = provider.GetServices<IMultipelOne>();
            Assert.True(one.Count() == 1);
            Assert.True(one.ToList()[0].GetType() == typeof(Multipe));

            var two = provider.GetServices<IMultipelTwo>();
            Assert.True(two.Count() == 1);
            Assert.True(two.ToList()[0].GetType() == typeof(Multipe));
        }
    }
}
