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
    /// Interaction logic for CustomeWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        BlApi.IBL theBL;
        Customer customer;
        

        public CustomerWindow(Customer theCustomer, BlApi.IBL bl)
        {
            theBL = bl;
            customer = theCustomer;
            InitializeComponent();
             DataContext = customer;
           
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            new EnterWindow().Show();
            Close();
        }



       
        private void ParcelsForCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ParcelsForCustomer.SelectedItem != null)
            {
                Parcel parcel = theBL.GetParcel(ParcelsForCustomer.SelectedItem as ParcelAtCustomer);
                new ParcelOptionsWindow(parcel as object, theBL, refresh, false).Show();

            }
            ParcelsForCustomer.UnselectAll();

        }
        private void ParcelsFromCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ParcelsFromCustomer.SelectedItem != null)
            {
                Parcel parcel = theBL.GetParcel(ParcelsFromCustomer.SelectedItem as ParcelAtCustomer);
                new ParcelOptionsWindow(parcel as object, theBL, refresh, false).Show();

            }
            ParcelsFromCustomer.UnselectAll();

        }
        internal void refresh()
        {
            customer = theBL.GetCustomer(customer.Id);
            DataContext = customer;
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {

           new CustomerViewWindow(customer, theBL, refresh, false).Show();

        }

        private void AddParcel_Click(object sender, RoutedEventArgs e)
        {
            new AddParcelWindow(theBL, refresh, customer.Id).Show();
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            new ChangePasswordWindow(theBL, customer.Id).Show();
        }
    }
}




