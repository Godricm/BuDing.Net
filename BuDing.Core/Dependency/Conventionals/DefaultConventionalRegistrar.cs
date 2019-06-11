using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using BuDing.Core.Dependency.Attributes;
using BuDing.Dependency;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BuDing.Core.Dependency.Conventionals
{
   public class DefaultConventionalRegistrar:ConventionalRegistrarBase
    {
        public override void AddType(IServiceCollection services, Type type)
        {
            //验证是否禁用
            if (IsConventionalRegistrationDisabled(type))
            {
                return;
            }

            //获取依赖标识
            var dependencyAttribute = GetDependencyAttributeOrNull(type);
            var lifeTime = GetLifeTimeOrNull(type, dependencyAttribute);
            if (lifeTime == null)
            {
                return;
            }

            foreach (var serviceType in AutoRegistrationHelper.GetExposedServices(services,type))
            {
                var serviceDescriptor = ServiceDescriptor.Describe(serviceType, type, lifeTime.Value);
                if (dependencyAttribute?.ReplaceServices == true)
                {
                    services.Replace(serviceDescriptor);
                }else if (dependencyAttribute?.TryRegister == true)
                {
                    services.TryAdd(serviceDescriptor);
                }
                else
                {
                    services.Add(serviceDescriptor);
                }
            }
        }

        protected virtual bool IsConventionalRegistrationDisabled(Type type)
        {
            return type.IsDefined(typeof(DisableConventionalsRegistratorAttribute), true);
        }

        /// <summary>
        /// 获取依赖标识信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual DependencyAttribute GetDependencyAttributeOrNull(Type type)
        {
            return type.GetCustomAttribute<DependencyAttribute>(true);
        }

        protected virtual ServiceLifetime? GetLifeTimeOrNull(Type type, DependencyAttribute dependencyAttribute)
        {
            return dependencyAttribute.Lifetime ?? GetServiceLifetimeFromCLassHierarcy(type);
        }

        protected virtual ServiceLifetime? GetServiceLifetimeFromCLassHierarcy(Type type)
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
    }
}
