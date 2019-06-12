using System;
using BuDing.DynamicProxy;
using BuDing.System.Collections;

namespace BuDing.Dependency
{
    public interface IOnServiceRegistredContext
    {
       ITypeList<InterceptorBase> Interceptors { get; }

       Type ImplementationType { get; }

    }
}
