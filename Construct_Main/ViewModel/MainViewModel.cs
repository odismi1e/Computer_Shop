using BLL;
using Construct_04;
using Construct_Main.Components;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Construct_Main.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private IDbCrud crudServ;
        private IOrderService orderServ;
        private IReportService reportServ;
        private IAutorizationService authServ;
        private ISupplyService supplyService;

        private Frame MainFrame;
        private SideMenu SideMenuBar;
        private MainWindow MainWindow;

        private List<ProductModel> pr;
        private List<CategoryModel> cat;
        private List<ManufacturerModel> Manufacturer;

        private OrderModel Busket;

        #region Commands

        private RelayCommand navigate;
        public RelayCommand NavigateCommand
        {
            get
            {
                return navigate ?? (navigate = new RelayCommand(obj =>
                {
                    var real = (string)obj;
                    switch(real)
                    {
                        case "Catalog":
                            NavigatetoToCatalogPage();
                            break;
                        case "Auth":
                            NavigatetoToAuthPage();
                            break;
                        case "Busket":
                            NavigatetoToBusketPage();
                            break;
                        case "Order":
                            NavigatetoToOrderPage();
                            break;
                        case "Report":
                            NavigatetoToReportPage();
                            break;
                        case "Supply":
                            NavigatetoToSupplyPage();
                            break;
                    }
                }));
            }
        }

        private RelayCommand sidemenu;
        public RelayCommand SideMenuCommand
        {
            get
            {
                return sidemenu ?? (sidemenu = new RelayCommand(obj =>
                {
                    SideMenu();
                }));
            }
        }

        private RelayCommand sidemenuclose;
        public RelayCommand SideMenuCloseCommand
        {
            get
            {
                return sidemenuclose ?? (sidemenuclose = new RelayCommand(obj =>
                {
                    SideMenuClose();
                }));
            }
        }

        private RelayCommand close;
        public RelayCommand CloseCommand
        {
            get
            {
                return close ?? (close = new RelayCommand(obj =>
                {
                    CloseWindow();
                }));
            }
        }

        private RelayCommand maxmin;
        public RelayCommand MaxMinCommand
        {
            get
            {
                return maxmin ?? (maxmin = new RelayCommand(obj =>
                {
                    MaxMinWindow();
                }));
            }
        }

        #endregion

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private Visibility _sellerVis;
        public Visibility SellerVis
        {
            get { return _sellerVis = authServ.GetCurrentUser().type == UserType.Seller ? Visibility.Visible : Visibility.Collapsed; }
            set
            {
                _sellerVis = value;
                NotifyPropertyChanged("SellerVis");
            }
        }

        private Visibility _customerVis;
        public Visibility CustomerVis
        {
            get { return _customerVis = authServ.GetCurrentUser().type == UserType.Customer ? Visibility.Visible : Visibility.Collapsed; }
            set
            {
                _customerVis = value;
                NotifyPropertyChanged("CustomerVis");
            }
        }

        private Visibility _notSel;
        public Visibility NotSel
        {
            get { return _notSel = authServ.GetCurrentUser().type != UserType.Seller ? Visibility.Visible : Visibility.Collapsed; }
            set
            {
                _notSel = value;
                NotifyPropertyChanged("NotSel");
            }
        }

        public void UpdateAuth()
        {
            NotifyPropertyChanged("SellerVis");
            NotifyPropertyChanged("CustomerVis");
            NotifyPropertyChanged("NotSel");
        }

        #endregion

        public MainViewModel(SideMenu sideMenu, Frame mainFraim, MainWindow mainWindow)
        {
            SideMenuBar = sideMenu;
            MainFrame = mainFraim;

            var kernel = new StandardKernel(new NinjectRegistrations(), new ServiceModule("ComputerShopContext"));

            crudServ = kernel.Get<IDbCrud>();
            orderServ = kernel.Get<IOrderService>();
            reportServ = kernel.Get<IReportService>();
            authServ = kernel.Get<IAutorizationService>();
            supplyService = kernel.Get<ISupplyService>();

            Busket = crudServ.CreateBusket();

            pr = crudServ.GetAllProduct();
            cat = crudServ.GetAllCategories();
            Manufacturer = crudServ.GetAllManufacturers();

            MainFrame.Navigate(new View.CatalogPage(pr, cat, this, Busket, Manufacturer));
            SideMenuBar.DataContext = this;
            MainWindow = mainWindow;
        }

        private void NavigatetoToReportPage()
        {
            MainFrame.Navigate(new View.ReportPage(reportServ));
            SideMenuBar.CloseSide();
        }

        private void CheckForProductUpdate()
        {
            var pp = pr.Where(i => i.IsInBusket).Select(i => i.Id).ToList();

            pr = crudServ.GetAllProduct();
            foreach (var p in pp)
            {
                pr.Where(i => i.Id == p).FirstOrDefault().IsInBusket = true;
            }
        }
        private void NavigatetoToSupplyPage()
        {
            Busket = crudServ.CreateBusket();
            pr = crudServ.GetAllProduct();
            MainFrame.Navigate(new View.SupplyPage(crudServ, supplyService, pr));
            SideMenuBar.CloseSide();
        }

        private void NavigatetoToAuthPage()
        {
            MainFrame.Navigate(new View.AuthPage(authServ, this));
            SideMenuBar.CloseSide();
        }

        private void NavigatetoToOrderPage()
        {
            MainFrame.Navigate(new View.OrderPage(authServ, orderServ, crudServ, pr));
            SideMenuBar.CloseSide();
        }

        private void NavigatetoToBusketPage()
        {
            MainFrame.Navigate(new View.BusketPage(crudServ, authServ, pr, orderServ, Busket));
            SideMenuBar.CloseSide();
        }

        private void NavigatetoToCatalogPage()
        {
            CheckForProductUpdate();
            
            MainFrame.Navigate(new View.CatalogPage(pr, cat, this, Busket, Manufacturer));
            SideMenuBar.CloseSide();
        }

        private void CloseWindow()
        {
            MainWindow.Close();
        }
        private void SideMenu()
        {
            SideMenuBar.OpenSide();      
        }

        private void SideMenuClose()
        {
            SideMenuBar.CloseSide();
        }

        public void ToProductPage(int id)
        {
            MainFrame.Navigate(new View.ProductPage(pr, id, this, Busket));
        }

        public void ToCatalogPage()
        {
            NavigatetoToCatalogPage();
        }

        private void MaxMinWindow()
        {
            if (MainWindow.WindowState == WindowState.Maximized)
                MainWindow.WindowState = WindowState.Normal;
            else
                MainWindow.WindowState = WindowState.Maximized;
        }
    }
}
