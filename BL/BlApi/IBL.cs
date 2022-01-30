using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BlApi
{
    public interface IBL
    {
        void Add_base_station(BaseStation baseStation);
        void Add_customer(Customer customer);
        void Add_drone(Drone drone, int baseStation_num);
        //int Add_parcel(Parcel parcel, int sender_id, int getter_id);
        int AddParcel(Parcel parcel);
        void connect_parcel_to_drone(int drone_id);
        void delivered_parcel_by_drone(int drone_id);
        void drone_from_charge(int drone_id);
        void pickedUp_parcel_by_drone(int drone_id);
        void send_drone_to_charge(int id);
        string StringAllCustomers();
        string StringAllDrones();
        string StringAllParcels();
        
        string StringAllParcelsWithout_drone();
        string StringCustomer(int customer_id);
        string StringDrone(int drone_id);
        IEnumerable<BaseStationToList> GetAllBaseStations(Predicate<BaseStationToList> match);
        bool CheckManagerLogin(string userName, string password);
        bool CheckCustomerLogin(int id, string password);
        IEnumerable<ParcelToList> GetAllParcels(Predicate<ParcelToList> match);
        IEnumerable<DroneToList> GetAllDrones(Predicate<DroneToList> match);
        IEnumerable<CustomerToList> GetAllCustomers(Predicate<CustomerToList> match);
        string StringParcel(int parcel_id);
        string string_all_baseStations();
        string string_all_baseStations_with_free_slots();
        Drone GetDrone(DroneToList drone);
        DroneToList GetDroneToList(int id);
        Customer GetCustomer(CustomerToList customer);
        CustomerAtParcel GetCustomerAtParcel(int id);
        Customer GetCustomer(int id);
        BaseStation GetBaseStation(int id);
        string string_baseStation(int baseStation_id);
        void update_baseStation(int id, string new_name, string new_slot);
        void update_customer(int id, string new_name, string new_phone, String password , double lattitude, double longitude);
        void updateCustomer(Customer customer);
        void updateParcel(Parcel parcel);
         void UpdateBaseStation(BaseStation baseStation);
        void update_model_drone(int drone_id, string model);
        void DelsteParcel(int id);
        ParcelAtTransfer GetCurrectParcelAtTransferOfDrone(int id);
        Parcel GetCurrectParcelOfDrone(int id);
        void auto(int id, Action c, Func<bool> f);
        Parcel GetParcel(ParcelAtCustomer parcel);
        Parcel GetParcel(ParcelToList parcel);
        Parcel GetParcel(int id);
        bool CheckDate(DateTime from, DateTime until);
    }
}