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
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        private OrderViewModel VM;
        public OrderPage(IAutorizationService autorizationService,IOrderService orderService, IDbCrud dbCrud, List<ProductModel> productModels)
        {
            InitializeComponent();
            VM = new OrderViewModel(autorizationService, orderService, dbCrud, productModels);
            DataContext = VM;
        }
    }
}
