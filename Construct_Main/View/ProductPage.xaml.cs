using BLL;
using Construct_Main.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Construct_Main.View
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        private ProductViewModel VM;
        public ProductPage(List<ProductModel> products, int id, MainViewModel mainWindow, OrderModel busket)
        {
            InitializeComponent();
            VM = new ProductViewModel(products, id, mainWindow, busket);
            DataContext = VM;
        }

    }
}
