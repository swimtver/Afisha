using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Store.Domain.Abstractions;

namespace Store.Nhibernate
{
    public class NHUnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new NHUnitOfWork(NHibernateHelper.GetSession());
        }
    }
}
