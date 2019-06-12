 
using Autofac;
using BuDing.Dependency.AutoFac;
using Microsoft.Extensions.DependencyInjection;

namespace BuDing.Dependency
{
    public static class AutofacServiceBuilderExtensions
    {
        public static ServiceBuilderOptions UseAutofac(this ServiceBuilderOptions options)
        {
            var builder = new ContainerBuilder();
            options.Services.AddSingleton<IServiceProviderFactory<ContainerBuilder>>(
                new AutofacServiceProviderFactory(builder));
            return options;

        }
    }
}
