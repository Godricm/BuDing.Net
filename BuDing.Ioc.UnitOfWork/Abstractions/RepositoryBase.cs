using System;
using System.Collections.Generic;
using System.Text;
using BuDing.Ioc.UnitOfWork.Interfaces;

namespace BuDing.Ioc.UnitOfWork.Abstractions
{
    public abstract class RepositoryBase:IRepository
    {
        protected RepositoryBase(IDbFactory factory)
        {
            Factory = factory;
        }

        public IDbFactory Factory { get; }

    }
}
