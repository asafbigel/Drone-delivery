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
using System.Collections.ObjectModel;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddCustomerWindow.xaml
    /// </summary>
    public partial class AddCustomerWindow : Window
    {

        CustomerToList newCustomer;
        BlApi.IBL bl;
        CustomersPage theCustomersPage;
        ObservableCollection<CustomerToList> customer;
        public AddCustomerWindow(BlApi.IBL theBL, CustomersPage customersPage)
        {
            InitializeComponent();
            bl = theBL;
            theCustomersPage = customersPage;
            newCustomer = new CustomerToList();
            this.DataContext = newCustomer;
            
        }
        private void AddTheCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(this.Id.Text);
                string name = this.Name.Text;
                string phone = this.Phone.Text;
                
                double myLongitude = double.Parse(this.longitude.Text);
                double myLatitude = double.Parse(this.latitude.Text);
                Location myLocation = new Location()
                {
                    longitude = myLongitude,
                    latitude = myLatitude
                };

                Customer customer = new Customer()
                {
                    Id = id,
                    Name = name,
                    Phone = phone,
                    CustomerLocation = myLocation
                    


                };
                bl.Add_customer(customer);
                MessageBox.Show("Succsess", "Succsess");
                Close();
                theCustomersPage.customers.Add(new CustomerToList()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    NumOfParcelsSentAndArrived = 0,
                    NumOfParcelsSentAndNotArrived = 0,
                    NumOfParcelsGot = 0,
                    numOfParcelsToGet = 0,
                   
                });
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Wrong input: " + ex.Message, "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
            //ListDroneWindow.
        }
    }
}
