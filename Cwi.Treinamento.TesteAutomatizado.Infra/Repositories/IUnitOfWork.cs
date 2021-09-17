using System;
using System.Data;

namespace Cwi.Treinamento.TesteAutomatizado.Infra.Repositories
{
    public interface IUnitOfWork<T> : IDisposable
    {
        Guid Id { get; }

        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; }

        void Begin();

        void Begin(IsolationLevel isolationLevel);

        void Commit();

        void Rollback();
    }
}
