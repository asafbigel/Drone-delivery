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
using BO;
using BL;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerViewWindow.xaml
    /// </summary>
    public partial class CustomerViewWindow : Window
    {
        BlApi.IBL theBL;
        Customer customer;
        Action previousRefresh;
        bool managerFlag;
        public CustomerViewWindow(Object ob, BlApi.IBL bl, Action refreshing, bool manager)
        {
            theBL = bl;
            previousRefresh = refreshing;
            managerFlag = manager;
            InitializeComponent();
            customer = (Customer)ob;
            DataContext = customer;
            if (!manager)
            {
                Password.Visibility = Visibility.Collapsed;
               
            }
            
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();           
        }
        internal void refresh()
        {
            if(previousRefresh!=null)
                previousRefresh();
            customer = theBL.GetCustomer(customer.Id);
            DataContext = customer;
        }


        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            new AddCustomerWindow(theBL, refresh).Show();
        }
        private void ParcelsForCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ParcelsForCustomer.SelectedItem != null)
            {
                Parcel parcel = theBL.GetParcel(ParcelsForCustomer.SelectedItem as ParcelAtCustomer);
                new ParcelOptionsWindow(parcel as object, theBL, null, managerFlag, customer.Id).Show();

            }
            ParcelsForCustomer.UnselectAll();

        }
        private void ParcelsFromCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ParcelsFromCustomer.SelectedItem != null)
            {
                Parcel parcel = theBL.GetParcel(ParcelsFromCustomer.SelectedItem as ParcelAtCustomer);
                new ParcelOptionsWindow(parcel as object, theBL, null, managerFlag, customer.Id).Show();

            }
            ParcelsFromCustomer.UnselectAll();

        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                theBL.UpdateCustomer(customer);               
                refresh();
                TheCustomerLocation.Text = customer.CustomerLocation.ToString();
                MessageBox.Show("Succsess", "Succsess");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void AddParcel_Click(object sender, RoutedEventArgs e)
        {
            new AddParcelWindow(theBL, refresh, customer.Id).Show();
        }
    }
}



       
