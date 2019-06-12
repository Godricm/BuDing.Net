using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Builder;
using Autofac.Extensions.DependencyInjection;

namespace BuDing.Dependency.AutoFac
{
    public class AutofacServiceProviderFactory : IServiceProviderFactory<ContainerBuilder>
    {
        private readonly ContainerBuilder _builder;

        public AutofacServiceProviderFactory(ContainerBuilder builder)
        {
            _builder = builder;
        }


        public ContainerBuilder CreateBuilder(IServiceCollection services)
        {
            _builder.Populate(new ServiceCollection());
            RegisterServices(services);
            return _builder;
        }

        public IServiceProvider CreateServiceProvider(ContainerBuilder containerBuilder)
        {
            return new AutofacServiceProvider(containerBuilder.Build());
        }

        private void RegisterServices(IServiceCollection services)
        {
            ServiceRegistrationActionList actionList = null;
            var serviceDescriptor =
                services.FirstOrDefault(d => d.ServiceType == typeof(ServiceRegistrationActionList));
            if (serviceDescriptor != null)
            {
                actionList = (ServiceRegistrationActionList)serviceDescriptor.ImplementationInstance;
            }

            if (actionList == null)
            {
                actionList = new ServiceRegistrationActionList();
                services.AddSingleton(actionList);
            }

            foreach (var service in services)
            {
                if (service.ImplementationType != null)
                {
                    var serviceTypeInfo = service.ServiceType.GetTypeInfo();
                    if (serviceTypeInfo.IsGenericTypeDefinition)
                    {
                        _builder.RegisterGeneric(service.ImplementationType)
                            .As(service.ServiceType)
                            .ConfigureLifecycle(service.Lifetime)
                            .ConfigureConventions(actionList);
                    }
                    else
                    {
                        _builder.RegisterType(service.ImplementationType)
                            .As(service.ServiceType)
                            .ConfigureLifecycle(service.Lifetime)
                            .ConfigureConventions(actionList);
                    }
                }
                else if (service.ImplementationFactory != null)
                {
                    var registration =
                        RegistrationBuilder.ForDelegate(service.ServiceType, (context, parameters) =>
                            {

                                var serviceProvider = context.Resolve<IServiceProvider>();
                                return service.ImplementationFactory(serviceProvider);
                            }).ConfigureLifecycle(service.Lifetime)
                            .CreateRegistration();

                    _builder.RegisterComponent(registration);
                }
                else
                {
                    _builder.RegisterInstance(service.ImplementationInstance)
                        .As(service.ServiceType)
                        .ConfigureLifecycle(service.Lifetime);
                }
            }
        }
    }
}
