namespace Store.Domain.Model
{
    public class Purchase : Entity
    {
        public virtual Account Account { get; set; }
        public virtual string Name { get; set; }
        public virtual double Price { get; set; }
        public virtual int Count { get; set; }
    }
}
