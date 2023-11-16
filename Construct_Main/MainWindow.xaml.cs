using BLL;
using Construct_04;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Construct_Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel.MainViewModel VM; 
        public MainWindow()
        {
            InitializeComponent();

            VM = new ViewModel.MainViewModel(SideMenuBar, MainFrame, this);
            DataContext = VM;
        }

        private void DragRectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void SideMenuBar_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
