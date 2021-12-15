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
    /// Interaction logic for AddDroneWindow.xaml
    /// </summary>
    public partial class AddDroneWindow : Window
    {
        DroneToList newDrone;
        IBL.IBL bl;
        public AddDroneWindow(IBL.IBL theBL)
        {
            InitializeComponent();
            //droneList = droneListView;
            bl = theBL;
            newDrone = new DroneToList();
            this.DataContext = newDrone;
            this.Weight.ItemsSource= Enum.GetValues(typeof(WeightCategories));
        }

        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(this.Id.Text);
                string model = this.Model.Text;
                WeightCategories weight = (WeightCategories)this.Weight.SelectedItem;
                int baseStation = int.Parse(this.Station.Text);
                Drone drone = new Drone()
                {
                    Id = id,
                    MaxWeight = weight,
                    Model = model
                };
                bl.Add_drone(drone, baseStation);
                MessageBox.Show("succsses");
                Close();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Wrong input: " + ex.Message, "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
