using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using Store.Domain.Abstractions;
using Store.Domain.Model;
using NHibernate;

namespace Store.Nhibernate
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected ISession Session { get; private set; }

        public Repository(IUnitOfWork unitOfWork)
        {
            Session = ((INHUnitOfWork)unitOfWork).Session;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Session.Query<T>().ToList();
        }
        public virtual T Get(int id)
        {
            return Session.Get<T>(id);
        }
        public virtual void Save(T entity)
        {
            Session.SaveOrUpdate(entity);
        }
        public virtual void Delete(T entity)
        {
            Session.Delete(entity);
        }
    }
}