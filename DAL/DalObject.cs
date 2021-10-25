using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public class DalObject
    {
        public void add_base_station(int id, string name, double longitude, double lattitude, int chargeSlots)
        {
            DataSource.baseStations[DataSource.Config.firstBaseStation++] = new() { ChargeSlots = chargeSlots, Id =id, Name = name, Lattitude = lattitude, Longitude = longitude };
        }
        
        public void add_drone(int id, string model, WeightCategories maxWeight, DroneStatuses status, double battery)
        {
            DataSource.drones[DataSource.Config.firstDrone++] = new() { Battery = battery, Id = id, MaxWeight = maxWeight, Model = model, Status = status };
        }
       
        public void add_customer(int id, string name, string phone, double longitude, double latitude)
        {
            DataSource.customers[DataSource.Config.firstCustomer++] = new() { Id = id, Name = name, Phone = phone, Latitude = latitude, Longitude =longitude };
        }
       
        public void add_parcel(int my_id, int my_senderId, int my_targetId, WeightCategories my_Weight, Priorities my_Priority, int my_DroneId, DateTime my_Scheduled, DateTime my_PickedUp, DateTime my_Delivered)
        {
            DataSource.parcels[DataSource.Config.firstParcel++] = new() { Id = my_id, Weight = my_Weight, targetId =my_targetId, senderId = my_senderId, Priority = my_Priority, DroneId = my_DroneId, Delivered = my_Delivered, PickedUp = my_PickedUp, Scheduled = my_Scheduled };

        }

        public void parcel_to_drone(int my_id,int my_DroneId )
        {
            int i=0;
            for(i=0;  DataSource.parcels[i].Id!=my_id&&i<=DataSource.Config.firstParcel;i++)
            {
            }
            if(i<DataSource.Config.firstParcel)
            {
                DataSource.parcels[i].DroneId=my_DroneId;
            }

        }
        
         public void parcel_pick_up(int my_id)
         {
            int i=0;
            for(i=0;  DataSource.parcels[i].Id!=my_id&&i<=DataSource.Config.firstParcel;i++)
            {
            }
            if(i<DataSource.Config.firstParcel)
            {
                DataSource.parcels[i].PickedUp=DateTime.Now;
            }

         }
       
        public void delivered_to_target(int my_id)
        {
            int i=0;
            for(i=0;  DataSource.parcels[i].Id!=my_id&&i<=DataSource.Config.firstParcel;i++)
            {
            }
            if(i<DataSource.Config.firstParcel)
            {
                DataSource.parcels[i].Delivered=DateTime.Now;
            }

        }


        
        DalObject()
        {
            DataSource.Initialize();
        }
    }
}