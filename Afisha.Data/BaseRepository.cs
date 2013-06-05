using Afisha.Models;
using BLToolkit.Data;
using BLToolkit.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Afisha.Data
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private DbManager _db;

        public BaseRepository()
        {
            _db = new DbManager();
        }

        public virtual IEnumerable<T> GetList()
        {
            return Query().SelectAll();
        }

        public virtual T GetItem(int id)
        {
            return Query().SelectByKey(id);
        }

        public virtual T Insert(T item)
        {
            var id = Query().Insert(item);
            item.Id = id;
            return item;
        }

        public virtual void Update(T item)
        {
            Query().Update(item);
        }

        public virtual void Delete(int id)
        {
            Query().DeleteByKey(id);
        }

        protected SqlQuery<T> Query()
        {
            return new SqlQuery<T>(_db);
        }

        protected SprocQuery<T> Sproc()
        {
            return new SprocQuery<T>(_db);
        }
    }    
}
