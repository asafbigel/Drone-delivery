using BO;
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
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        BlApi.IBL bl;
        public ManagerWindow(BlApi.IBL theBl)
        {
            InitializeComponent();
            bl = theBl;
                 
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
        private void ManagerWindow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Pages.SelectedItem == null)
                return;
            string option = ((TextBlock)Pages.SelectedItem).Text;
            switch (option)
            {
                case "Drones":
                    View.Content = new DronesPage(bl);
                    break;
                case "Base stations":
                    View.Content = new BaseStationsPage(bl);
                    break;
                case "Customers":
                    View.Content = new CustomersPage(bl);
                    break;
                case "Parcels":
                    View.Content = new ParcelsPage(bl);
                    break;
                case "Exit":
                    new EnterWindow().Show();
                    Close();
                    

                    break;
                default:
                    break;
            }
        }
    }
}