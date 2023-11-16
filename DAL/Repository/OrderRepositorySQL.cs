using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class OrderRepositorySQL : IRepository<OrderC>
    {
        private ComputerShopContext context;

        public OrderRepositorySQL(ComputerShopContext context)
        {
            this.context = context;
        }

        public void Create(OrderC item)
        {
            context.OrderC.Add(item);
        }

        public void Delete(int id)
        {
            OrderC order = context.OrderC.Find(id);
            if (order != null)
                context.OrderC.Remove(order);
        }

        public OrderC GetItem(int id)
        {
            return context.OrderC.Find(id);
        }

        public List<OrderC> GetList()
        {
            return context.OrderC.ToList();
        }

        public void Update(OrderC item)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
