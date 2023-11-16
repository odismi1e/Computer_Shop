using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Construct_Main.Components
{
    /// <summary>
    /// Логика взаимодействия для OrderComponent.xaml
    /// </summary>
    public partial class OrderComponent : UserControl
    {
        public OrderComponent()
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
