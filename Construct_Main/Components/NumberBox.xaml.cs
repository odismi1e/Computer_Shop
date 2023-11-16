using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Construct_Main.Components
{
    /// <summary>
    /// Логика взаимодействия для NumberBox.xaml
    /// </summary>
    public partial class NumberBox : UserControl
    {
        public NumberBox()
        {
            InitializeComponent();
            NumberTextBox.Text = "0";
        }

        public static readonly DependencyProperty NumberPropery;

        public decimal Number
        {
            get { return (decimal)GetValue(NumberPropery); }
            set { SetValue(NumberPropery, value); }
        }

        static NumberBox()
        {
            NumberPropery = DependencyProperty.Register("Number", typeof(decimal), typeof(UserControl), new FrameworkPropertyMetadata(null));
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public decimal GetNumber()
        {
            
            return decimal.Parse(NumberTextBox.Text == "" ? "-1" : NumberTextBox.Text);
        }

        private void NumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Number = GetNumber();
        }
    }
}
