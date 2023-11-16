using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class SellerRepositorySQL : IRepository<Seller>
    {
        private ComputerShopContext context;

        public SellerRepositorySQL(ComputerShopContext context)
        {
            this.context = context;
        }

        public void Create(Seller item)
        {
            context.Seller.Add(item);
        }

        public void Delete(int id)
        {
            Seller sel = context.Seller.Find(id);
            if (sel != null)
                context.Seller.Remove(sel);
        }

        public Seller GetItem(int id)
        {
            return context.Seller.Find(id);
        }

        public List<Seller> GetList()
        {
            return context.Seller.ToList();
        }

        public void Update(Seller item)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
