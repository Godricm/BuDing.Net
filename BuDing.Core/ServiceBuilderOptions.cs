using System;
using System.Collections.Generic;
using System.Text;
using BuDing.Dependency;
using BuDing.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

namespace BuDing
{
    public class ServiceBuilderOptions
    {
        public ServiceBuilderOptions(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }

        public void Initialize()
        {
            //aop注入
            Services.AddTransient(typeof(CastleInterceptorAdapter<>));
            //依赖注入
            Services.RegisterAssemblyByBasicInterface(GetType().Assembly);
        }
    }
}
