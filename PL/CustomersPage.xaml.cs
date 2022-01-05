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

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomersPage.xaml
    /// </summary>
    public partial class CustomersPage : Page
    {
        internal ObservableCollection<CustomerToList> customers;
        BlApi.IBL bl;
        public CustomersPage(BlApi.IBL theBL)
        {
            InitializeComponent();
            bl = theBL;
            customers = new ObservableCollection<CustomerToList>(bl.GetAllCustomers(item => true));

    }
}
}
