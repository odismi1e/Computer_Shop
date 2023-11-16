using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construct_Main.ViewModel
{
    public class ReportViewModel : INotifyPropertyChanged
    {
        private IReportService reportService;

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _countOrders = 0;
        public int CountOrders
        {
            get { return _countOrders; }
            set
            {
                _countOrders = value;
                NotifyPropertyChanged("CountOrders");
            }
        }

        private int _CountSuccessOrders = 0;
        public int CountSuccessOrders
        {
            get { return _CountSuccessOrders; }
            set
            {
                _CountSuccessOrders = value;
                NotifyPropertyChanged("CountSuccessOrders");
            }
        }

        private int _CountProducts = 0;
        public int CountProducts
        {
            get { return _CountProducts; }
            set
            {
                _CountProducts = value;
                NotifyPropertyChanged("CountProducts");
            }
        }

        private int _Money = 0;
        public int Money
        {
            get { return _Money; }
            set
            {
                _Money = value;
                NotifyPropertyChanged("Money");
            }
        }

        #endregion

        #region Commands
        private RelayCommand _CalculateReport;
        public RelayCommand CalculateReport
        {
            get
            {
                return _CalculateReport ?? (_CalculateReport = new RelayCommand(obj =>
                {
                    var values = (object[])obj;
                    DateTime from = (DateTime)values[0];
                    DateTime to = (DateTime)values[1];
                    RecalculateData(from, to);
                }));
            }
        }
        #endregion

        public ReportViewModel(IReportService reportService)
        {
            this.reportService = reportService;
        }

        public void RecalculateData(DateTime from, DateTime to)
        {
            if (from != null && to != null && to > from)
            {
                var report = reportService.getReport(from, to);
                Money = report.Money;
                CountProducts = report.CountProductsInOrders;
                CountSuccessOrders = report.CountSuccessOrders;
                CountOrders = report.CountOrders;
            }
            else
            {
                Money = 0;
                CountProducts = 0;
                CountSuccessOrders = 0;
                CountOrders = 0;
            }
        }
    }
}
