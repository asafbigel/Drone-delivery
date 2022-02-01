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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BO;



namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelsPage.xaml
    /// </summary>
    public partial class ParcelsPage : Page
    {
        BlApi.IBL bl;
        ParcelStatuses? status;
        WeightCategories? weight;
        internal ObservableCollection<ParcelToList> parcels;
        
       /* int? StartDay;
        int? StartMonth;
        int? StartYear;

        int? EndDay;
        int? EndMonth;
        int? EndYear;*/


        // DateTime? DateFrom; 
        // DateTime? Until;

        public ParcelsPage(BlApi.IBL theBL)
        {
            InitializeComponent();
            bl = theBL;
            parcels = new ObservableCollection<ParcelToList>(bl.GetAllParcels(item => true));
            DataContext = parcels;
           
        }
        
        
        

        private void refresh(object sender, SelectionChangedEventArgs e)
        {/*
            if (WeightSelector.SelectedItem != null)
            {
                weight = (WeightCategories)WeightSelector.SelectedItem;
                var x = bl.GetAllParcels(item => item.Weight == weight && (status == null || item.Status == status));
                parcels.Clear();
                foreach (var item in x)
                {
                    parcels.Add(item);
                }
            }*/
            refresh();
        }
        internal void refresh()
        {
           try
           {
              status = (ParcelStatuses?)StatusSelector.SelectedItem;
              weight = (WeightCategories?)WeightSelector.SelectedItem;
              var x = (bl.GetAllParcels(item => (status == null || item.Status == status) && (weight == null || item.Weight == weight))).ToList();
              if ((yearFrom.Text != "") || (yearFrom.Text.Trim() != "") || (monthFrom.Text.Trim() != "")
                      || (dayFrom.Text.Trim() != "") || (monthEnd.Text.Trim() != "") || (yearEnd.Text.Trim() != "") || (dayEnd.Text.Trim() != ""))
              {
                 DateTime from = new DateTime(int.Parse(this.yearFrom.Text.Trim()), int.Parse(this.monthFrom.Text.Trim()), int.Parse(this.dayFrom.Text.Trim()));
                 DateTime until = new DateTime(int.Parse(this.yearEnd.Text.Trim()), int.Parse(this.monthEnd.Text.Trim()), int.Parse(this.dayEnd.Text.Trim()));
                 bl.CheckDate(from, until);
                        
                 until = until.AddDays(1);
                 x = x.FindAll(item => (bl.GetParcel(item).Requested >= from) && (bl.GetParcel(item).Requested < until));
              }
              parcels.Clear();

              foreach (var item in x)
              {
                parcels.Add(item);
              }
             
           }
            catch (FormatException ex)
            {
                MessageBox.Show("Wrong input: " + ex.Message, "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
                yearFrom.Text = yearFrom.Text = monthFrom.Text = dayFrom.Text = yearEnd.Text = monthEnd.Text = dayEnd.Text = "";
                refresh();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                yearFrom.Text = yearFrom.Text = monthFrom.Text = dayFrom.Text = yearEnd.Text = monthEnd.Text = dayEnd.Text = "";
                refresh();
            }

        }

        private void AddParcel_Click(object sender, RoutedEventArgs e)
        {
            
            new AddParcelWindow(bl, refresh, null).Show();
        }

        private void ParcelListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ParcelListView.SelectedItem != null)
            {
                Parcel myParcel = bl.GetParcel((ParcelToList)ParcelListView.SelectedItem );
                new ParcelOptionsWindow(myParcel, bl, refresh, true, null).Show();
                ParcelToList d = (ParcelToList)ParcelListView.SelectedItem;
            }
            ParcelListView.UnselectAll();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            //ParcelListView.ItemsSource = bl.GetAllParcels(item => true);
            parcels = new ObservableCollection<ParcelToList>(bl.GetAllParcels(item => true));
            DataContext = parcels;
            WeightSelector.Text = "choose weight:";
            StatusSelector.Text = "choose status:";
            status = null;
            weight = null;
            yearFrom.Text = yearFrom.Text = monthFrom.Text = dayFrom.Text = yearEnd.Text = monthEnd.Text = dayEnd.Text = "";
        }
       
        private void GroupingByGetter_Click(object sender, RoutedEventArgs e)
        {
            var x = from parcel in parcels
                    where parcel != null
                    group parcel by parcel.GetterName into g
                    select g;
           
            parcels = new ObservableCollection<ParcelToList>();
            foreach (var item in x)
            {
                foreach (var parcel in item)
                {
                    parcels.Add(parcel);
                }
                parcels.Add(null);
            }
            DataContext = parcels;
        }

        private void GroupingBySender_Click(object sender, RoutedEventArgs e)
        {

            var x = from parcel in parcels
                    where parcel != null
                    group parcel by parcel.SenderName into g
                    select g;
            parcels = new ObservableCollection<ParcelToList>();
            foreach (var item in x)
            {
                foreach (var parcel in item)
                {
                    parcels.Add(parcel);
                }
                parcels.Add(null);
            }
            DataContext = parcels;
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            refresh();
        }
    }
   

}
