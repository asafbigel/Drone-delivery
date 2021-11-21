using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using IBL.BO;

namespace IBL
{
    public partial class BL
    {
        static IDAL.IDal mydal;
        public BL()
        {
            mydal = new DalObject.DalObject();
            double[] Electricity = mydal.ElectricityUse();
            double Electricity_free = Electricity[0];
            double Electricity_light = Electricity[1];
            double Electricity_medium = Electricity[2];
            double Electricity_heavy = Electricity[3];
            double Charge_at_hour = Electricity[4];


            #region List of drone from the data layer
            List<IBL.BO.Drone> my_drones = new List<IBL.BO.Drone>();
            List<IDAL.DO.Drone> idalDrones = mydal.Get_all_drones().ToList();
            foreach (var item in idalDrones)
            {
                my_drones.Add(new IBL.BO.Drone
                {
                    Id = item.Id,
                    Model = item.Model,
                    MaxWeight = (IBL.BO.WeightCategories)item.MaxWeight
                });
            }
            #endregion

            // List of the parcels
            List<IDAL.DO.Parcel> idalParcel = mydal.Get_all_parcels().ToList();

            foreach (var item in my_drones)
            {
                
            }


            /*
            #region List of parcel from the data layer
            List<IBL.BO.Parcel> my_parcels = new List<IBL.BO.Parcel>();
            List<IDAL.DO.Parcel> idalParcel = mydal.Get_all_parcels().ToList();
            foreach (var item in idalParcel)
            {
                my_parcels.Add(new IBL.BO.Parcel
                {
                     id = item.Id, PickedUp = item.PickedUp , priority = (BO.Priorities)item.Priority, Requested = item.Requested,
                      Delivered = item.Delivered, Scheduled = item.Scheduled, weight = item.Weight, getter=(CustomerAtParcel)item.TargetId, sender = item.SenderId
                });
            }
            #endregion
            */


        }
        /*
      public void Add_base_station(BO.BaseStation baseStation)
      {
          IEnumerable<IDAL.DO.BaseStation> my_baseStation = mydal.Get_all_base_stations();

          if (my_baseStation.Any(bs => bs.Id == baseStation.id))
          {
              throw new BaseStationExeption("id allready exist");
          }
          
        mydal.Add_base_station(baseStation);
        }
        */
    }
}