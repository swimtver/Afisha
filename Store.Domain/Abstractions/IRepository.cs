using System;
using System.Collections.Generic;
using System.Linq;
using Store.Domain.Model;

namespace Store.Domain.Abstractions
{
    public interface IRepository<T> where T : Entity
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Save(T entity);
        void Delete(T entity);
    }
}
