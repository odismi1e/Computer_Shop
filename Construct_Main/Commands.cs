using System.Windows.Controls;
using System.Windows.Input;

namespace Construct_Main
{
    public class Commands
    {
        static Commands() 
        {
            Search = new RoutedCommand("Search", typeof(Page));
            ApplyFilter = new RoutedCommand("ApplyFilter", typeof(Page));
        }
        public static RoutedCommand ApplyFilter { get; set; }
        public static RoutedCommand Search { get; set; }

    }
}
