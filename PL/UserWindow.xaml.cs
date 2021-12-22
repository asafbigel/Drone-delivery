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
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        BlApi.IBL bl;
        public UserWindow(BlApi.IBL theBL)
        {
            InitializeComponent();
            bl = theBL; 
        }

        private void Drones_Click(object sender, RoutedEventArgs e)
        {
            View.Content = new DronesPage(bl);
        }

        private void BaseStations_Click(object sender, RoutedEventArgs e)
        {
            View.Content = new BaseStationsPage(bl);
        }

        private void Customers_Click(object sender, RoutedEventArgs e)
        {
            View.Content = new CustomersPage(bl);
        }

        private void Parcels_Click(object sender, RoutedEventArgs e)
        {
            View.Content = new ParcelsPage(bl); 
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
