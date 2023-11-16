using DAL;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace BLL
{
    public class DBDataOperation : IDbCrud
    {
        private IDbRepos context;

        public DBDataOperation(IDbRepos repos)
        {
            context = repos;
        }

        public List<ProductModel> GetAllProduct()
        {
            return context.Products.GetList().Select(i => new ProductModel(i)).ToList();
        }
        public List<OrderModel> GetAllOrders()
        {
            return context.Orders.GetList().Select(i => new OrderModel(i)).ToList();
        }
        public List<ManufacturerModel> GetAllManufacturers()
        {
            return context.Manufacturers.GetList().Select(i => new ManufacturerModel(i)).ToList();
        }
        public List<CategoryModel> GetAllCategories()
        {
            return context.Categories.GetList().Select(i => new CategoryModel(i)).ToList();
        }
        public List<CustomerModel> GetAllCustomers()
        {
            return context.Customers.GetList().Select(i => new CustomerModel(i)).ToList();
        }
        public List<SellerModel> GetAllSellers()
        {
            return context.Sellers.GetList().Select(i => new SellerModel(i)).ToList();
        }
        public ProductModel GetProduct(int id)
        {
            return new ProductModel(context.Products.GetItem(id));
        }
        public int CreateProduct(ProductModel p)
        {
            Manufacturer m = context.Manufacturers.GetList().Where(i => i.id == p.ManufId).FirstOrDefault();
            Category c = context.Categories.GetList().Where(i => i.id == p.CategoryId).FirstOrDefault();

            Product newProduct = new Product { price = p.Price, description = p.Description, name = p.Name, count = p.Count, Manufacturer = m, Category = c };
            context.Products.Create(newProduct);
            Save();
            return newProduct.id;
        }
        public void UpdateProduct(ProductModel p)
        {
            Product pr = context.Products.GetItem(p.Id);
            pr.id = p.Id;
            pr.price = p.Price;
            pr.description = p.Description;
            pr.name = p.Name;
            pr.count = p.Count;

            Manufacturer m = context.Manufacturers.GetList().Where(i => i.id == p.ManufId).FirstOrDefault();
            Category c = context.Categories.GetList().Where(i => i.id == p.CategoryId).FirstOrDefault();

            pr.Manufacturer = m;
            pr.Category = c;

            Save();
        }
        public void DeleteProduct(int id)
        {
            Product p = context.Products.GetItem(id);
            if (p != null)
            {
                context.Products.Delete(id);
                Save();
            }
        }
        public void DeleteOrder(int id)
        {
            OrderC o = context.Orders.GetItem(id);
            if (o != null)
            {
                context.Orders.Delete(id);
                Save();
            }
        }
        public void DeleteOrderLine(int orderId, int productId)
        {
            Order_line o = context.OrderLines.GetList().Where(i => i.OrderC.id == orderId && i.Product.id == productId).FirstOrDefault();
            if (o != null)
            {
                o.OrderC.total_cost -= (int?)(o.count * o.Product.price);
                ((OrderLineRepositorySQL)context.OrderLines).Delete(orderId, o.id);
                Save();
            }
        }

        public void UpdateOrderLine(OrderLineModel o)
        {
            Order_line ol = ((OrderLineRepositorySQL)context.OrderLines).GetItem(o.OrderId, o.Id);
            ol.OrderC.total_cost += (int?)((o.Count - ol.count) * ol.Product.price);
            ol.count = o.Count;

            Save();
        }

        public OrderModel CreateBusket()
        {
            int id = new Random().Next();

            OrderModel model = new OrderModel
            {
                Id = id,
                CustomerId = -1,
                Date = DateTime.Now,
                ProductCounts = new List<int>(),
                Products = "",
                ProductsIds = new List<int>(),
                Status = 3,
                StatusName = ""/*context.Statuses.GetItem(3).name*/,
                TotalCost = 0
            };
            return model;
        }
        public bool AddOrderLine(OrderLineModel o)
        {
            Order_line orl = context.Orders.GetItem(o.OrderId).Order_line.Where(i => i.Product.id == o.ProductId).FirstOrDefault();

            if (orl == null)
            {
                int id;
                if (o.Id != -1)
                    id = o.Id;
                else
                {
                    var ordline = context.OrderLines.GetList().Where(i => i.OrderC.id == o.OrderId).OrderByDescending(i => i.id).FirstOrDefault();
                    id = ordline == null ? 0 : ordline.id + 1;
                }

                Order_line ol = new Order_line
                {
                    id = id,
                    count = o.Count,
                    OrderC = context.Orders.GetItem(o.OrderId),
                    Product = context.Products.GetItem(o.ProductId),
                    cost = ""
                };
                ol.OrderC.total_cost += ol.count * (int?)ol.Product.price;

                context.OrderLines.Create(ol);
                Save();
                return false;
            }
            else
            {
                o.Id = orl.id;
                o.Count = (int)orl.count + o.Count;
                if (o.Count > 0)
                {
                    if (o.Count <= orl.Product.count)
                        UpdateOrderLine(o);
                    return false;
                }
                else
                {
                    DeleteOrderLine(o.OrderId, o.ProductId);
                    return true;
                }
            }
        }

        public bool Save()
        {
            if (context.Save() > 0) return true;
            return false;
        }


    }
}
