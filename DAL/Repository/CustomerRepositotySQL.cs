using System.Collections.Generic;
using System.Linq;


namespace DAL
{
    public class CustomerRepositotySQL : IRepository<Customer>
    {
        private ComputerShopContext context;

        public CustomerRepositotySQL(ComputerShopContext context)
        {
            this.context = context;
        }

        public void Create(Customer item)
        {
            context.Customer.Add(item);
        }

        public void Delete(int id)
        {
            Customer cust = context.Customer.Find(id);
            if (cust != null)
                context.Customer.Remove(cust);
        }

        public Customer GetItem(int id)
        {
            return context.Customer.Find(id);
        }

        public List<Customer> GetList()
        {
            return context.Customer.ToList();
        }

        public void Update(Customer item)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
