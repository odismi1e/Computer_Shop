using System;

namespace BLL
{

    public class ReportModel
    {
        public int CountOrders { get; set; }
        public int CountSuccessOrders { get; set; }
        public int CountProductsInOrders { get; set; }
        public int Money { get; set; }
    }
}
