using System;
using System.Data;

namespace Cwi.Treinamento.TesteAutomatizado.Infra.Repositories
{
    public sealed class UnitOfWork<T> : IUnitOfWork<T>, IDisposable
    {
        IDbContext _context;
        IDbTransaction _transaction = null;
        Guid _id = Guid.Empty;

        public UnitOfWork(IDbContext context)
        {
            _id = Guid.NewGuid();
            _context = context;
        }

        IDbConnection IUnitOfWork<T>.Connection
        {
            get
            {
                return _context.GetConnection();
            }
        }
        IDbTransaction IUnitOfWork<T>.Transaction
        {
            get { return _transaction; }
        }
        Guid IUnitOfWork<T>.Id
        {
            get { return _id; }
        }

        public void Begin()
        {
            if (_transaction == null)
                _transaction = _context.GetConnection().BeginTransaction();
        }

        public void Begin(IsolationLevel isolationLevel)
        {
            if (_transaction == null)
                _transaction = _context.GetConnection().BeginTransaction(isolationLevel);
        }

        public void Commit()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                DisposeTransaction();
            }
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                DisposeTransaction();
            }
        }

        public void DisposeTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            DisposeTransaction();

            _context.GetConnection()?.Dispose();
        }
    }
}
