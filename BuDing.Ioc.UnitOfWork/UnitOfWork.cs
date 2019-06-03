using System;
using System.Data;
using System.Data.Common;
using BuDing.Ioc.UnitOfWork.Interfaces;

namespace BuDing.Ioc.UnitOfWork
{
   public class UnitOfWork:DbTransaction,IUnitOfWork
    {
        public SqlDialect SqlDialect { get; }

        private readonly  Guid _guid=Guid.NewGuid();

        public IDbTransaction Transaction { get; set; }
        public IDbConnection Connection { get; }
        public override IsolationLevel IsolationLevel { get; }


        public override void Commit()
        {
            throw new NotImplementedException();
        }

        public override void Rollback()
        {
            throw new NotImplementedException();
        }

        protected override DbConnection DbConnection { get; }
    }
}
