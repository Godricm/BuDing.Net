using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers;
using BuDing.Ioc.UnitOfWork.Interfaces;
using SimpleInjector;
using SimpleInjector.Diagnostics;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.ExampleTests.Ioc.IoC_Example_Installers
{
    [NoIoCFluentRegistration]
    public class SimpleInjectorRegistrar
    {
        public void Register(Container container)
        {
            container.RegisterInstance<IDbFactory>(new SimpleInjectorDbFactory(container));
        }
        public static void RegisterDisposableTransient(Container container, Type service, Type implementation)
        {
            var reg = Lifestyle.Transient.CreateRegistration(implementation, container);
            reg.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "suppressed.");
            container.AddRegistration(service, reg);
        }


        [NoIoCFluentRegistration]
        sealed class SimpleInjectorDbFactory:IDbFactory
        {
            private readonly Container _container;

            public SimpleInjectorDbFactory(Container container)
            {
                _container = container;
            }

            public T Create<T>() where T : class, ISession
            {
                return _container.GetInstance<T>();
            }

            public T CreateSession<T>() where T : class, ISession
            {
                return _container.GetInstance<T>();
            }
            public TUnitOfWork Create<TUnitOfWork, TSession>(IsolationLevel isolationLevel = IsolationLevel.Serializable) where TUnitOfWork : class, IUnitOfWork where TSession : class, ISession
            {
                return new BuDing.Ioc.UnitOfWork.UnitOfWork(_container.GetInstance<IDbFactory>(), Create<TSession>(),
                    isolationLevel, true) as TUnitOfWork;
            }
            public T Create<T>(IDbFactory factory, ISession session, IsolationLevel isolationLevel = IsolationLevel.Serializable) where T : class, IUnitOfWork
            {
                return new BuDing.Ioc.UnitOfWork.UnitOfWork(factory, session, isolationLevel) as T;
            }
            public void Release(IDisposable instance)
            {
                instance?.Dispose();
            }
        }
    }
}
