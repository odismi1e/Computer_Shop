using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SupplyRepositorySQL : IRepository<Supply>
    {
        private ComputerShopContext context;

        public SupplyRepositorySQL(ComputerShopContext context)
        {
            this.context = context;
        }

        public void Create(Supply item)
        {
            context.Supply.Add(item);
        }

        public void Delete(int id)
        {
            Supply cat = context.Supply.Find(id);
            if (cat != null)
                context.Supply.Remove(cat);
        }

        public Supply GetItem(int id)
        {
            return context.Supply.Find(id);
        }

        public List<Supply> GetList()
        {
            return context.Supply.ToList();
        }

        public void Update(Supply item)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
