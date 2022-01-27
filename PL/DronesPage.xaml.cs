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
using System.Windows.Shapes;
using BO;


namespace PL
{
    /// <summary>
    /// Interaction logic for DronesPage.xaml
    /// </summary>
    public partial class DronesPage : Page
    {
        BlApi.IBL bl;
        DroneStatuses? status;
        WeightCategories? weight;
        internal ObservableCollection<DroneToList> drones;
       
        public DronesPage(BlApi.IBL theBL)
        {
            InitializeComponent();
            bl = theBL;
            drones = new ObservableCollection<DroneToList>(bl.GetAllDrones(item => true));
            DataContext = drones;
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StatusSelector.SelectedItem != null)
            {
                status = (DroneStatuses)StatusSelector.SelectedItem;
                var x = (bl.GetAllDrones(item => item.Status == status && (weight == null || item.MaxWeight == weight)));
                drones.Clear();
                foreach (var item in x)
                {
                    drones.Add(item);
                }
            }
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeightSelector.SelectedItem != null)
            {
                weight = (WeightCategories)WeightSelector.SelectedItem;
                var x = bl.GetAllDrones(item => item.MaxWeight == weight && (status == null || item.Status == status));
                drones.Clear();
                foreach (var item in x)
                {
                    drones.Add(item);
                }
            }
        }

        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            new AddDroneWindow(bl,this).Show();
        }

        private void DroneListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DroneListView.SelectedItem != null)
            {
                if (DroneSelection.IsChecked == true)              
                    new DroneViewWindow(DroneListView.SelectedItem, bl, Refresh).Show();
                DroneToList d = (DroneToList)DroneListView.SelectedItem;
                ParcelAtTransfer parcel = bl.GetCurrectParcelAtTransferOfDrone(d.Id);
                 if (ParcelSelection.IsChecked == true && parcel != null && parcel.Id != 0)
                    new ParcelViewWindow(parcel, bl).Show();
            }
            DroneListView.UnselectAll();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            drones = new ObservableCollection<DroneToList>(bl.GetAllDrones(item => true));
            DataContext = drones;
            WeightSelector.Text = "choose weight:";
            StatusSelector.Text = "choose status:";
            status = null;
            weight = null;
        }

        private void Grouping_Click(object sender, RoutedEventArgs e)
        {
            var x = from drone in drones
                    where drone != null
                    group drone by drone.Status into g
                    select g;
            drones = new ObservableCollection<DroneToList>();
            foreach (var item in x)
            {
                foreach (var drone in item)
                {
                    drones.Add(drone);
                }
                drones.Add(null);
            }
            DataContext = drones;
        }

        internal void Refresh()
        {
            weight = null;
            status = null;
            try
            {
                if (WeightSelector.SelectedItem != null)
                    weight = (WeightCategories)WeightSelector.SelectedItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (StatusSelector.SelectedItem != null)
                status = (DroneStatuses)StatusSelector.SelectedItem;
            var x = bl.GetAllDrones(item => (weight == null || item.MaxWeight == weight) && (status == null || item.Status == status));
            drones.Clear();
            foreach (var item in x)
            {
                drones.Add(item);
            }
        }

    }
}