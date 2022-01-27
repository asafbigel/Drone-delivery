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
    /// Interaction logic for AddParcelWindow.xaml
    /// </summary>
    public partial class AddParcelWindow : Window
    {

        ParcelToList newParcel;
        BlApi.IBL bl;
        ParcelsPage theParcelsPage;
        
        ObservableCollection<ParcelToList> parcel;
        public AddParcelWindow(BlApi.IBL theBL, ParcelsPage parcelsPage)
        {
            InitializeComponent();
            bl = theBL;
            
            theParcelsPage = parcelsPage;
            newParcel = new ParcelToList();
            this.DataContext = newParcel;

        }
        private void AddTheParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int senderId = int.Parse(this.SenderId.Text);
                int getterId = int.Parse(this.GetterId.Text);
                WeightCategories myWeight = (WeightCategories)this.WeightSelector.SelectedItem;
                Priorities myPriority = (Priorities)this.WeightSelector.SelectedItem;



                Parcel parcel = new Parcel() { Weight = myWeight, Priority = myPriority };
                int myId = bl.Add_parcel(parcel, senderId, getterId);
                MessageBox.Show("Succsess", "Succsess");
                Close();
                parcel = bl.GetParcel(myId);

                /*  if (theParcelsPage != null))
                      theParcelsPage.parcels.Add(new ParcelToList()
                      {
                          Id = parcel.Id,
                          SenderName = parcel.Sender.CustomerName,
                          GetterName = parcel.Getter.CustomerName,
                          Weight = parcel.Weight,
                          Priority = parcel.Priority,
                          Status = ParcelStatuses.Defined

                      }) ;*/
                if (theParcelsPage != null)
                    theParcelsPage.refresh();



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
            
        }
    }
}
