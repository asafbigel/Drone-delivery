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

        //ParcelToList newParcel;
        Parcel myParcel = new();
        BlApi.IBL bl;
        Action refresh;
        int? id;

        ObservableCollection<ParcelToList> parcel;
        public AddParcelWindow(BlApi.IBL theBL, Action refreshing, int? customerId)
        {
            InitializeComponent();
            bl = theBL;
            myParcel.Sender = new();
            
            myParcel.Getter = new();

            refresh = refreshing;
            //newParcel = new ParcelToList();

            id = customerId;
            if (id !=null)
            {
                
                SenderId.Visibility = Visibility.Collapsed;
            }
            DataContext = myParcel;

        }
        private void AddTheParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /* int senderId;
                 if (id == null)
                     senderId = int.Parse(this.SenderId.Text);
                 else senderId = (int)id;

                 int getterId = int.Parse(this.GetterId.Text);
                 // WeightCategories myWeight = (WeightCategories)this.WeightSelector.SelectedItem;
                 //Priorities myPriority = (Priorities)this.WeightSelector.SelectedItem;
                 WeightCategories myWeight = newParcel.Weight;
                 Priorities myPriority = newParcel.Priority;


                 Parcel parcel = new Parcel() { Weight = myWeight, Priority = myPriority };
                 int myId = bl.Add_parcel(parcel, senderId, getterId);
                 MessageBox.Show("Succsess", "Succsess");
                 Close();
                 parcel = bl.GetParcel(myId);*/
                if (id != null)
                    myParcel.Sender.Id= (int)id;
                int parcelId = bl.AddParcel(myParcel);
                MessageBox.Show("Succsess, the id of the new id is "+ parcelId, "Succsess");
                if (refresh != null)
                    refresh();
                Close();

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
