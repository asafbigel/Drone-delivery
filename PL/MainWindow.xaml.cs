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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
         BlApi.IBL theBL;
        public MainWindow()
        {
            theBL = BlApi.DalFactory.GetBL();
        }

        private void btnListDrone_Click(object sender, RoutedEventArgs e)
        {
            new ListDroneWindow(theBL).Show();
        }

        private void btnAddDrone_Click(object sender, RoutedEventArgs e)
        {
     //       new AddDroneWindow(theBL).Show();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnUserView_Click(object sender, RoutedEventArgs e)
        {
           // new UserWindow(theBL).Show();
        }
    }
}
