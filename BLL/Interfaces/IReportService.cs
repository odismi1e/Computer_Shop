using System;
using System.Collections.Generic;

namespace BLL
{
    public interface IReportService
    {
        ReportModel getReport(DateTime from, DateTime to);
    }
}
