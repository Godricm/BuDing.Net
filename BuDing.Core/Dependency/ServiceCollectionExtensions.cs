using System.Reflection;
using BuDing.Core.Dependency.Conventionals;
using Microsoft.Extensions.DependencyInjection;

namespace BuDing.Dependency
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterAssemblyByBasicInterface(this IServiceCollection services,
            Assembly assembly)
        {
            var register = new DefaultConventionalRegistrar();
            register.AddAssembly(services,assembly);
            return services;
        }
    }
}
