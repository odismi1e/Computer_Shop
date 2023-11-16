using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class CategoryRepositorySQL : IRepository<Category>
    {
        private ComputerShopContext context;

        public CategoryRepositorySQL(ComputerShopContext context)
        {
            this.context = context;
        }

        public void Create(Category item)
        {
            context.Category.Add(item);
        }

        public void Delete(int id)
        {
            Category cat = context.Category.Find(id);
            if (cat != null)
                context.Category.Remove(cat);
        }

        public Category GetItem(int id)
        {
            return context.Category.Find(id);
        }

        public List<Category> GetList()
        {
            return context.Category.ToList();
        }

        public void Update(Category item)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
