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
        CustomersPage theCustomersPage;
        bool CustomersPageFlag = false;
        public CustomerViewWindow(Object ob, BlApi.IBL bl, Object customersPage)
        {
            theBL = bl;
            if(customersPage != null)
            {
                theCustomersPage = (CustomersPage)customersPage;
                CustomersPageFlag = true;
            }
            InitializeComponent();
            customer = (Customer)ob;
            DataContext = customer;

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            new AddCustomerWindow(theBL, theCustomersPage).Show();
        }
        private void ParcelsForCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ParcelsForCustomer.SelectedItem != null)
            {
                Parcel parcel = theBL.GetParcel(ParcelsForCustomer.SelectedItem as ParcelAtCustomer);
                new ParcelOptionsWindow(parcel as object, theBL, null).Show();

            }
            ParcelsForCustomer.UnselectAll();

        }
        private void ParcelsFromCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ParcelsFromCustomer.SelectedItem != null)
            {
                Parcel parcel = theBL.GetParcel(ParcelsFromCustomer.SelectedItem as ParcelAtCustomer);
                new ParcelOptionsWindow(parcel as object, theBL, null).Show();

            }
            ParcelsFromCustomer.UnselectAll();

        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                theBL.updateCustomer(customer);
                MessageBox.Show("Succsess", "Succsess");
                if (theCustomersPage != null)
                    theCustomersPage.refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
    }
}



       
