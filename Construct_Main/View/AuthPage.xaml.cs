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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private AuthViewModel VM;
        public AuthPage(IAutorizationService db, MainViewModel mw)
        {
            InitializeComponent();
            VM = new AuthViewModel(db, mw);
            DataContext = VM;
        }
    }
}
