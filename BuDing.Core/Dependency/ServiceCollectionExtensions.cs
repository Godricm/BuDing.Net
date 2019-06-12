using System;
using System.Linq;
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

        /// <summary>
        /// 服务注册事件
        /// </summary>
        /// <param name="services"></param>
        /// <param name="registrationAction"></param>
        public static void OnRegistred(this IServiceCollection services,
            Action<IOnServiceRegistredContext> registrationAction)
        {
            ServiceRegistrationActionList actionList = null;
            var serviceDescriptor =
                services.FirstOrDefault(d => d.ServiceType == typeof(ServiceRegistrationActionList));
            if (serviceDescriptor != null)
            {
                actionList = (ServiceRegistrationActionList) serviceDescriptor.ImplementationInstance;
            }

            if (actionList == null)
            {
                actionList=new ServiceRegistrationActionList();
                services.AddSingleton(actionList);
            }
            actionList.Add(registrationAction);
        }
    }
}
