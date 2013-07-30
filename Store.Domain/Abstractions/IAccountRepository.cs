using Store.Domain.Model;

namespace Store.Domain.Abstractions
{
    public interface IAccountRepository:IRepository<Account>
    {
        Account GetWithPurchases(int id);
    }
}
