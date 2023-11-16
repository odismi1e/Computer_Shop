using System;

namespace DAL
{
    public class Report
    {
        public int CountOrders { get; set; }
        public int CountSuccessOrders { get; set; }
        public int CountProductsInOrders { get; set; }
        public int Money { get; set; }
    }
}
