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
        Action refresh;
        ObservableCollection<CustomerToList> customers;

        Customer customer = new Customer();
        public AddCustomerWindow(BlApi.IBL theBL, Action refreshing)
        {
            InitializeComponent();
            bl = theBL;
            refresh = refreshing;
            // newCustomer = new CustomerToList();
            //this.DataContext = newCustomer;
            customer.CustomerLocation = new Location();
            DataContext = customer;
        }
        private void AddTheCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /* int id = int.Parse(this.Id.Text);
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
                */
                int.Parse(this.Id.Text);
                int.Parse(this.Phone.Text);
                double.Parse(this.longitude.Text);
                if (Password.Password.Length < 1)
                    throw new EnterPasswotdExeption("Enter Password");
                customer.Password = Password.Password;
                double.Parse(this.latitude.Text);
                bl.AddCustomer(customer);
                MessageBox.Show("Succsess", "Succsess");
                MessageBox.Show("Succsess, the User name to cusomer login is the Id" , "Succsess");
                Close();

                /* if(theCustomersPage!=null)
                 theCustomersPage.customers.Add(new CustomerToList()
                 {
                     Id = customer.Id,
                     Name = customer.Name,
                     Phone = customer.Phone,
                     NumOfParcelsSentAndArrived = 0,
                     NumOfParcelsSentAndNotArrived = 0,
                     NumOfParcelsGot = 0,
                     numOfParcelsToGet = 0,

                 });*/
                if (refresh != null)
                    refresh();


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
