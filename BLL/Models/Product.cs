using DAL;
using System;
using System.IO;
using System.Linq;

// ------------------------------------------------------------------------------
namespace BLL
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PriceString { get; set; }
        public int ManufId { get; set; }
        public string ManufacturerName { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public bool IsInBusket { get; set; }
        public ProductModel() { }

        public ProductModel(Product p)
        {
            Id = p.id;
            Name = p.name;
            Price = (int)p.price;
            PriceString = Price.ToString() + " Р.";
            ManufId = (int)p.Manufacturer.id;
            ManufacturerName = p.Manufacturer.name;
            CategoryId = (int)p.Category.id;
            Category = p.Category.name;
            Description = p.description;
            Count = (int)p.count;
            IsInBusket = false; 
            
        }
    }
}
