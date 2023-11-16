using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SupplyLineRepositorySQL : IRepository<SupplyLine>
    {
        private ComputerShopContext context;

        public SupplyLineRepositorySQL(ComputerShopContext context)
        {
            this.context = context;
        }

        public void Create(SupplyLine item)
        {
            context.SupplyLine.Add(item);
        }

        public void Delete(int id)
        {
            SupplyLine cat = context.SupplyLine.Find(id);
            if (cat != null)
                context.SupplyLine.Remove(cat);
        }

        public SupplyLine GetItem(int id)
        {
            return context.SupplyLine.Find(id);
        }

        public List<SupplyLine> GetList()
        {
            return context.SupplyLine.ToList();
        }

        public void Update(SupplyLine item)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
