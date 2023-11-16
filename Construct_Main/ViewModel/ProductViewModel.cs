using BLL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construct_Main.ViewModel
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private List<ProductModel> products;
        private int productId;
        private MainViewModel mainWindow;
        private OrderModel cart;
        
        public ProductModel productModel { get; set; }

        #region Commands

        private RelayCommand addtobusket;
        private RelayCommand removeProduct;
        private RelayCommand tocatalogpage;

        public RelayCommand AddToBusketCommand
        {
            get
            {
                return addtobusket ?? (addtobusket = new RelayCommand(obj =>
                {
                    AddProductToBusket((int)obj);
                }));
            }
        }

        public RelayCommand ToCatalogPageCommand
        {
            get
            {
                return tocatalogpage ?? (tocatalogpage = new RelayCommand(obj =>
                {
                    ToCatalogPage();
                }));
            }
        }

        public RelayCommand RemoveProductCommand
        {
            get
            {
                return removeProduct ?? (removeProduct = new RelayCommand(obj =>
                {
                    RemoveProduct((int)obj);
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

        private bool _isisbusket;
        public bool IsInBusket
        {
            get { return _isisbusket; }
            set
            {
                _isisbusket = value;
                NotifyPropertyChanged("IsInBusket");
            }
        }

        #endregion
        public ProductViewModel(List<ProductModel> products, int id, MainViewModel mainWindow, OrderModel cart)
        {
            this.products = products;
            this.cart = cart;
            productId = id;
            productModel = products.Where(i => i.Id == productId).FirstOrDefault();
            IsInBusket = productModel.IsInBusket;
            this.mainWindow = mainWindow;   
        }

        public void AddProductToBusket(int id)
        {
            //OrderModel or = db.GetAllOrders().Where(i => i.CustomerId == 0 && i.Status == 3).FirstOrDefault();
            //if (or != null)
            //{
            //    OrderLineModel o = new OrderLineModel
            //    {
            //        Count = 1,
            //        Id = -1,
            //        ProductId = id,
            //        OrderId = or.Id
            //    };

            //    db.AddOrderLine(o);
            //}
            //else
            //{
            //    int Oid = db.CreateBusket();
            //    OrderLineModel o = new OrderLineModel
            //    {
            //        Count = 1,
            //        Id = -1,
            //        ProductId = id,
            //        OrderId = Oid
            //    };

            //    db.AddOrderLine(o);
            //}


            cart.ProductsIds.Add(id);
            cart.ProductCounts.Add(1);
            cart.TotalCost += products.Where(i => i.Id == id).FirstOrDefault().Price;

            products.Where(i => i.Id == id).FirstOrDefault().IsInBusket = true;
            IsInBusket = true;
        }
        public void RemoveProduct(int id)
        {
            //OrderModel or = db.GetAllOrders().Where(i => i.CustomerId == 0 && i.Status == 3).FirstOrDefault();
            //db.DeleteOrderLine(or.Id, id);

            for (int i = 0; i < cart.ProductsIds.Count; i++)
            {
                if (cart.ProductsIds[i] == id)
                {
                    cart.ProductsIds.RemoveAt(i);
                    cart.ProductCounts.RemoveAt(i);
                    cart.TotalCost -= products.Where(a => a.Id == id).FirstOrDefault().Price;
                }
            }

            products.Where(i => i.Id == id).FirstOrDefault().IsInBusket = false;
            IsInBusket = false;
        }
        public void ToCatalogPage()
        {
            mainWindow.ToCatalogPage();
        }
    }
}
