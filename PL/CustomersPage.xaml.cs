using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomersPage.xaml
    /// </summary>
    public partial class CustomersPage : Page
    {
        BlApi.IBL bl;
        internal ObservableCollection<CustomerToList> customers;

        public CustomersPage(BlApi.IBL theBL)
        {
            bl = theBL;
            InitializeComponent();
            customers = new ObservableCollection<CustomerToList>(bl.GetAllCustomers(item => true));
            DataContext = customers;
        }

        private void AddCustumer_Click(object sender, RoutedEventArgs e)
        {
            new AddCustomerWindow(bl, this).Show();
        }
    }
}
