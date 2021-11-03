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
        public void Add_base_station(BaseStation baseStation)
        {
            DataSource.baseStations[DataSource.Config.firstBaseStation++] = baseStation;
        }

        public void Add_drone(Drone drone)
        {
            DataSource.drones[DataSource.Config.firstDrone++] = drone;
        }

        public void Add_customer(Customer customer)
        {
            DataSource.customers[DataSource.Config.firstCustomer++] = customer;
        }

        public void Add_parcel(Parcel parcel)
        {
            DataSource.parcels[DataSource.Config.firstParcel++] = parcel;

        }

       
        public void UpdateBaseStation(BaseStation baseStation)
        {
            int i = Find_index_parcel(baseStation.Id);
            DataSource.baseStations[i] = baseStation;
        }

        public void UpdateDrone(Drone drone)
        {
            int i = Find_index_parcel(drone.Id);
            DataSource.drones[i] = drone;
        }
        public void UpdateParcel(Parcel parcel)
        {
            int i = Find_index_parcel(parcel.Id);
            DataSource.parcels[i] = parcel;
        }






        public void drone_to_charge(int base_station_id, int drone_id)
        {
            int i_baseStation = Find_index_baseStation(base_station_id);
            DataSource.baseStations[i_baseStation].ChargeSlots--;
            DataSource.droneCharges[DataSource.Config.firstDroneCharge++] = new DroneCharge() { DroneId = drone_id, StationId = base_station_id };
            // change status of the drone to "maintenance"
            DataSource.drones[Find_index_drone(drone_id)].Status = (DroneStatuses)1;
        }

        public void drone_from_charge(int drone_id)
        {
            int index_droneCharge = Find_index_droneCharge_by_drone(drone_id);
            int station = DataSource.droneCharges[index_droneCharge].StationId;
            int index_station = Find_index_baseStation(station);
            int index_drone = Find_index_drone(drone_id);
            DataSource.baseStations[index_station].ChargeSlots++;
            DataSource.droneCharges[index_droneCharge].DroneId = 0;
            DataSource.droneCharges[index_droneCharge].StationId = 0;
           // change status of the drone to "vacant"
           DataSource.drones[index_drone].Status = (DroneStatuses)0;
        }


        /*


          public void Parcel_to_drone(int my_id, int my_DroneId)
         {
             int i = Find_index_parcel(my_id);
             DataSource.parcels[i].DroneId = my_DroneId;
             DataSource.parcels[i].Scheduled = DateTime.Now;
         }
        public void parcel_pick_up(int my_id)
        {
            int i = Find_index_parcel(my_id);
            DataSource.parcels[i].PickedUp = DateTime.Now;
        }

        public void parcel_delivered_to_target(int my_id)
        {
            int i = Find_index_parcel(my_id);
            DataSource.parcels[i].Delivered = DateTime.Now;
        }

         ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

         public void print_baseStation(int my_id)
         {
             int i = Find_index_baseStation(my_id);
             if (i != -1)
             Console.WriteLine(DataSource.baseStations[i]);
         }

         public void print_drone(int my_id)
         {
             int i = Find_index_drone(my_id);
             if(i!=-1)
                 Console.WriteLine(DataSource.drones[i]);
         }

         public void print_customer(int my_id)
         {
             int i = Find_index_customer(my_id);
             if (i != -1)
                 Console.WriteLine(DataSource.customers[i]);
         }

         public void print_parcel(int my_id)
         {
             int i = Find_index_parcel(my_id);
             if (i != -1)
             Console.WriteLine(DataSource.parcels[i]);

         }




         public void print_all_baseStations()
         {
             for (int i = 0; i < DataSource.Config.firstBaseStation; i++)
             {
                 Console.WriteLine(DataSource.baseStations[i].Name + ": " + DataSource.baseStations[i].Id);
             }
         }

         public void print_all_drones()
         {
             for (int i = 0; i < DataSource.Config.firstDrone; i++)
             {
                 Console.WriteLine(DataSource.drones[i].Id);
             }
         }

         public void print_all_customers()
         {
             for (int i = 0; i < DataSource.Config.firstCustomer; i++)
             {
                 Console.WriteLine(DataSource.customers[i].Name + ": " + DataSource.customers[i].Id);
             }
         }

         public void print_all_parcel()
         {
             for (int i = 0; i < DataSource.Config.firstParcel; i++)
             {
                 Console.WriteLine(DataSource.parcels[i].Id);
             }
         }



         public void print_parcel_without_drone()
         {
             for (int i =0;  i < DataSource.Config.firstParcel; i++)
             {
                 if (DataSource.parcels[i].DroneId==0)
                     Console.WriteLine(DataSource.parcels[i]);
             }
         }

         public void print_baseStation_with_free_charger()
         {
             for (int i = 0; i < DataSource.Config.firstBaseStation; i++)
             {
                 if(DataSource.baseStations[i].ChargeSlots > 0)
                     Console.WriteLine(DataSource.baseStations[i]);
             }
         }
         */

        public int Find_index_parcel(int my_id)
        {
            for (int i = 0; i < DataSource.Config.firstParcel; i++)
            {
                if (DataSource.parcels[i].Id == my_id)
                    return i;
            }
            return -1;
        }
        public int Find_index_baseStation(int my_id)
        {
            for (int i = 0; i < DataSource.Config.firstBaseStation; i++)
            {
                if (DataSource.baseStations[i].Id == my_id)
                    return i;
            }
            return -1;
        }
        public int Find_index_customer(int my_id)
        {
            for (int i = 0; i < DataSource.Config.firstCustomer; i++)
            {
                if (DataSource.customers[i].Id == my_id)
                    return i;
            }
            return -1;
        }
        public int Find_index_drone(int my_id)
        {
            for (int i = 0; i < DataSource.Config.firstDrone; i++)
            {
                if (DataSource.drones[i].Id == my_id)
                    return i;
            }
            return -1;
        }
        public int Find_index_droneCharge_by_drone(int my_drone_id)
        {
            for (int i = 0; i < DataSource.Config.firstDroneCharge; i++)
            {
                if (DataSource.droneCharges[i].DroneId == my_drone_id)
                    return i;
            }
            return -1;
        }

        public IEnumerable<BaseStation> GetAllBaseStaition()
        {
            return DataSource.baseStations.ToList();
        }

        //////////////////////////////////////////////////////////////////////////
        public Parcel Find_parcel(int my_id)
        {
            for (int i = 0; i < DataSource.Config.firstParcel; i++)
            {
                if (DataSource.parcels[i].Id == my_id)
                    return DataSource.parcels[i];

            }
            return new Parcel();
        }
        public BaseStation Find_baseStation(int my_id)
        {
            for (int i = 0; i < DataSource.Config.firstBaseStation; i++)
            {
                if (DataSource.baseStations[i].Id == my_id)
                    return DataSource.baseStations[i]; 
            }
            return new BaseStation();
        }
        public Customer Find_customer(int my_id)
        {
            for (int i = 0; i < DataSource.Config.firstCustomer; i++)
            {
                if (DataSource.customers[i].Id == my_id)
                    return DataSource.customers[i];
            }
            return new Customer();
        }
        public Drone Find_drone(int my_id)
        {
            for (int i = 0; i < DataSource.Config.firstDrone; i++)
            {
                if (DataSource.drones[i].Id == my_id)
                    return DataSource.drones[i];
            }
            return new Drone();
        }
        public DroneCharge Find_droneCharge_by_drone(int my_drone_id)//????????????????????????????
        {
            for (int i = 0; i <  DataSource.Config.firstDroneCharge; i++)
            {
                if (DataSource.droneCharges[i].DroneId == my_drone_id)
                    return DataSource.droneCharges[i];
            }

            return new DroneCharge();
        }
        public DalObject()
        {
            DataSource.Initialize();
        }
//*******************************************************************
        public int GetFirstBaseStation()
        {
           return DataSource.Config.firstBaseStation;
        }

        public BaseStation[] Get_all_base_stations()
        {
            return DataSource.baseStations;
        }
        
        public int GetFirstDrone()
        {
            return DataSource.Config.firstDrone;
        }

        public Drone[] Get_all_drones()
        {
            return DataSource.drones;
        }
        
        public int GetFirstCustomer()
        {
            return DataSource.Config.firstCustomer;
        }

        public Customer[] Get_all_customers()
        {
            return DataSource.customers;
        }

        public int GetFirstParcel()
        {
            return DataSource.Config.firstParcel;
        }

        public Parcel[] Get_all_parcels()
        {
            return DataSource.parcels;
        }

    }
}