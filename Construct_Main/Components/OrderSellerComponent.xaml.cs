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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Construct_Main.Components
{
    /// <summary>
    /// Логика взаимодействия для OrderSellerComponent.xaml
    /// </summary>
    public partial class OrderSellerComponent : UserControl
    {
        public OrderSellerComponent()
        {
            InitializeComponent();
        }

        private bool isOpen = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!isOpen)
            {
                DoubleAnimation da = new DoubleAnimation();
                da.Duration = TimeSpan.FromSeconds(0.3);
                da.EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut };
                da.To = 200;
                OrderBody.BeginAnimation(HeightProperty, da);

                Arrow.Kind = MaterialDesignThemes.Wpf.PackIconKind.ArrowUp;
            }
            else
            {
                DoubleAnimation da = new DoubleAnimation();
                da.Duration = TimeSpan.FromSeconds(0.3);
                da.EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut };
                da.To = 0;
                OrderBody.BeginAnimation(HeightProperty, da);
                Arrow.Kind = MaterialDesignThemes.Wpf.PackIconKind.ArrowDown;
            }

            isOpen = !isOpen;
        }
    }
}
