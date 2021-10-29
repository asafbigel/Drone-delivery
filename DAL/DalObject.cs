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
            DataSource.baseStations[DataSource.Config.firstBaseStation++] = new() { ChargeSlots = chargeSlots, Id = id, Name = name, Lattitude = lattitude, Longitude = longitude };
        }

        public void add_drone(int id, string model, WeightCategories maxWeight, DroneStatuses status, double battery)
        {
            DataSource.drones[DataSource.Config.firstDrone++] = new() { Battery = battery, Id = id, MaxWeight = maxWeight, Model = model, Status = status };
        }

        public void add_customer(int id, string name, string phone, double longitude, double latitude)
        {
            DataSource.customers[DataSource.Config.firstCustomer++] = new() { Id = id, Name = name, Phone = phone, Latitude = latitude, Longitude = longitude };
        }

        public void add_parcel(int my_id, int my_senderId, int my_targetId, WeightCategories my_Weight, Priorities my_Priority, int my_DroneId, DateTime my_Scheduled, DateTime my_PickedUp, DateTime my_Delivered)
        {
            DataSource.parcels[DataSource.Config.firstParcel++] = new() { Id = my_id, Weight = my_Weight, targetId = my_targetId, senderId = my_senderId, Priority = my_Priority, DroneId = my_DroneId, Delivered = my_Delivered, PickedUp = my_PickedUp, Scheduled = my_Scheduled };

        }

        public void parcel_to_drone(int my_id, int my_DroneId)
        {
            int i = find_parcel(my_id);
            DataSource.parcels[i].DroneId = my_DroneId;
            DataSource.parcels[i].Scheduled = DateTime.Now;
        }

        public void parcel_pick_up(int my_id)
        {
            int i = find_parcel(my_id);
            DataSource.parcels[i].PickedUp = DateTime.Now;
        }

        public void parcel_delivered_to_target(int my_id)
        {
            int i = find_parcel(my_id);
            DataSource.parcels[i].Delivered = DateTime.Now;
        }



        public void drone_to_charge(int base_station_id, int drone_id)
        {
            int i_baseStation = find_baseStation(base_station_id);
            DataSource.baseStations[i_baseStation].ChargeSlots--;
            int i_droneCharge = find_droneCharge_by_drone(0);
            DataSource.droneCharges[i_droneCharge] = new() { DroneId = drone_id, StationId = base_station_id };
            // change status of the drone to "maintenance"
            DataSource.drones[find_drone(drone_id)].Status = (DroneStatuses)1;
        }

        public void drone_from_charge(int drone_id)
        {
            int index_droneCharge = find_droneCharge_by_drone(drone_id);
            int station = DataSource.droneCharges[index_droneCharge].StationId;
            int index_station = find_baseStation(station);
            DataSource.baseStations[index_station].ChargeSlots++;
            DataSource.droneCharges[index_droneCharge].DroneId = 0;
            DataSource.droneCharges[index_droneCharge].StationId = 0;
           // change status of the drone to "vacant"
           DataSource.drones[i].Status = (DroneStatuses)0;
        }




        public void print_baseStation(int my_id)
        {

            if (i < DataSource.Config.firstBaseStation)
            {
                Console.WriteLine(DataSource.baseStations[i]);
            }
        }

        public void print_drone(int my_id)
        {
            int i = 0;
            for (;i < DataSource.Config.firstDrone; i++)
            {
                if (DataSource.drones[i].Id == my_id)
                    break;
            }
            if (i < DataSource.Config.firstDrone)
            {
                Console.WriteLine(DataSource.drones[i]);
            }
        }

        public void print_customer(int my_id)
        {
            int i = 0;
            for (;i <= DataSource.Config.firstCustomer; i++)
            {
                if (DataSource.customers[i].Id == my_id)
                    break;
            }
            if (i < DataSource.Config.firstCustomer)
            {
                Console.WriteLine(DataSource.customers[i]);
            }
        }

        public void print_parcel(int my_id)
        {
            int i = 0;
            for (; i < DataSource.Config.firstParcel; i++)
            {
                if (DataSource.parcels[i].Id == my_id)
                    break;
            }
            if (i < DataSource.Config.firstParcel)
            {
                Console.WriteLine(DataSource.parcels[i]);
            }
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

        public int find_parcel(int my_id)
        {
            for (int i = 0; i < DataSource.Config.firstParcel; i++)
            {
                if (DataSource.parcels[i].Id == my_id)
                    return i;
            }
            return -1;
        }
        public int find_baseStation(int my_id)
        {
            for (int i = 0; i < DataSource.Config.firstBaseStation; i++)
            {
                if (DataSource.baseStations[i].Id == my_id)
                    return i;
            }
            return -1;
        }
        public int find_customer(int my_id)
        {
            for (int i = 0; i < DataSource.Config.firstCustomer; i++)
            {
                if (DataSource.customers[i].Id == my_id)
                    return i;
            }
            return -1;
        }
        public int find_drone(int my_id)
        {
            for (int i = 0; i < DataSource.Config.firstDrone; i++)
            {
                if (DataSource.drones[i].Id == my_id)
                    return i;
            }
            return -1;
        }
        public int find_droneCharge_by_drone(int my_drone_id)
        {
            for (int i = 0; i < DataSource.Config.firstDroneCharge; i++)
            {
                if (DataSource.droneCharges[i].DroneId == my_drone_id)
                    return i;
            }
            return -1;
        }




        DalObject()
        {
            DataSource.Initialize();
        }
    }
}