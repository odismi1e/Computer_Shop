using BLL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Construct_Main.ViewModel
{
    public struct ProductLine
    {
        public string Name { get; set; }
        public string CategoryImageSource { get; set; }
        public int CountInOrder { get; set; }
    }
    public struct OrderLine
    {
        public int Id { get; set; }
        public string CustomerInfo { get; set; }
        public decimal TotalCost { get; set; }
        public string StatusName { get; set; }
        public string TotalCount { get; set; }
        public Visibility OrderedVisibility { get; set; }
        public Visibility VerifiedVisibility { get; set; }
        public Visibility CompleteVisibility { get; set; }
        public Visibility CancelVisibility { get; set; }
        public List<ProductLine> Products { get; set; }
    }

    public class OrderViewModel : INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _isCustomer;
        private bool _isSeller;
        private bool _isUnauth;
        public bool IsCustomer
        {
            get { return _isCustomer; }
            set
            {
                _isCustomer = value;
                NotifyPropertyChanged("IsCutomer");
            }
        }

        public bool IsSeller
        {
            get { return _isSeller; }
            set
            {
                _isSeller = value;
                NotifyPropertyChanged("IsSeller");
            }
        }

        public bool IsUnauth
        {
            get { return _isUnauth; }
            set
            {
                _isUnauth = value;
                NotifyPropertyChanged("IsSeller");
            }
        }

        #endregion

        #region Commands

        private RelayCommand cancelOrder;
        private RelayCommand takeOrder;
        private RelayCommand verifyOrder;
        public RelayCommand CancelOrderCommand
        {
            get
            {
                return cancelOrder ?? (cancelOrder = new RelayCommand(obj =>
                {
                    CancelOrder(Int32.Parse((string)obj));
                }));
            }
        }

        public RelayCommand TakeOrderCommand
        {
            get
            {
                return takeOrder ?? (takeOrder = new RelayCommand(obj =>
                {
                    TakeOrder(Int32.Parse((string)obj));
                }));
            }
        }

        public RelayCommand VerifyOrderCommand
        {
            get
            {
                return verifyOrder ?? (verifyOrder = new RelayCommand(obj =>
                {
                    VerifyOrder(Int32.Parse((string)obj));
                }));
            }
        }

        #endregion

        private IAutorizationService authService;
        private IOrderService orderService;
        private IDbCrud db;
        private List<ProductModel> _products;
        public ObservableCollection<OrderLine> Orders { get; set; }


        public OrderViewModel(IAutorizationService authService, IOrderService orderService, IDbCrud db, List<ProductModel> products)
        {
            this.authService = authService;
            IsCustomer = authService.GetCurrentUser().type == UserType.Customer;
            IsSeller = authService.GetCurrentUser().type == UserType.Seller;
            IsUnauth = authService.GetCurrentUser().type == UserType.Unauthorized;

            this.db = db;
            _products = products;

            Orders = new ObservableCollection<OrderLine>();
            SetOrders(this.db.GetAllOrders());
            this.orderService = orderService;
        }
        private void SetOrders(List<OrderModel> or)
        {
            if (authService.GetCurrentUser().type == UserType.Seller)
                SetOrdersForSeller(or);
            else if (authService.GetCurrentUser().type == UserType.Customer)
                SetOrdersForCustomer(or);
        }
        private void SetOrdersForSeller(List<OrderModel> or)
        {
            Orders.Clear();
            foreach (var item in or)
            {
                    List<ProductLine> pr = new List<ProductLine>();

                    for (int i = 0; i < item.ProductsIds.Count; ++i)
                    {
                        ProductModel p = _products.Where(a => a.Id == item.ProductsIds[i]).FirstOrDefault();
                        pr.Add(new ProductLine
                        {
                            Name = p.Name,
                            CategoryImageSource = p.Category,
                            CountInOrder = item.ProductCounts[i]
                        });
                    }

                    var c = db.GetAllCustomers().Where(i => i.Id == item.CustomerId).FirstOrDefault();

                    Orders.Add(new OrderLine
                    {
                        Id = item.Id,
                        StatusName = item.StatusName,
                        TotalCost = item.TotalCost,
                        Products = pr,
                        TotalCount = "Товаров " + item.ProductCounts.Sum().ToString() + " шт.",
                        CancelVisibility = item.Status != 2 && item.Status != 3 ? Visibility.Visible : Visibility.Collapsed,
                        CompleteVisibility = item.Status == 2 ? Visibility.Visible : Visibility.Collapsed,
                        OrderedVisibility = item.Status == 1 ? Visibility.Visible : Visibility.Collapsed,
                        VerifiedVisibility = Visibility.Collapsed,
                        CustomerInfo = c.Name + " " + item.Date
                    });
            }
        }
        private void SetOrdersForCustomer(List<OrderModel> or)
        {
            Orders.Clear();
            foreach (var item in or)
            {
                if (item.CustomerId == authService.GetCurrentUser().id)
                {
                    List<ProductLine> pr = new List<ProductLine>();

                    for (int i = 0; i < item.ProductsIds.Count; ++i)
                    {
                        ProductModel p = _products.Where(a => a.Id == item.ProductsIds[i]).FirstOrDefault();
                        pr.Add(new ProductLine
                        {
                            Name = p.Name,
                            CategoryImageSource = p.Category,
                            CountInOrder = item.ProductCounts[i]
                        });
                    }

                    var c = db.GetAllCustomers().Where(i => i.Id == item.CustomerId).FirstOrDefault();

                    Orders.Add(new OrderLine
                    {
                        Id = item.Id,
                        StatusName = item.StatusName,
                        TotalCost = item.TotalCost,
                        Products = pr,
                        TotalCount = "Товаров " + item.ProductCounts.Sum().ToString() + " шт.",
                        CancelVisibility = item.Status != 2 && item.Status != 3 ? Visibility.Visible : Visibility.Collapsed,
                        CompleteVisibility = item.Status == 2 ? Visibility.Visible : Visibility.Collapsed,
                        OrderedVisibility = item.Status == 1 ? Visibility.Visible : Visibility.Collapsed,
                        VerifiedVisibility = Visibility.Collapsed,
                        CustomerInfo = c.Name
                    });
                }
            }
        }
        private void CancelOrder(int id)
        {
            orderService.CancelOrder(id, authService);
            SetOrders(this.db.GetAllOrders());

            _products = db.GetAllProduct();
        }

        private void TakeOrder(int id)
        {
            if (IsCustomer)
            {
                orderService.TakeOrder(id, authService);
                SetOrders(this.db.GetAllOrders());
            }
        }

        private void VerifyOrder(int id)
        {
            if (IsSeller)
            {
                orderService.VerifyOrder(id, authService);
                SetOrders(this.db.GetAllOrders());
            }
        }

    }
}


