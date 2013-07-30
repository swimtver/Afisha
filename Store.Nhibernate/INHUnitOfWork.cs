using NHibernate;
using Store.Domain.Abstractions;

namespace Store.Nhibernate
{
    public interface INHUnitOfWork : IUnitOfWork
    {
        ISession Session { get; }
    }
}
