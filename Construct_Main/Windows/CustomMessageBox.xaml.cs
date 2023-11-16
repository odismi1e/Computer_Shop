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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Construct_Main.Windows
{
    /// <summary>
    /// Логика взаимодействия для CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window/*, INotifyPropertyChanged*/
    {

        //public event PropertyChangedEventHandler PropertyChanged;
        //private void NotifyPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}

        //private string _mainText = "";
        //public string MainText
        //{
        //    get { return _mainText; }
        //    set
        //    {
        //        _mainText = value;
        //        NotifyPropertyChanged("MainText");
        //    }
        //}

        //private string _headText = "";
        //public string HeadText
        //{
        //    get { return _headText; }
        //    set
        //    {
        //        _headText = value;
        //        NotifyPropertyChanged("HeadText");
        //    }
        //}

        public CustomMessageBox(string main, string head)
        {
            InitializeComponent();
            //MainText = main;
            //HeadText = head;

            MainText.Text = main;
            HeadText.Text = head;
        }

        //public void UpdateContent()
        //{
        //    NotifyPropertyChanged("HeadText");
        //    NotifyPropertyChanged("MainText");
        //}
        private void DragRectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
