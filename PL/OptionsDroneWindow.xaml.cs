using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.ComponentModel;
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
        Action refresh;
        int i = 1;
        BackgroundWorker worker;
        //BackgroundWorker worker;
        public OptionsDroneWindow( BlApi.IBL theBL, DroneToList theDrone, Action _refresh)
        {
            InitializeComponent();
            bl = theBL;
            drone = theDrone;
            refresh = _refresh;
            hideButtoms();
            #region Hide the unneeded buttoms
            
            #endregion
            updateDroneDetails(drone);
        }

        private void hideButtoms()
        {
            Close.Visibility = Visibility.Visible;
            Update.Visibility = Visibility.Visible;
            Charge.Visibility = Visibility.Visible;
            FromCharge.Visibility = Visibility.Visible;
            Send.Visibility = Visibility.Visible;
            PickUp.Visibility = Visibility.Visible;
            Delivered.Visibility = Visibility.Visible;
            Automatic.Visibility = Visibility.Visible;
            Manual.Visibility = Visibility.Collapsed;

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
            refresh();
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
                drone = bl.GetAllDrones(item => true).ToList().Find(item => item.Id == drone.Id);
                /*
                updateDroneDetails(drone);
                if (theDronesPage != null)
                    theDronesPage.refresh();
                */
                MessageBox.Show("Succsess", "Succsess");
                updateDroneDetails(drone);
                refresh();
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
                refresh();
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
                //double hours = double.Parse(Interaction.InputBox("Hpw many hours at charge?"));
                bl.drone_from_charge(drone.Id);
                MessageBox.Show("Succsess", "Succsess");
                updateDroneDetails(drone);
                refresh();
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
                refresh();
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
                MessageBox.Show("Succsess", "Succsess");
                updateDroneDetails(drone);
                refresh();
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
                MessageBox.Show("Succsess", "Succsess");
                updateDroneDetails(drone);
                refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Automatic_Click(object sender, RoutedEventArgs e)
        {
            Close.Visibility = Visibility.Collapsed;
            Update.Visibility = Visibility.Collapsed;
            Charge.Visibility = Visibility.Collapsed;
            FromCharge.Visibility = Visibility.Collapsed;
            Send.Visibility = Visibility.Collapsed;
            PickUp.Visibility = Visibility.Collapsed;
            Delivered.Visibility = Visibility.Collapsed;
            Automatic.Visibility = Visibility.Collapsed;
            Manual.Visibility = Visibility.Visible;

            try
            {
                worker = new BackgroundWorker();
                worker.DoWork += Worker_DoWork;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.WorkerReportsProgress = true;
                worker.WorkerSupportsCancellation = true;

                worker.RunWorkerAsync();
              //  worker.RunWorkerAsync(12);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();   
        }

        public void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            refresh();
        }

        public void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                bl.auto(drone.Id, update, stop);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool stop()
        {
           // MessageBox.Show(i++.ToString(), "ok");

            return worker.CancellationPending;
        }

        internal void update()
        {
            worker.ReportProgress(0);
        }

        private bool foo()
        {
            i++;
            //  MessageBox.Show("continue Y/N", "choose", MessageBoxButton.YesNo, MessageBoxImage.None, MessageBoxResult.Yes);
            MessageBox.Show(i.ToString(), "ok");
            return Manual.Visibility == Visibility.Visible;
            /*
            MessageBoxResult mbResult = MessageBox.Show("continue Y/N", "choose", MessageBoxButton.YesNo, MessageBoxImage.None, MessageBoxResult.Yes);
            switch (mbResult)
            {
                case MessageBoxResult.Yes:
                    return true;
                case MessageBoxResult.No:
                    return false;
                default:
                    return true;
            }
            */
        }

        private void Manual_Click(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
           // hideButtoms();
        }


        /*

        private void Automatic_Click(object sender, RoutedEventArgs e)
        {
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }
        */
    }
}
