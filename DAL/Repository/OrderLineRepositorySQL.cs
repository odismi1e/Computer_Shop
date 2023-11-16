using System.Collections.Generic;
using System.Linq;


namespace DAL
{
    public class OrderLineRepositorySQL : IRepository<Order_line>
    {
        private ComputerShopContext context;

        public OrderLineRepositorySQL(ComputerShopContext context)
        {
            this.context = context;
        }

        public void Create(Order_line item)
        {
            context.Order_line.Add(item);
        }

        public void Delete(int idOrder, int idSelf)
        {
            Order_line order_Line = context.Order_line.Find(idSelf, idOrder);
            if (order_Line != null)
                context.Order_line.Remove(order_Line);
        }

        public void Delete(int id)
        {
            return;
        }
        public Order_line GetItem(int id)
        {
            return null;
        }

        public Order_line GetItem(int idOrder, int idSelf)
        {
            return context.Order_line.Find(idSelf, idOrder);
        }

        public List<Order_line> GetList()
        {
            return context.Order_line.ToList();
        }

        public void Update(Order_line item)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
