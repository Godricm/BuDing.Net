using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Autofac;
using BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.TestHelpers;
using BuDing.Ioc.UnitOfWork.Interfaces;

namespace BuDing.Ioc.Dapper.Repository.UnitOfWork.Tests.ExampleTests.Ioc.IoC_Example_Installers
{
    [NoIoCFluentRegistration]
    public class AutofacRegistrar:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Register(builder);
        }

        public void Register(ContainerBuilder builder)
        {
            builder.Register(c => new DbFactory(c.Resolve<IComponentContext>())).As<IDbFactory>().SingleInstance();
            builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>));
            builder.RegisterType<BuDing.Ioc.UnitOfWork.UnitOfWork>().As<IUnitOfWork>();
            //ToDo something like this to inject IRepository interfaces without a named interface
            //var assemblies = AssemblyHelper.GetReferencingAssemblies(AssemblyHelper.ProjectName);
            // builder.RegisterAssemblyTypes(assemblies.ToArray()).AsClosedTypesOf(typeof(IRepository<,>));
        }

        [NoIoCFluentRegistration]
        sealed class  DbFactory:IDbFactory
        {
            private readonly IComponentContext _container;

            public DbFactory(IComponentContext container)
            {
                _container = container;
            }


            public T Create<T>() where T : class, ISession
            {
                return _container.Resolve<T>();
            }

            public TUnitOfWork Create<TUnitOfWork, TSession>(IsolationLevel isolationLevel = IsolationLevel.RepeatableRead) where TUnitOfWork : class, IUnitOfWork where TSession : class, ISession
            {
                return _container.Resolve<TUnitOfWork>(new NamedParameter("factory", _container.Resolve<IDbFactory>()),
                    new NamedParameter("session", Create<TSession>()), new NamedParameter("isolationLevel", isolationLevel)
                    , new NamedParameter("sessionOnlyForThisUnitOfWork", true));
            }

            public T Create<T>(IDbFactory factory, ISession session, IsolationLevel isolationLevel = IsolationLevel.RepeatableRead) where T : class, IUnitOfWork
            {
                return _container.Resolve<T>(new NamedParameter("factory", factory),
                    new NamedParameter("session", session), new NamedParameter("isolationLevel", isolationLevel));
            }

            public void Release(IDisposable instance)
            {
                instance.Dispose();
            }
        }
    }
}
