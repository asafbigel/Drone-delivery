using System;
using System.Collections.Generic;
using DalApi;
using DO;

namespace Dal
{
    sealed class DalXml : IDal
    {
        static readonly IDal instance = new DalXml();
        public static IDal Instance { get => instance; }
        DalXml() { }

        public void Add_base_station(BaseStation baseStation)
        {
            throw new NotImplementedException();
        }
        public void DeleteParcel(int id)
        {
            throw new NotImplementedException();
        }

        public void Add_customer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void Add_drone(Drone drone)
        {
            throw new NotImplementedException();
        }

        public void Add_DroneCharge(DroneCharge droneCharge)
        {
            throw new NotImplementedException();
        }

        public void Add_parcel(Parcel parcel)
        {
            throw new NotImplementedException();
        }

        public BaseStation Find_baseStation(int my_id)
        {
            throw new NotImplementedException();
        }

        public Customer Find_customer(int my_id)
        {
            throw new NotImplementedException();
        }

        public Drone Find_drone(int my_id)
        {
            throw new NotImplementedException();
        }

        public DroneCharge Find_droneCharge_by_drone(int my_drone_id)
        {
            throw new NotImplementedException();
        }

        public DroneCharge Find_drone_charge(int my_drone_id)
        {
            throw new NotImplementedException();
        }

        public Parcel Find_parcel(int my_id)
        {
            throw new NotImplementedException();
        }

        public Parcel Find_parcel(Predicate<Parcel> match)
        {
            throw new NotImplementedException();
        }

        public void UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BaseStation> Get_all_base_stations(Predicate<BaseStation> match)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Get_all_customers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DroneCharge> Get_all_DroneCharge()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Drone> Get_all_drones()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> Get_all_parcels(Predicate<Parcel> match)
        {
            throw new NotImplementedException();
        }

        public void UpdateBaseStation(BaseStation baseStation)
        {
            throw new NotImplementedException();
        }

        public void UpdateDrone(Drone drone)
        {
            throw new NotImplementedException();
        }

        public void UpdateDroneCharge(DroneCharge droneCharge, int DroneId)
        {
            throw new NotImplementedException();
        }

        public void UpdateParcel(Parcel parcel)
        {
            throw new NotImplementedException();
        }

        public int GetAndUpdateRunNumber()
        {
            throw new NotImplementedException();
        }

        public double[] ElectricityUse()
        {
            throw new NotImplementedException();
        }

        public void send_drone_to_charge(DroneCharge droneCharge)
        {
            throw new NotImplementedException();
        }

        public void put_out_drone_from_charge(int my_drone_id)
        {
            throw new NotImplementedException();
        }
    }
}