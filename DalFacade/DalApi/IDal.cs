using DO;
using System;
using System.Collections.Generic;

namespace DalApi
{
    public interface IDal
    {
        #region Add
        void Add_base_station(BaseStation baseStation);
        void Add_customer(Customer customer);
        void Add_drone(Drone drone);
        void Add_DroneCharge(DroneCharge droneCharge);
        void Add_parcel(Parcel parcel);
        #endregion
        #region Find
        BaseStation Find_baseStation(int my_id);
        Customer Find_customer(int my_id);
        Drone Find_drone(int my_id);
        DroneCharge FindDroneCharge(int my_drone_id);
        Parcel Find_parcel(int my_id);
        IEnumerable<Parcel> Get_all_parcels(Predicate<Parcel> match);
        void UpdateCustomer(Customer customer);
        #endregion
        #region Get all
        IEnumerable<BaseStation> Get_all_base_stations(Predicate<BaseStation> match);
        IEnumerable<Customer> Get_all_customers();
        IEnumerable<DroneCharge> Get_all_DroneCharge();
        IEnumerable<Drone> Get_all_drones();
        //IEnumerable<Parcel> Get_all_parcels_that_have_not_yet_been_connect_to_drone();
        //IEnumerable<BaseStation> Get_all_base_stations_with_free_charge_slot();
        #endregion
        #region Update
        void UpdateBaseStation(BaseStation baseStation);
        void UpdateDrone(Drone drone);
        //void UpdateDroneCharge(DroneCharge droneCharge, int DroneId);
        void UpdateParcel(Parcel parcel);
        #endregion
        #region delete
        public void DeleteDroneCharge(int id);
        #endregion
        int GetAndUpdateRunNumber();
         double[] ElectricityUse();
        void send_drone_to_charge(DroneCharge droneCharge);
        void put_out_drone_from_charge(int my_drone_id);
    }
}