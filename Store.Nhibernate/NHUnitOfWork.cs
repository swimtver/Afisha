using System;
using System.Data;
using NHibernate;
using NHibernate.Context;

namespace Store.Nhibernate
{
    public class NHUnitOfWork : INHUnitOfWork
    {
        private readonly ISession _session;
        private readonly ITransaction _transaction;

        public ISession Session { get { return this._session; } }

        public NHUnitOfWork(ISession session)
        {
            if (session == null)
                throw new ArgumentNullException("Session is null");
            _session = session;
            _session.FlushMode = FlushMode.Auto;
            _transaction = session.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void Commit()
        {
            if (!_transaction.IsActive)
                throw new InvalidOperationException("No active transation");
            _transaction.Commit();
        }

        public void Rollback()
        {
            if (this._transaction.IsActive) _transaction.Rollback();
        }

        public void Dispose()
        {
            if (!_transaction.WasCommitted && !_transaction.WasRolledBack)
                _transaction.Rollback();
            _transaction.Dispose();
            CurrentSessionContext.Unbind(_session.SessionFactory);
            _session.Close();
            _session.Dispose();
        }
    }
}