using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace BuDing.AutoFac.AutoFac
{
    public class AutoFacServiceProviderFactory:IServiceProviderFactory<ContainerBuilder>
    {
        private readonly ContainerBuilder _builder;

        public AutoFacServiceProviderFactory(ContainerBuilder builder)
        {
            _builder = builder;
        }


        public ContainerBuilder CreateBuilder(IServiceCollection services)
        {
           _builder.Populate(new ServiceCollection());
           return _builder;
        }

        public IServiceProvider CreateServiceProvider(ContainerBuilder containerBuilder)
        {
            return new AutofacServiceProvider(containerBuilder.Build());
        }

        private void RegisterServices(IServiceCollection services)
        {
            ServiceRegistrationActionList
        }
    }
}
