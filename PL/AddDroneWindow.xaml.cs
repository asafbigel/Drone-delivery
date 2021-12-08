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
            bl = theBL;
            newDrone = new DroneToList();
            this.DataContext = newDrone;
            this.Weight.ItemsSource= Enum.GetValues(typeof(WeightCategories));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
            this.Close();
        }
    }
}
