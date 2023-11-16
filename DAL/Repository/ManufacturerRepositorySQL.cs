using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class ManufacturerRepositorySQL : IRepository<Manufacturer>
    {
        private ComputerShopContext context;

        public ManufacturerRepositorySQL(ComputerShopContext context)
        {
            this.context = context;
        }

        public void Create(Manufacturer item)
        {
            context.Manufacturer.Add(item);
        }

        public void Delete(int id)
        {
            Manufacturer manuf = context.Manufacturer.Find(id);
            if (manuf != null)
                context.Manufacturer.Remove(manuf);
        }

        public Manufacturer GetItem(int id)
        {
            return context.Manufacturer.Find(id);
        }

        public List<Manufacturer> GetList()
        {
            return context.Manufacturer.ToList();
        }

        public void Update(Manufacturer item)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
