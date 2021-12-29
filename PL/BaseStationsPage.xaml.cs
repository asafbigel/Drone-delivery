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
        private ObservableCollection<BaseStationToList> baseStations;
        //private IEnumerable<BaseStationToList> baseStation;
        public BaseStationsPage(BlApi.IBL theBL)
        {
            bl = theBL;
            InitializeComponent();
            baseStations = new ObservableCollection<BaseStationToList>(bl.GetAllBaseStations(item => true));
            DataContext = baseStations;
        }

        private void Grouping_Checked(object sender, RoutedEventArgs e)
        {
            if (Grouping.IsChecked == true)
                DataContext = from baseStation in baseStations
                              group baseStation by baseStation.NumOfFreeSlots into g
                              select new { FreeSlots = g.Key, baseStations = g };
                              
        }
    }
}
