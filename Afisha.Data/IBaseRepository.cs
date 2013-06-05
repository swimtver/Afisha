using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Afisha.Models;

namespace Afisha.Data
{
    public interface IBaseRepository<T> where T:Entity 
    {
        IEnumerable<T> GetList();
        T GetItem(int id);
        T Insert(T movie);
        void Update(T one);
        void Delete(int id);
    }
}
