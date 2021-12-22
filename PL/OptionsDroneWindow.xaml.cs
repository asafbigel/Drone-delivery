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
using Microsoft.VisualBasic;

namespace PL
{
    /// <summary>
    /// Interaction logic for OptionsDroneWindow.xaml
    /// </summary>
    public partial class OptionsDroneWindow : Window
    {
         BlApi.IBL bl;
        DroneToList drone;
        public OptionsDroneWindow( BlApi.IBL theBL, DroneToList theDrone)
        {
            InitializeComponent();
            bl = theBL;
            drone = theDrone;
            #region Hide the unneeded buttoms
            if (drone.Status == DroneStatuses.vacant)
            {
                FromCharge.Visibility = Visibility.Collapsed;
                PickUp.Visibility = Visibility.Collapsed;
                Delivered.Visibility = Visibility.Collapsed;
            }
            if (drone.Status == DroneStatuses.maintenance)
            {
                Charge.Visibility = Visibility.Collapsed;
                Send.Visibility = Visibility.Collapsed;
                PickUp.Visibility = Visibility.Collapsed;
                Delivered.Visibility = Visibility.Collapsed;
            }
            if (drone.Status == DroneStatuses.sending)
            {
                Charge.Visibility = Visibility.Collapsed;
                FromCharge.Visibility = Visibility.Collapsed;
                Send.Visibility = Visibility.Collapsed;
                Parcel parcel = bl.GetCurrectParcelOfDrone(drone.Id);
                if (parcel.PickedUp == null)
                    Delivered.Visibility = Visibility.Collapsed;
                else
                    PickUp.Visibility = Visibility.Collapsed;
            }
            #endregion
            updateDroneDetails(drone);
        }
        // set the details of this drone
        private void updateDroneDetails(DroneToList drone)
        {
            Id.Content = drone.Id;
            Model.Text = drone.Model;
            Weight.Content = drone.MaxWeight;
            Battery.Content = drone.Battery;
            Status.Content = drone.Status;
            Longitude.Content = drone.DroneLocation.longitude;
            Latitude.Content = drone.DroneLocation.latitude;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string newModel = Model.Text;
                bl.update_model_drone(drone.Id, newModel);
                drone = bl.GetAllDrones(item => true).Find(item => item.Id == drone.Id);
                MessageBox.Show("Succsess", "Succsess");
                updateDroneDetails(drone);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error" , MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Charge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.send_drone_to_charge(drone.Id);
                MessageBox.Show("Succsess", "Succsess");
                updateDroneDetails(drone);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FromCharge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double hours = double.Parse(Interaction.InputBox("Hpw many hours at charge?"));
                bl.drone_from_charge(drone.Id, hours);
MessageBox.Show("Succsess","Succsess");
                updateDroneDetails(drone);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.connect_parcel_to_drone(drone.Id);
MessageBox.Show("Succsess","Succsess");
                updateDroneDetails(drone);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PickUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.pickedUp_parcel_by_drone(drone.Id);
MessageBox.Show("Succsess","Succsess");
                updateDroneDetails(drone);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delivered_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.delivered_parcel_by_drone(drone.Id);
MessageBox.Show("Succsess","Succsess");
                updateDroneDetails(drone);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
