namespace Store.Domain.Abstractions
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }

}
