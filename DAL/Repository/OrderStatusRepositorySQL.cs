using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class OrderStatusRepositorySQL : IRepository<OrderStatus>
    {
        private ComputerShopContext context;

        public OrderStatusRepositorySQL(ComputerShopContext context)
        {
            this.context = context;
        }

        public void Create(OrderStatus item)
        {
            context.OrderStatus.Add(item);
        }

        public void Delete(int id)
        {
            OrderStatus cat = context.OrderStatus.Find(id);
            if (cat != null)
                context.OrderStatus.Remove(cat);
        }

        public OrderStatus GetItem(int id)
        {
            return context.OrderStatus.Find(id);
        }

        public List<OrderStatus> GetList()
        {
            return context.OrderStatus.ToList();
        }

        public void Update(OrderStatus item)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
