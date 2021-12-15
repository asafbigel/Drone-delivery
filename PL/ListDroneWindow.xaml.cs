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
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ListDroneWindow.xaml
    /// </summary>
    public partial class ListDroneWindow : Window
    {
        IBL.IBL bl;
        //List<DroneToList> drones;
        public ListDroneWindow(IBL.IBL theBL)
        {
            bl = theBL;
            InitializeComponent();
            //drones = new List<DroneToList>();
            //DroneListView.ItemsSource = drones;
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));          
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StatusSelector.SelectedItem != null)
            {
                DroneStatuses status = (DroneStatuses)StatusSelector.SelectedItem;
                DroneListView.ItemsSource = bl.GetAllDrones(item => item.Status == status);
                this.WeightSelector.Text = "choose weight:";
            }
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeightSelector.SelectedItem != null)
            {
                WeightCategories weight = (WeightCategories)WeightSelector.SelectedItem;
                this.DroneListView.ItemsSource = bl.GetAllDrones(item => item.MaxWeight == weight);
                this.StatusSelector.Text = "choose status:";
            }
        }

        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            new AddDroneWindow(bl).Show();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DroneListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DroneListView.SelectedItem != null)
                new DroneViewWindow(DroneListView.SelectedItem, bl).Show();
            DroneListView.UnselectAll();
        }
    }
}
