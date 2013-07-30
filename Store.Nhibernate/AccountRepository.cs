using System;
using System.Linq;
using NHibernate.Linq;
using Store.Domain.Abstractions;
using Store.Domain.Model;

namespace Store.Nhibernate
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public Account GetWithPurchases(int id)
        {
            return Session.Query<Account>().Fetch(a => a.Purchases).SingleOrDefault(a => a.Id == id);
        }
    }
}
