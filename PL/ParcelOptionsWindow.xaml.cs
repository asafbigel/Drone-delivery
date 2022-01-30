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
    /// Interaction logic for ParcelOptionsWindow.xaml
    /// </summary>
    public partial class ParcelOptionsWindow : Window
    {
        BlApi.IBL theBL;
        Parcel parcel;
        Action refresh;
        bool managerFlag;
        public ParcelOptionsWindow(object ob, BlApi.IBL bl, Action refreshing, bool manager)
        {
            theBL = bl;
            refresh = refreshing;
            managerFlag = manager;


            InitializeComponent();
            parcel = (Parcel)ob;
            DataContext = parcel;
            if (parcel.Scheduled == null)
                OpenDrone.Visibility = Visibility.Collapsed;
            if (parcel.Scheduled != null)
            {
                Delete.Visibility = Visibility.Collapsed;
                Update.Visibility = Visibility.Collapsed;                
                PrioritySelector.IsEnabled = false;
                WeightSelector.IsEnabled = false;
                GetterId.IsEnabled = false;
            }
            if(!manager)
            {
                OpenDrone.Visibility = Visibility.Collapsed;
                OpenSender.Visibility = Visibility.Collapsed;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            new AddParcelWindow(theBL, refresh, null).Show();

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                theBL.DelsteParcel(parcel.Id);
                if (refresh != null)
                    refresh();
                MessageBox.Show("Succsess", "Succsess");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void OpenSender_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer customer = theBL.GetCustomer(parcel.Sender.Id);
                new CustomerViewWindow(customer, theBL, null, managerFlag).Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void OpenGetter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer customer = theBL.GetCustomer(parcel.Getter.Id);
                new CustomerViewWindow(customer, theBL, null, managerFlag).Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenDrone_Click(object sender, RoutedEventArgs e)
        {

            if(parcel.Scheduled != null)
            {
                DroneToList myDrone = theBL.GetDroneToList(parcel.TheDrone.Id);
                new DroneViewWindow(myDrone, theBL, null).Show();
                
            }


        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CustomerAtParcel mySender = theBL.GetCustomerAtParcel(parcel.Sender.Id);
                CustomerAtParcel myGetter = theBL.GetCustomerAtParcel(parcel.Getter.Id);
                parcel.Sender.Id = mySender.Id;
                parcel.Sender.CustomerName = mySender.CustomerName;

                parcel.Getter.Id = myGetter.Id;
                parcel.Getter.CustomerName = myGetter.CustomerName;
               
                theBL.updateParcel(parcel);
                
                SenderName.Content = parcel.Sender.CustomerName;
                GetterName.Content = parcel.Getter.CustomerName;
                if (refresh != null)
                    refresh();
                MessageBox.Show("Succsess", "Succsess");

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

        
    }
}

