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
    /// Логика взаимодействия для SideMenu.xaml
    /// </summary>
    public partial class SideMenu : UserControl
    {
        public SideMenu()
        {
            InitializeComponent();
        }

        public Button GetButton(string name)
        {
            switch(name)
            {
                case "Catalog":
                    return CatalogButton;
                case "Busket":
                    return BusketButton;
                case "Orders":
                    return OrdersButton;
                case "Auth":
                        return AuthButton;
                case "Report":
                    return ReportButton;
            }
            return null;
        }

        public void OpenSide()
        {
            ThicknessAnimation da = new ThicknessAnimation();
            da.Duration = TimeSpan.FromSeconds(0.3);
            da.EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut };
            da.To = new System.Windows.Thickness(0, 30, 0, 0);
            BeginAnimation(MarginProperty, da);

        }
        public void CloseSide()
        {
            ThicknessAnimation da = new ThicknessAnimation();
            da.Duration = TimeSpan.FromSeconds(0.3);
            da.EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut };
            da.To = new System.Windows.Thickness(-260, 30, 0, 0);
            BeginAnimation(MarginProperty, da);
        }
    }
}
