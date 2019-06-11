using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace BuDing.Core.Dependency.Conventionals
{
    public abstract class ConventionalRegistrarBase:IConventionalRegistrar
    {
        public void AddAssembly(IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes().Where(t => t != null && t.IsClass && !t.IsAbstract && !t.IsGenericType)
                .ToArray();
            AddTypes(services,types);
        }

        public void AddTypes(IServiceCollection services, params Type[] types)
        {
            foreach (var type in types)
            {
                AddType(services,type);
            }
        }

        public abstract void AddType(IServiceCollection services, Type type);
    }
}
