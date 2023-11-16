using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SellerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int ExperienceYears { get; set; }
        public decimal BaseWage { get; set; }
        public int CountOfOrders { get; set; }
        public SellerModel() { }
        public SellerModel(Seller c)
        {
            Id = c.id;
            Name = String.Join(" ", c.name, c.sername);
            ExperienceYears = (int)((DateTime.Now - StartDate).TotalDays / 365);
        }
    }
}

