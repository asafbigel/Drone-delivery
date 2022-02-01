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

namespace PL
{
    /// <summary>
    /// Interaction logic for EnterWindow.xaml
    /// </summary>
    public partial class EnterWindow : Window
    {
        BlApi.IBL bl;
        public EnterWindow()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
        }

        private void ManagerLogin_Click(object sender, RoutedEventArgs e)
        {
            


            if (bl.CheckManagerLogin(UserName.Text, Password.Password))
            {
                new ManagerWindow(bl).Show();
                Close();
            }
                
            else
            {
                UserName.Text = "";
                Password.Password = "";
                MessageBox.Show("Wrong  User Name or Password", "Error");

            }
                
            
        }

        private void CustomerLogin_Click(object sender, RoutedEventArgs e)
        {
            int customerId = 0;
            try
            {
                customerId = int.Parse(this.UserName.Text);


                if (bl.CheckCustomerLogin(customerId, Password.Password))
                {
                    Customer customer = bl.GetCustomer(customerId);
                    new CustomerWindow(customer, bl ).Show();
                    Close();
                }

                else
                {
                    UserName.Text = "";
                    Password.Password = "";
                    MessageBox.Show("Wrong  User Name or Password", "Error");

                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("UserName of cusomer must contain only digits  " , "Worng UserName", MessageBoxButton.OK, MessageBoxImage.Warning);
                UserName.Text = "";
                Password.Password = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            new AddCustomerWindow(bl, null).Show();
        }
    }
}
