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
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        BlApi.IBL bl;
        int id;
        public ChangePasswordWindow(BlApi.IBL theBL, int myId)
        {
            bl = theBL;
            id = myId;
            InitializeComponent();
        }

        private void UpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(id.ToString() != UserName.Text)
                    throw new WrongUserNameExeption("Wrong User Name");


                Customer customer = bl.GetCustomer(id);

                if (OldPassword.Password != customer.Password)
                    MessageBox.Show("Wrong Old Password", "Error");

                if (NewPassword.Password.Length < 1)
                    throw new EnterPasswotdExeption("Enter Password");
                
                    customer.Password = NewPassword.Password;
                    bl.UpdateCustomer(customer);
                    MessageBox.Show("Succsess", "Succsess");
                    Close();
                
            }
            catch (EnterPasswotdExeption ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (WrongUserNameExeption ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                UserName.Text = "";
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
    }
}
