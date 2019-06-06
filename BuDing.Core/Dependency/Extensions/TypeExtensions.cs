using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BuDing.Core.Dependency.Extensions
{
    public static class TypeExtensions
    {
        public static ServiceLifetime? GetServiceLifetimeFromClassHierarcy(Type type)
        { 
            if (typeof(ITransientDependency).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Transient;
            }

            if (typeof(ISIngletonDependency).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Singleton;
            }

            if (typeof(IScopedDependency).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Scoped;
            }
            return null;
        }

        /// <summary>
        /// Validator the type weathor AssignableFrom  ITransientDependency ISIngletonDependency IScopedDependency
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool ValidatorServiceLifetime(Type type)
        {
            var result = false;
            var types = type.GetInterfaces();
            
            return result;
        }
    }
}
