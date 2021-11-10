using IDAL.DO;

namespace IDAL
{
    public interface IDal
    {
        #region Creat part of C.R.U.D
        void Add_base_station(BaseStation baseStation);
        void Add_customer(Customer customer);
        void Add_drone(Drone drone);
        void Add_DroneCharge(DroneCharge droneCharge);
        void Add_parcel(Parcel parcel);
        #endregion

        #region Read part of C.R.U.D
        #region Real all elements
        BaseStation[] Get_all_base_stations();
        Customer[] Get_all_customers();
        Drone[] Get_all_drones();
        Parcel[] Get_all_parcels();
        #endregion
        #region Read a single element
        BaseStation Find_baseStation(int my_id);
        Customer Find_customer(int my_id);
        Drone Find_drone(int my_id);
        DroneCharge Find_droneCharge_by_drone(int my_drone_id);
        DroneCharge Find_drone_charge(int my_drone_id);
        Parcel Find_parcel(int my_id);
        #endregion
        #endregion

        #region Update part of C.R.U.D
        void UpdateBaseStation(BaseStation baseStation);
        void UpdateDrone(Drone drone);
        void UpdateDroneCharge(DroneCharge droneCharge, int DroneId);
        void UpdateParcel(Parcel parcel);
        #endregion

        int GetAndUpdateRunNumber();        
    }
}