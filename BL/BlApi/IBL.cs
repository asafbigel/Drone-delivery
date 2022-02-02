using BO;
using System;
using System.Collections.Generic;

namespace BlApi
{
    public interface IBL
    {
        /// <summary>
        /// fanction that add new baseStation
        /// </summary>
        /// <param name="baseStation"> the new baseStation to add </param>
        void AddBaseStation(BaseStation baseStation);
        /// <summary>
        /// fanction that add new Customer
        /// </summary>
        /// <param name="customer">the new customer to add</param>
        void AddCustomer(Customer customer);
        /// <summary>
        /// fanction that add new Drone
        /// </summary>
        /// <param name="drone">the new drone to add</param>
        /// <param name="baseStationNum"> the id of the baseStationof the new drone </param>
        void AddDrone(Drone drone, int baseStationNum);
        /// <summary>
        /// fanction that add new parcel
        /// </summary>
        /// <param name="parcel">the new parcel to add</param>
        /// <returns></returns>
        int AddParcel(Parcel parcel);
        /// <summary>
        /// fanction that find parcel and Connect the Parcel To the Drone
        /// </summary>
        /// <param name="droneId"> the drone id</param>
        void ConnectParcelToDrone(int droneId);
        /// <summary>
        /// fanction that Delivered Parcel By Drone
        /// </summary>
        /// <param name="droneId">the drone id</param>
        void DeliveredParcelByDrone(int droneId);
        /// <summary>
        /// fanction that remove Drone From harge
        /// </summary>
        /// <param name="droneId"> the drone id </param>
        void DroneFromCharge(int droneId);
        /// <summary>
        /// fanction that Picked Up Parcel from the sender By Drone
        /// </summary>
        /// <param name="droneId">the drone id </param>
        void PickedUpParcelByDrone(int droneId);
        /// <summary>
        /// fanction that Send Drone To Charge
        /// </summary>
        /// <param name="id">the drone id</param>
        void send_drone_to_charge(int id);
        /// <summary>
        /// Tostring of all the Customers 
        /// </summary>
        /// <returns>the Tostring of all the Customers </returns>
        string StringAllCustomers();
        /// <summary>
        ///  Tostring of all the Drones
        /// </summary>
        /// <returns> the Tostring of all the Drones</returns>
        string StringAllDrones();
        /// <summary>
        /// Tostring of all the Parcels
        /// </summary>
        /// <returns>the  Tostring of all the Parcels</returns>
     //   string StringAllParcels();
        /// <summary>
        ///Tostring of all the Parcels that Without Parcels
        /// </summary>
        /// <returns>the Tostring of all the Parcels that Without Parcels</returns>
      //  string StringAllParcelsWithoutDrone();

        string StringCustomer(int customer_id);
        string StringDrone(int drone_id);
        IEnumerable<BaseStationToList> GetAllBaseStations(Predicate<BaseStationToList> match);
        bool CheckManagerLogin(string userName, string password);
        bool CheckCustomerLogin(int id, string password);
        IEnumerable<ParcelToList> GetAllParcels(Predicate<ParcelToList> match);
        IEnumerable<DroneToList> GetAllDrones(Predicate<DroneToList> match);
        IEnumerable<CustomerToList> GetAllCustomers(Predicate<CustomerToList> match);
        string StringParcel(int parcel_id);
        string StringAllBaseStations();
        string StringAllBaseStationsWithFreeSlots();
        Drone GetDrone(DroneToList drone);
        DroneToList GetDroneToList(int id);
        Customer GetCustomer(CustomerToList customer);
        CustomerAtParcel GetCustomerAtParcel(int id);
        Customer GetCustomer(int id);
        BaseStation GetBaseStation(int id);
        //string StringBaseStation(int baseStation_id);
        void UpdateBaseStation(int id, string new_name, string new_slot);
        void UpdateCustomer(int id, string new_name, string new_phone, String password , double lattitude, double longitude);
        void UpdateCustomer(Customer customer);
        void updateParcel(Parcel parcel);
         void UpdateBaseStation(BaseStation baseStation);
        void UpdateModelDrone(int drone_id, string model);
        void DeleteParcel(int id);
        ParcelAtTransfer GetCurrectParcelAtTransferOfDrone(int id);
        Parcel GetCurrectParcelOfDrone(int id);
        void auto(int id, Action c, Func<bool> f);
        Parcel GetParcel(ParcelAtCustomer parcel);
        Parcel GetParcel(ParcelToList parcel);
        Parcel GetParcel(int id);
        bool CheckDate(DateTime from, DateTime until);
    }
}