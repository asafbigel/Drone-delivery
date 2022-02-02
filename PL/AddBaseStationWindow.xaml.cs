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
using System.Collections.ObjectModel;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddBaseStationWindow.xaml
    /// </summary>
    public partial class AddBaseStationWindow : Window
    {

        BaseStationToList newBaseStation;
        BlApi.IBL bl;
        //BaseStationsPage theBaseStationsPage;
        Action refresh;
        BaseStation baseStation = new BaseStation();
        public AddBaseStationWindow(BlApi.IBL theBL, Action _refresh)
        {
            InitializeComponent();
            bl = theBL;
            refresh = _refresh;
            baseStation.BaseStationLocation = new Location();
            DataContext = baseStation;
        }
        private void AddTheBaseStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int.Parse(this.Id.Text);
                int.Parse(this.NumOfSlotsCharge.Text);
                double.Parse(this.longitude.Text);
                double.Parse(this.latitude.Text);
                bl.AddBaseStation(baseStation);

                if (refresh != null)
                    refresh();

                MessageBox.Show("Succsess", "Succsess");
                Close();

                /*if (theBaseStationsPage != null)
                    theBaseStationsPage.baseStations.Add(new BaseStationToList()
                    {
                        Id = baseStation.Id,
                        Name = baseStation.Name,
                        NumOfFreeSlots = baseStation.Num_Free_slots_charge,
                        NumOfBusySlots = 0


                    });*/
               
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

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
            //ListDroneWindow.
        }


    }
}
