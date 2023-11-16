using System;
using System.Collections.Generic;
using DAL;
using System.Linq;
using System.Web;

namespace BLL
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime? Date { get; set; }
        public decimal TotalCost { get; set; }
        public string Products { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public List<int> ProductsIds { get; set; }
        public List<int> ProductCounts { get; set; }

        public OrderModel() { }

        public OrderModel(OrderC o)
        {
            Id = o.id;
            CustomerId = (int)o.Customer.id;
            Date = o.date;
            Status = (int)o.OrderStatus.id;
            StatusName = o.OrderStatus.name;
            Products = String.Join(" , ", o.Order_line.Select(i => i.Product.name));
            ProductsIds = o.Order_line.Select(i => i.Product.id).ToList();
            ProductCounts = o.Order_line.Select(i => (int)i.count).ToList();
            TotalCost = (int)o.total_cost;

        }
    }
}
