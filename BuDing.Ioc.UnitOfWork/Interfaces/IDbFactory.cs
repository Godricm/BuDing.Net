using System;
using System.Data;

namespace BuDing.Ioc.UnitOfWork.Interfaces
{
    public interface IDbFactory
    {
        /// <summary>
        ///  Creates an instance of your ISession expanded interface
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Create<T>() where T : class, ISession;

        /// <summary>
        ///  Creates a UnitOfWork and Session at same time. The session has the same scope as the unit of work.
        /// </summary>
        /// <typeparam name="TUnitOfWork"></typeparam>
        /// <typeparam name="TSession"></typeparam>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        TUnitOfWork Create<TUnitOfWork, TSession>(IsolationLevel isolationLevel = IsolationLevel.RepeatableRead)
            where TUnitOfWork : class, IUnitOfWork where TSession : class, ISession;

        /// <summary>
        /// Used for Session base to create UnitOfWork. Not recommeded to use in code
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        /// <param name="session"></param>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        T Create<T>(IDbFactory factory, ISession session, IsolationLevel isolationLevel = IsolationLevel.RepeatableRead)
            where T : class, IUnitOfWork;

        void Release(IDisposable instance);
    }
}
