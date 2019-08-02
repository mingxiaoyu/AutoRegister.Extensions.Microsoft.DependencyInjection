# AutoRegister.Extensions.Microsoft.DependencyInjection


1. Register for class

		interface ITestService
        {
        }

        [ServiceImplementation(ServiceLifetime.Scoped, typeof(ITestService))]
        class TestService : ITestService
        {
        }
		
		[ServiceImplementation(ServiceLifetime.Scoped)]
        class TestServiceTwo 
        {
        }
		
		it only register one.
2. Register for interface

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

	it will register the implementation classes of all inherited interfaces.

3.  how to use in serviceCollection

	services.AddAutoRegister(typeof(Startup).GetTypeInfo().Assembly);
	