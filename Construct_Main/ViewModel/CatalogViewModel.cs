using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BLL;

namespace Construct_Main.ViewModel
{
    public class CatalogViewModel : INotifyPropertyChanged
    {
        private MainViewModel mainWindow;
        private OrderModel cart;

        private string searchRequest;
        private decimal lowPrice;
        private decimal topPrice;
        private int categoryId;
        private int prodId;
        private bool visibleFilter = true;

        #region Commands

        private RelayCommand applyfiltercommand;
        private RelayCommand addtobusket;
        private RelayCommand removeProduct;
        private RelayCommand toproductpage;
        private RelayCommand changeVisibiltyFilter;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand ApplyFilterCommand 
        { 
            get { return applyfiltercommand ?? (applyfiltercommand = new RelayCommand(obj => 
            {
                var values = (object[])obj;
                decimal low =  (decimal)values[0];
                decimal top = (decimal)values[1];
                int catid = values[2] == null ? -1 : (int)values[2];
                int prodid = values[3] == null ? -1 : (int)values[3];
                SetFilters(low, top, catid, prodid);
            }));
            }
        }

        public Visibility VisibleFilter
        {
            get
            {
                return ((visibleFilter) ? Visibility.Visible : Visibility.Hidden);
            }
        }

        public Visibility VisibleButtonForFilter
        {
            get
            {
                return ((!visibleFilter) ? Visibility.Visible : Visibility.Hidden);
            }
        }

        public RelayCommand AddToBusketCommand
        {
            get
            {
                return addtobusket ?? (addtobusket = new RelayCommand(obj =>
                {
                    AddProductToBusket(Int32.Parse((string)obj));
                }));
            }
        }        
        
        public RelayCommand ChangeVisibiltyFilter
        {
            get
            {
                return changeVisibiltyFilter ?? (changeVisibiltyFilter = new RelayCommand(obj =>
                {
                    visibleFilter = !visibleFilter;
                    NotifyPropertyChanged(nameof(VisibleFilter));
                    NotifyPropertyChanged(nameof(VisibleButtonForFilter));
                }));
            }
        }

        public RelayCommand ToProductPageCommand
        {
            get
            {
                return toproductpage ?? (toproductpage = new RelayCommand(obj =>
                {
                    ToProductPage(Int32.Parse((string)obj));
                }));
            }
        }

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
        #endregion

        public ObservableCollection<ProductModel> Products { get; set; }
        private List<ProductModel> productModels { get; set; }
        public List<CategoryModel> Categories { get; set; }
        public List<ManufacturerModel> Manufacturers { get; set; }
        public CatalogViewModel(List<ProductModel> productModels, List<CategoryModel> categories, MainViewModel mainWindow, OrderModel cart, List<ManufacturerModel> Manufacturers)
        {
            Products = new ObservableCollection<ProductModel>();

            this.productModels = productModels;

            SetProducts(productModels);
            Categories = categories;
            this.Manufacturers = Manufacturers;
            this.mainWindow = mainWindow;
            this.cart = cart;
        }

        private void SetProducts(List<ProductModel> pr)
        {
            Products.Clear();
            foreach (var item in pr)
                Products.Add(item);
        }
        public void SetFilters(decimal low, decimal top, int catId, int prodId)
        {
            lowPrice = low;
            topPrice = top;
            categoryId = catId;
            this.prodId = prodId;
            ApplyFilter(searchRequest);
        }
        public void ApplyFilter(string request)
        {
            request = request ?? "";
            foreach (char c in request)
                if (c == ' ')
                    request = request.Remove(0, 1);
                else
                    break;
            searchRequest = request;

            List<ProductModel> pr;

            if (request.Length != 0)
                pr = productModels.Where(i => i.Name.ToLower().Contains(request.ToLower())).ToList();
            else
                pr = productModels;

            if (lowPrice > 0)
                pr = pr.Where(i =>i.Price > lowPrice).ToList();
            if (topPrice > lowPrice)
                pr = pr.Where(i => i.Price < topPrice).ToList();
            if (categoryId > 0)
                pr = pr.Where(i => i.CategoryId == categoryId).ToList();
            if (prodId > 0)
                pr = pr.Where(i => i.ManufId == prodId).ToList();

            SetProducts(pr);

        }
        public void AddProductToBusket(int id)
        {
            ProductModel product = productModels.FirstOrDefault(i => i.Id == id);

            cart.ProductsIds.Add(id);
            cart.ProductCounts.Add(1);
            cart.TotalCost += product.Price;

            product.IsInBusket = true;

            int index = Products.IndexOf(product);
            Products.Remove(product);
            Products.Insert(index, product);
        }
        public void RemoveProduct(int id)
        {
            ProductModel product = productModels.FirstOrDefault(i => i.Id == id);

            for (int i = 0; i < cart.ProductsIds.Count; i++)
            {
                if (cart.ProductsIds[i] == id)
                {
                    cart.ProductsIds.RemoveAt(i);
                    cart.ProductCounts.RemoveAt(i);
                    cart.TotalCost -= product.Price;
                }
            }

            product.IsInBusket = false;

            int index = Products.IndexOf(product);
            Products.Remove(product);
            Products.Insert(index, product);
        }

        public void ToProductPage(int id)
        {
            mainWindow.ToProductPage(id);
        }
    }
}
