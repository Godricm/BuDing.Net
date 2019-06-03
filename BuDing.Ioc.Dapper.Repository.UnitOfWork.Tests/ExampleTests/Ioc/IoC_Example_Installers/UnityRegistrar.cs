using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers;
using BuDing.Ioc.UnitOfWork.Interfaces;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Resolution;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.ExampleTests.Ioc.IoC_Example_Installers
{
    [NoIoCFluentRegistration]
    public class UnityRegistrar
    {
        public void Register(IUnityContainer container)
        {
            container.RegisterType<IDbFactory, UnityDbFactory>(new ContainerControlledLifetimeManager(),
                new InjectionConstructor(container));
            container.RegisterType<IUnitOfWork, BuDing.Ioc.UnitOfWork.UnitOfWork>();
        }
        [NoIoCFluentRegistration]
        private sealed class  UnityDbFactory:IDbFactory
        {
            private readonly IUnityContainer _container;

            public UnityDbFactory(IUnityContainer container)
            {
                _container = container;
            }

            public  T Create<T>() where T : class,ISession
            {
                return _container.Resolve<T>();
            }

            public TUnitOfWork Create<TUnitOfWork, TSession>(
                IsolationLevel isolationLevel = IsolationLevel.Serializable) where TUnitOfWork : class, IUnitOfWork
                where TSession : class, ISession
            {
                return _container.Resolve<TUnitOfWork>(
                    new ParameterOverride("factory", _container.Resolve<IDbFactory>()),
                    new ParameterOverride("session", Create<TSession>()),
                    new ParameterOverride("isolationLevel", isolationLevel),
                    new ParameterOverride("sessionOnlyForThisUnitOfWork", true));
            }

            public T Create<T>(IDbFactory factory, ISession session,
                IsolationLevel isolationLevel = IsolationLevel.Serializable) where T : class, IUnitOfWork
            {
                return _container.Resolve<T>(new ParameterOverride("factory", factory),
                    new ParameterOverride("session", session), new ParameterOverride("isolationLevel", isolationLevel),
                    new ParameterOverride("sessionOnlyForThisUnitOfWork", false));
            }

            public void Release(IDisposable instance)
            {
                instance.Dispose();
            }
        }
    }

}
