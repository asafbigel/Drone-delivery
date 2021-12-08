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
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ListDroneWindow.xaml
    /// </summary>
    public partial class ListDroneWindow : Window
    {
        IBL.IBL bl;
        public ListDroneWindow(IBL.IBL theBL)
        {
            this.bl = theBL;
            InitializeComponent();
            this.StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            this.WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DroneStatuses status = (DroneStatuses)StatusSelector.SelectedItem;
            //this.DroneStatus.Text = status.ToString();
            this.DroneListView.ItemsSource = bl.GetAllDrones(item => item.Status == status);
            //this.WeightSelector.Text = "choose weight:";
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WeightCategories weight = (WeightCategories)WeightSelector.SelectedItem;
            //this.MaxWeight.Text = weight.ToString();
            this.DroneListView.ItemsSource = bl.GetAllDrones(item => item.MaxWeight == weight);
            //this.StatusSelector.Text = "choose status:";
        }

        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            new AddDroneWindow(bl).Show();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
