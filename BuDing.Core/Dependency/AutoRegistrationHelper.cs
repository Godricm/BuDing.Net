using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BuDing.Core.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace BuDing.Dependency
{
    public static class AutoRegistrationHelper
    {
        public static IEnumerable<Type> GetExposedServices(IServiceCollection services, Type type)
        {
            var typeInfo = type.GetTypeInfo();
            var customExposedServices = typeInfo
                .GetCustomAttributes()
                .OfType<IExposedServiceTypesProvider>().SelectMany(p => p.GetExposedServiceTypes(type))
                .ToList();
            if (customExposedServices.Any())
            {
                return customExposedServices;
            }

            return GetDefaultExposedServices(services, type);
        }

        private static IEnumerable<Type> GetDefaultExposedServices(IServiceCollection services, Type type)
        {
            var serviceTypes=new List<Type>(){type};

            foreach (var interfaceType in type.GetTypeInfo().GetInterfaces())
            {
                var interfaceName = interfaceType.Name;
                if (interfaceName.StartsWith("I"))
                {
                    interfaceName = interfaceName.Substring(1, interfaceName.Length - 1);
                }

                if (type.Name.EndsWith(interfaceName))
                {
                    serviceTypes.Add(interfaceType);
                }
            }

            return serviceTypes;
        }
    }
}
