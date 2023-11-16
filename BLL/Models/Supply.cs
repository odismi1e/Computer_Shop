using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class SupplyModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalCost { get; set; }
        public List<int> ProductsIds { get; set; }
        public List<int> ProductCounts { get; set; }
        public List<decimal> ProductPrices { get; set; }
        public SupplyModel() { }

        public SupplyModel(Supply s)
        {
            Id = s.id;
            Date = (DateTime)s.date;
            TotalCost = (decimal)s.total_cost;

            ProductsIds = s.SupplyLine.Select(i => i.Product.id).ToList();
            ProductCounts = s.SupplyLine.Select(i => (int)i.count).ToList();
            ProductPrices = s.SupplyLine.Select(i => (decimal)i.price).ToList();
        }
    }
}
