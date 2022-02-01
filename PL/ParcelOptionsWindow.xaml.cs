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
    /// Interaction logic for ParcelOptionsWindow.xaml
    /// </summary>
    public partial class ParcelOptionsWindow : Window
    {
        BlApi.IBL theBL;
        Parcel parcel;
        Action previousRefresh;
        bool managerFlag;
        int? customerId;
        public ParcelOptionsWindow(object ob, BlApi.IBL bl, Action refreshing, bool manager, int? myCustomerId)
        {
            theBL = bl;
            previousRefresh = refreshing;
            managerFlag = manager;
            customerId = myCustomerId;




            InitializeComponent();
            parcel = (Parcel)ob;
            DataContext = parcel;
            
            if (parcel.Scheduled != null)
            {
                Delete.Visibility = Visibility.Collapsed;
                //Update.Visibility = Visibility.Collapsed;                
                PrioritySelector.IsEnabled = false;
                WeightSelector.IsEnabled = false;
                GetterId.IsEnabled = false;
                

            }
            if(!managerFlag)
            {
                OpenDrone.Visibility = Visibility.Collapsed;
                OpenSender.Visibility = Visibility.Collapsed;
            }
            refresh();
        }

        internal void refresh()
        {
            if (parcel.Scheduled == null)
                OpenDrone.Visibility = Visibility.Collapsed;
            else OpenDrone.Visibility = Visibility.Visible;

            if (parcel.PickedUp != null && (managerFlag || parcel.Sender.Id == customerId) )
                CollectionConfirmation.Visibility = Visibility.Visible;
            if (parcel.Delivered != null && (managerFlag || parcel.Getter.Id == customerId) )
                ReciveConfirmation.Visibility = Visibility.Visible;

            if (parcel.CollectionConfirmation)
                CollectionConfirmation.IsEnabled = false;
            if (parcel.ReciveConfirmation)
                ReciveConfirmation.IsEnabled = false;
            if (previousRefresh != null)
                previousRefresh();

            if (parcel.Scheduled != null &&
                (CollectionConfirmation.Visibility == Visibility.Collapsed || CollectionConfirmation.IsEnabled == false)
                && (ReciveConfirmation.Visibility == Visibility.Collapsed || ReciveConfirmation.IsEnabled == false))
                Update.Visibility = Visibility.Collapsed;
            
            else Update.Visibility = Visibility.Visible;


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
                theBL.DeleteParcel(parcel.Id);
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

