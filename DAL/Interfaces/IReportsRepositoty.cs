using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IReportsRepositoty
    {
        Report GetReport(DateTime from, DateTime to);
    }
}
