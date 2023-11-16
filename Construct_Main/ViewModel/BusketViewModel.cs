using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using BLL;

namespace Construct_Main.ViewModel
{
    public struct BusketLine
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PriceString { get; set; }
        public int ProductCount { get; set; }
        public string CountString { get; set; }
        public string Category { get; set; }
    }
    public class BusketViewModel : INotifyPropertyChanged
    {
        private IDbCrud db;
        private List<ProductModel> products;
        private IAutorizationService authService;
        private IOrderService orderService;
        private OrderModel cart;
        public ObservableCollection<BusketLine> BusketLines { get; set; }

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private decimal _priceText = 0;
        public string PriceText
        {
            get { return _priceText.ToString() + " Руб."; }
            set
            {
                _priceText = Decimal.Parse(value);
                NotifyPropertyChanged("PriceText");
            }
        }
        #endregion

        #region Commands

        private RelayCommand makeOrder;
        public RelayCommand MakeOrderCommand
        {
            get
            {
                return makeOrder ?? (makeOrder = new RelayCommand(obj =>
                {
                    MakeOrder();
                }));
            }
        }

        private RelayCommand removeProduct;
        public RelayCommand RemoveProductCommand
        {
            get
            {
                return removeProduct ?? (removeProduct = new RelayCommand(obj =>
                {
                    RemoveProduct(Int32.Parse((string)obj));
                }));
            }
        }

        private RelayCommand addCount;
        public RelayCommand AddCountCommand
        {
            get
            {
                return addCount ?? (addCount = new RelayCommand(obj =>
                {
                    AddCount(Int32.Parse((string)obj));
                }));
            }
        }

        private RelayCommand subCount;
        public RelayCommand SubCountCommand
        {
            get
            {
                return subCount ?? (subCount = new RelayCommand(obj =>
                {
                    SubCount(Int32.Parse((string)obj));
                }));
            }
        }
        #endregion
        public BusketViewModel(IDbCrud dbCrud, IAutorizationService authService, List<ProductModel> products, IOrderService orderService, OrderModel busket)
        {
            db = dbCrud;
            cart = busket;
            PriceText = cart == null ? "0" : cart.TotalCost.ToString();
            BusketLines = new ObservableCollection<BusketLine>();
            SetBusket();
            this.authService = authService;
            this.products = products;
            this.orderService = orderService;
        }

        public void UpdateBusket()
        {
            PriceText = cart.TotalCost.ToString();
        }

        public void SetBusket()
        {
            BusketLines.Clear();
            if (cart != null)
                for(int i = 0; i < cart.ProductsIds.Count; ++i)
                {
                    ProductModel p = db.GetProduct(cart.ProductsIds[i]);
                    string name = p.Name;
                    string price = p.Price.ToString();
                    BusketLines.Add(new BusketLine() 
                    { 
                        ProductId = cart.ProductsIds[i], 
                        ProductCount = cart.ProductCounts[i], 
                        ProductName = name, 
                        PriceString = price, 
                        CountString = cart.ProductCounts[i].ToString(),
                        Category = p.Category
                    });
                }
        }
        
        public void RemoveProduct(int id)
        {
            for (int i = 0; i < cart.ProductsIds.Count; ++i)
            {
                if (cart.ProductsIds[i] == id)
                {
                    cart.TotalCost -= db.GetProduct(cart.ProductsIds[i]).Price * cart.ProductCounts[i];
                    cart.ProductCounts.RemoveAt(i);
                    cart.ProductsIds.RemoveAt(i);
                    products.Where(a => a.Id == id).FirstOrDefault().IsInBusket = false;
                }
            }

            UpdateBusket();
            SetBusket();
        }

        public void AddCount(int id)
        {
            for (int i = 0; i < cart.ProductsIds.Count; ++i)
            {
                if (cart.ProductsIds[i] == id && products.Where(a => a.Id == id).FirstOrDefault().Count > cart.ProductCounts[i])
                {
                    cart.ProductCounts[i]++;
                    cart.TotalCost += products.Where(a => a.Id == id).FirstOrDefault().Price;
                    break;
                }
            }

            UpdateBusket();
            SetBusket();
        }

        public void SubCount(int id)
        {
            for (int i = 0; i < cart.ProductsIds.Count; ++i)
            {
                if (cart.ProductsIds[i] == id)
                {
                    cart.ProductCounts[i]--;
                    cart.TotalCost -= db.GetProduct(cart.ProductsIds[i]).Price;
                }

                if (cart.ProductCounts[i] < 1)
                {
                    cart.ProductCounts.RemoveAt(i);
                    cart.ProductsIds.RemoveAt(i);
                    products.Where(a => a.Id == id).FirstOrDefault().IsInBusket = false;
                }
            }

            UpdateBusket();
            SetBusket();
        }

        public void MakeOrder()
        {
            if (cart != null)
            {
                if (authService.GetCurrentUser().type == UserType.Customer)
                {
                    foreach (var p in BusketLines)
                        products.Where(i => i.Id == p.ProductId).FirstOrDefault().IsInBusket = false;

                    orderService.MakeOrderFromModel(cart, authService);

                    cart.ProductCounts.Clear();
                    cart.ProductsIds.Clear();
                    cart.TotalCost = 0;
                    cart.Id = db.CreateBusket().Id;

                    UpdateBusket();
                    SetBusket();

                }
                else
                {
                    var msgbx = new Windows.CustomMessageBox("Войдите в систему,\nчтобы сделать заказ", "Ошибка заказа");
                    msgbx.ShowDialog();
                }
            }
        }
    }
}
