using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Unity;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers.Registrations
{
    public class UnitConventionRegistrar
    {
        public UnitConventionRegistrar(IUnityContainer container)
        {
            var asm = Assembly.GetExecutingAssembly();
            var interfaces = asm.GetInterfaces();
            foreach (var interfaceType in interfaces.Where(t => t != typeof(IDisposable)))
            {
                var currentInterfaceType = interfaceType;
                var implementations = asm.GetImplementationOfInterface(interfaceType);
                if (implementations.Count > 1)
                {
                    implementations.ToList().ForEach(i =>
                    {
                        if (i.GetCustomAttribute<NoIoCFluentRegistration>() == null)
                        {
                            container.RegisterType(currentInterfaceType, i, i.Name);
                        }
                    });
                }
                else
                {
                    implementations.ToList().ForEach(i =>
                    {
                        if (i.GetCustomAttribute<NoIoCFluentRegistration>() == null)
                        { 
                            container.RegisterType(currentInterfaceType, i);
                        }
                    });
                }
            }
        }
    }
}
