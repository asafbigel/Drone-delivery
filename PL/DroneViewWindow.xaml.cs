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
    /// Interaction logic for DroneViewWindow.xaml
    /// </summary>
    public partial class DroneViewWindow : Window
    {
        BlApi.IBL theBL;
        Drone drone;
        DroneToList droneToList;
        Action refresh;
        DronesPage dp;
        public DroneViewWindow(Object ob, BlApi.IBL bl, Action Refresh)
        {
            theBL = bl;
            droneToList = (DroneToList)ob;
            drone = bl.GetDrone(droneToList);
            DataContext = drone;
            this.refresh = Refresh;
            /*
             Id.Content = drone.Id;
             Model.Content = drone.Model;
             Weight.Content = drone.MaxWeight;
             Battery.Content = drone.Battery;
             Status.Content = drone.Status;
             Longitude.Content = "Longitude: " + drone.DroneLocation.longitude;
             Latitude.Content = "Latitude: " + drone.DroneLocation.latitude;
             ParcelId.Content = drone.Parcel.Id;
            */
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            new AddDroneWindow(theBL, refresh).Show();
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            // if (refresh != null)
            // {
            new OptionsDroneWindow(theBL, droneToList, refresh).Show();
            Close();
            //  }
            //  else
            //      MessageBox.Show("Can't do it", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}
