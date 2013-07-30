using System.Collections.Generic;

namespace Store.Domain.Model
{
    public class Account : Entity
    {
        public Account()
        {
            Purchases = new List<Purchase>();
        }
        public virtual string Name { get; set; }
        public virtual IList<Purchase> Purchases { get; set; }
    }
}
