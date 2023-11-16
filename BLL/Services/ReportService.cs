using System;
using System.Linq;
using System.Collections.Generic;
using DAL;

namespace BLL
{
    public class ReportService : IReportService
    {
        private IDbRepos context;
        public ReportService(IDbRepos repos)
        {
            context = repos;
        }

        public ReportModel getReport(DateTime from, DateTime to)
        {
            ReportModel reportModel = new ReportModel();
            Report report = context.Reports.GetReport(from, to);
            reportModel.CountSuccessOrders = report.CountSuccessOrders;
            reportModel.CountProductsInOrders = report.CountProductsInOrders;
            reportModel.CountOrders = report.CountOrders;
            reportModel.Money = report.Money;
            return reportModel;
        }
    }
}
