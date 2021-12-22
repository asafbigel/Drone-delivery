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
        private ObservableCollection<DroneToList> drones;
        public DronesPage(BlApi.IBL theBL)
        {
            bl = theBL;
            InitializeComponent();
            drones = new ObservableCollection<DroneToList>(bl.GetAllDrones(item => true));
            DataContext = drones;
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
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
            //new AddDroneWindow(bl, drones).Show();
            new AddDroneWindow(bl).Show();
        }

        private void DroneListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DroneListView.SelectedItem != null)
                new DroneViewWindow(DroneListView.SelectedItem, bl).Show();
            DroneListView.UnselectAll();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            DroneListView.ItemsSource = bl.GetAllDrones(item => true);
            WeightSelector.Text = "choose weight:";
            StatusSelector.Text = "choose status:";
            status = null;
            weight = null;
        }
    }
}