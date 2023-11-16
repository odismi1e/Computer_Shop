using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class OrderLineModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }

        public OrderLineModel() { }

        public OrderLineModel(Order_line o)
        {
            Id = o.id;
            OrderId = o.OrderC.id;
            ProductId = (int)o.Product.id;
            Count = (int)o.count;
        }
    }
}
