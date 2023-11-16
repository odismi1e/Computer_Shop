
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class ProductRepositorySQL : IRepository<Product>
    {
        private ComputerShopContext context;

        public ProductRepositorySQL(ComputerShopContext context)
        {
            this.context = context;
        }

        public void Create(Product item)
        {
            context.Product.Add(item);
        }

        public void Delete(int id)
        {
            Product product = context.Product.Find(id);
            if (product != null)
                context.Product.Remove(product);
        }

        public Product GetItem(int id)
        {
            return context.Product.Find(id);
        }

        public List<Product> GetList()
        {
            return context.Product.ToList();
        }

        public void Update(Product item)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
