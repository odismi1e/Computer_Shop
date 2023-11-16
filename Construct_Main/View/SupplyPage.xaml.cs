using BLL;
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
    /// Логика взаимодействия для SupplyPage.xaml
    /// </summary>
    public partial class SupplyPage : Page
    {
        private ViewModel.SupplyViewModel VM;
        public SupplyPage(IDbCrud dbCrud, ISupplyService supplyService, List<ProductModel> pr)
        {
            VM = new ViewModel.SupplyViewModel(dbCrud, supplyService, pr);
            DataContext = VM;
            InitializeComponent();
        }
    }
}
