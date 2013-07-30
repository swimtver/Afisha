using System;

namespace Store.Domain.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
