using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SupplyLineModel
    {
        public int Id { get; set; }
        public int SupplyId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }

        public SupplyLineModel() { }

        public SupplyLineModel(SupplyLine s)
        {
            Id = s.id;
            SupplyId = s.Supply.id;
            Price = (decimal)s.price;
            Count = (int)s.count;
            ProductId = (int)s.Product.id;
        }
    }
}
