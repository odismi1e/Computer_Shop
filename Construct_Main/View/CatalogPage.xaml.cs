using BLL;
using Construct_Main.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
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
    /// Логика взаимодействия для CatalogPage.xaml
    /// </summary>
    public partial class CatalogPage : Page
    {
        private CatalogViewModel VM;
        public CatalogPage(List<ProductModel> products, List<CategoryModel> categories, MainViewModel mainWindow, OrderModel busket, List<ManufacturerModel> Manufacturers)
        {
            InitializeComponent();
            VM = new CatalogViewModel(products, categories, mainWindow, busket, Manufacturers);
            DataContext = VM;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            VM.ApplyFilter(SearchBox.Text);
        }

    }

    public class MyConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class NegateVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var realV = (Visibility)value;

            if (realV == Visibility.Hidden)
                return Visibility.Visible;
            else
                return Visibility.Hidden;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var realV = (Visibility)value;

            if (realV == Visibility.Hidden)
                return Visibility.Visible;
            else
                return Visibility.Hidden;
        }
    }

    public class BoolToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var realV = (bool)value;

            if (realV)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var realV = (Visibility)value;

            if (realV == Visibility.Collapsed)
                return false;
            else
                return true;
        }
    }

    public class NegateBoolToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var realV = (bool)value;

            if (!realV)
                return Visibility.Visible;
            else
                return Visibility.Hidden;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var realV = (Visibility)value;

            if (realV == Visibility.Visible)
                return false;
            else
                return true;
        }
    }

    public class IntToBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var realV = (int)value;

            if (realV < 1)
                return false ;
            else
                return true;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
