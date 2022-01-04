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
    /// Interaction logic for ListDroneWindow.xaml
    /// </summary>
    public partial class ListDroneWindow : Window
    {
        BlApi.IBL bl;
        DroneStatuses? status;
        WeightCategories? weight;
        private ObservableCollection<DroneToList> list;
        public ListDroneWindow( BlApi.IBL theBL)
        {
            bl = theBL;
            InitializeComponent();
            //drones = new List<DroneToList>();
            //DroneListView.ItemsSource = drones;
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            //ComboBoxItem allCombo = new ComboBoxItem();
            //allCombo.Content = "";
            //StatusSelector.Items.Add(allCombo);
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            //WeightSelector.Items.Add("");
            //list = new ObservableCollection<DroneToList>(bl.GetAllDrones(item => true));
            //DroneListView.ItemsSource = list;
            DroneListView.ItemsSource = bl.GetAllDrones(item => true);
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                    status = (DroneStatuses)StatusSelector.SelectedItem;
                    DroneListView.ItemsSource = (bl.GetAllDrones(item => item.Status == status && (weight == null || item.MaxWeight == weight)));
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeightSelector.SelectedItem != null)
            {
                weight = (WeightCategories)WeightSelector.SelectedItem;
                DroneListView.ItemsSource = bl.GetAllDrones(item => item.MaxWeight == weight && (status == null || item.Status== status));
            }
        }

        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
  //          new AddDroneWindow(bl).Show();
            //Hide();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
