using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class ReportRepositorySQL : IReportsRepositoty
    {
        private ComputerShopContext context;

        public ReportRepositorySQL(ComputerShopContext context)
        {
            this.context = context;
        }

        public Report GetReport(DateTime from, DateTime to)
        {
            Report report = new Report();
            report.CountProductsInOrders = context.OrderC.ToList().Where(i => i.date >= from && i.date <= to)
                .Join(context.Order_line.ToList(), i => i.id, i => i.OrderC.id, (i, j) => j.count)
                .Sum().GetValueOrDefault(0);
            report.CountOrders = context.OrderC.ToList().Where(i => i.date >= from && i.date <= to).Count();
            report.Money = context.OrderC.ToList().Where(i => i.date >= from && i.date <= to)
                .Where(i => i.OrderStatus.id == 2)
                .Sum(i => i.total_cost).GetValueOrDefault(0);
            report.CountSuccessOrders = context.OrderC.ToList().Where(i => i.date >= from && i.date <= to)
                .Where(i => i.OrderStatus.id == 2)
                .Count();
            return report;
        }
    }
}
