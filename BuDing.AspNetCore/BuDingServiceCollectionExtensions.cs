using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace BuDing.AspNetCore
{
    public static class BuDingServiceCollectionExtensions
    {
        public static IServiceProvider AddBuDing(this IServiceCollection services,
            Action<ServiceBuilderOptions> options = null)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            var builder = new ServiceBuilderOptions(services);
            options?.Invoke(builder);
            builder.Initialize();
            return services.BuildServiceProviderFromFactory();
        }

        public static IServiceProvider BuildServiceProviderFromFactory(this IServiceCollection services)
        {
            foreach (var service in services)
            {
                var factoryInterface = service.ImplementationInstance?.GetType()
                    .GetTypeInfo()
                    .GetInterfaces()
                    .FirstOrDefault(i =>
                        i.GetTypeInfo().IsGenericType &&
                        i.GetTypeInfo() != typeof(IServiceProviderFactory<IServiceCollection>) &&
                        i.GetGenericTypeDefinition() == typeof(IServiceProviderFactory<>));
                if (factoryInterface == null)
                {
                    continue;
                }

                var containerBuilderType = factoryInterface.GenericTypeArguments[0];
                return (IServiceProvider) typeof(BuDingServiceCollectionExtensions)
                    .GetTypeInfo()
                    .GetMethods()
                    .Single(m => m.Name == nameof(BuildServiceProviderFromFactory) && m.IsGenericMethod)
                    .MakeGenericMethod(containerBuilderType)
                    .Invoke(null, new[] {services, null});
            }

            return services.BuildServiceProvider();
        }

        public static IServiceProvider BuildServiceProviderFromFactory<TContainerBuilder>(this IServiceCollection services,
            Action<TContainerBuilder> builderAction = null)
        {
            var serviceProviderFactory = (IServiceProviderFactory<TContainerBuilder>) services
                .FirstOrDefault(d => d.ServiceType == typeof(IServiceProviderFactory<TContainerBuilder>))
                ?.ImplementationInstance;
            if (serviceProviderFactory == null)
            {
                throw new Exception($"Could not find {typeof(IServiceProviderFactory<TContainerBuilder>).FullName} in {services}.");
            }

            var builder = serviceProviderFactory.CreateBuilder(services);
            builderAction?.Invoke(builder);
            return serviceProviderFactory.CreateServiceProvider(builder);
        }
    }
}
