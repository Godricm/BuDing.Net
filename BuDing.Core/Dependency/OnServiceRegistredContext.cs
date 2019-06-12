using System;
using System.Collections.Generic;
using System.Text;
using BuDing.DynamicProxy;
using BuDing.System.Collections;

namespace BuDing.Dependency
{
   public class OnServiceRegistredContext:IOnServiceRegistredContext
    {
        public OnServiceRegistredContext(Type serviceType, Type implementationType)
        {
            ServiceType = serviceType;
            ImplementationType = implementationType;
            Interceptors=new TypeList<InterceptorBase>();
        }

        public virtual ITypeList<InterceptorBase> Interceptors { get; }

        public  virtual Type ServiceType { get; }
        public Type ImplementationType { get; }
    }
}
