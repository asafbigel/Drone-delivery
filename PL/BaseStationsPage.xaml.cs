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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BO;
using System.Collections.ObjectModel;


namespace PL
{
    /// <summary>
    /// Interaction logic for BaseStationsPage.xaml
    /// </summary>
    public partial class BaseStationsPage : Page
    {
        BlApi.IBL bl;
        internal ObservableCollection<BaseStationToList> baseStations;
        //private IEnumerable<BaseStationToList> baseStation;
        public BaseStationsPage(BlApi.IBL theBL)
        {
            bl = theBL;
            InitializeComponent();
            baseStations = new ObservableCollection<BaseStationToList>(bl.GetAllBaseStations(item => true));
            DataContext = baseStations;
        }
        private void BaseStationsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (BaseStationListView.SelectedItem != null)
            {
                BaseStation baseStation = bl.GetBaseStation(((BaseStationToList) BaseStationListView.SelectedItem).Id);
                new BaseStationsOptionsWindow(baseStation as object, bl, this).Show();

            }
            BaseStationListView.UnselectAll();

        }
        private void Grouping_Checked(object sender, RoutedEventArgs e)
        {
            /*if (Grouping.IsChecked == true)
                DataContext = from baseStation in baseStations
                              group baseStation by baseStation.NumOfFreeSlots into g
                              select new { FreeSlots = g.Key, baseStations = g };
            else
            {
                baseStations = new ObservableCollection<BaseStationToList>(bl.GetAllBaseStations(item => true));
                DataContext = baseStations;
            }*/

            refresh();

        }
        internal void refresh()
        {
            baseStations = new ObservableCollection<BaseStationToList>(bl.GetAllBaseStations(item => true));

            var x = from baseStation in baseStations
                        where baseStation != null
                        orderby baseStation.NumOfFreeSlots
                        group baseStation by baseStation.NumOfFreeSlots into g
                        select g;
            if (Grouping.IsChecked == true)
            {
                baseStations = new ObservableCollection<BaseStationToList>();
                foreach (var item in x)
                {
                    foreach (var baseStation in item)
                    {
                        baseStations.Add(baseStation);
                    }
                    baseStations.Add(null);
                }
                
            }
                       
                DataContext = baseStations;
            
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            new AddBaseStationWindow(bl, this).Show();
        }
    }
}
