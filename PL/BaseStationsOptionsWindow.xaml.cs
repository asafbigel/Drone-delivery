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
using BL; 

namespace PL
{
    /// <summary>
    /// Interaction logic for BaseStationOptionsWindow.xaml
    /// </summary>
    public partial class BaseStationsOptionsWindow : Window
    {
        BlApi.IBL theBL;
        BaseStation baseStation;
        //BaseStationsPage theBaseStationsPage;
        Action refresh;
        public BaseStationsOptionsWindow(object ob, BlApi.IBL bl, Action _refresh)
        {
            theBL = bl;
            refresh = _refresh;
            
            InitializeComponent();
            baseStation = (BaseStation)ob;
            DataContext = baseStation;
           
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            new AddBaseStationWindow(theBL, refresh).Show();

        }

        
       
        private void Update_Click(object sender, RoutedEventArgs e)
        { 
            try
            {

                theBL.UpdateBaseStation(baseStation);

                if (refresh != null)
                    refresh();

                MessageBox.Show("Succsess", "Succsess");
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Wrong input: " + ex.Message, "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            

        }
        private void DroneInChargings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (DroneInChargings.SelectedItem != null)
            {
                DroneToList drone = theBL.GetDroneToList(((DroneInCharging)DroneInChargings.SelectedItem).Id);  
                new DroneViewWindow(drone as object, theBL, null).Show();

            }
            DroneInChargings.UnselectAll();
            
        }

    }
}

