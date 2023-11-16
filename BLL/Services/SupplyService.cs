using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SupplyService : ISupplyService
    {
        private IDbRepos context;

        public SupplyService(IDbRepos repos)
        {
            context = repos;
        }

        public bool MakeSupply(SupplyModel supplyDto)
        {
            List<SupplyLine> suppliedProducts = new List<SupplyLine>();
            int lineid = 0;

            int id;
            if (supplyDto.Id != -1)
                id = supplyDto.Id;
            else
                id = context.Supplies.GetList().OrderByDescending(i => i.id).FirstOrDefault() == null ? 0 : context.Supplies.GetList().OrderByDescending(i => i.id).FirstOrDefault().id + 1;

            Supply r = new Supply
            {
                id = id,
                date = DateTime.Now,
                total_cost = supplyDto.TotalCost,
                SupplyLine = suppliedProducts
            };

            for (int i = 0; i < supplyDto.ProductsIds.Count; ++i)
            {
                Product p = context.Products.GetItem(supplyDto.ProductsIds[i]);

                if (p == null)
                    throw new Exception("Продукт не найден");
                suppliedProducts.Add(new SupplyLine { id = lineid++, Product = p, count = supplyDto.ProductCounts[i], Supply = r, price = supplyDto.ProductPrices[i] });

                p.count += supplyDto.ProductCounts[i];

                context.Products.Update(p);
            }

            context.Supplies.Create(r);
            if (context.Save() > 0)
                return true;
            return false;
        }
    }
}
