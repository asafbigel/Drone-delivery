using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using IDAL.DO;


namespace DalObject
{
    public class DalObject : IDal
    {
        #region Get a object, and add to the lists (public)
        public void Add_base_station(BaseStation baseStation)
        {
            if (DataSource.BaseStations.Any(bs => bs.Id == baseStation.Id))
            {
                throw new BaseStationExeption("id allready exist");
            }
            DataSource.BaseStations.Add(baseStation);
        }
        public void Add_drone(Drone drone)
        {
            if (DataSource.Drones.Any(dr => dr.Id == drone.Id))
                throw new DroneExeption("id allready exist");
            DataSource.Drones.Add(drone);
        }
        public void Add_customer(Customer customer)
        {
            if (DataSource.Customers.Any(cs => cs.Id == customer.Id))
                throw new CustomerExeption("id allready exist");
            DataSource.Customers.Add(customer);
        }
        public void Add_parcel(Parcel parcel)
        {
            if (DataSource.Parcels.Any(pr => pr.Id == parcel.Id))
                throw new ParcelExeption("id allready exist");
            DataSource.Parcels.Add(parcel);
        }
        public void Add_DroneCharge(DroneCharge droneCharge)
        {
            DataSource.droneCharges[DataSource.Config.firstDroneCharge++] = droneCharge;
        }
        #endregion

        #region Get an object, and update the lists (public)
        public void UpdateBaseStation(BaseStation baseStation)
        {
            int i = find_index_baseStation(baseStation.Id);
            DataSource.baseStations[i] = baseStation;
        }
        public void UpdateDrone(Drone drone)
        {
            int i = find_index_drone(drone.Id);
            DataSource.drones[i] = drone;
        }
        public void UpdateParcel(Parcel parcel)
        {
            int i = find_index_parcel(parcel.Id);
            DataSource.parcels[i] = parcel;
        }
        public void UpdateDroneCharge(DroneCharge droneCharge, int DroneId)
        {
            int i = find_index_droneCharge_by_drone(DroneId);
            DataSource.droneCharges[i] = droneCharge;
        }
        #endregion

        #region Get id of object, and find his index at the array (private)
        private int find_index_parcel(int my_id)
        {
            for (int i = 0; i < DataSource.Config.firstParcel; i++)
            {
                if (DataSource.parcels[i].Id == my_id)
                    return i;
            }
            return -1;
        }
        private int find_index_baseStation(int my_id)
        {
            for (int i = 0; i < DataSource.Config.firstBaseStation; i++)
            {
                if (DataSource.baseStations[i].Id == my_id)
                    return i;
            }
            return -1;
        }
        private int find_index_customer(int my_id)
        {
            for (int i = 0; i < DataSource.Config.firstCustomer; i++)
            {
                if (DataSource.customers[i].Id == my_id)
                    return i;
            }
            return -1;
        }
        private int find_index_drone(int my_id)
        {
            for (int i = 0; i < DataSource.Config.firstDrone; i++)
            {
                if (DataSource.drones[i].Id == my_id)
                    return i;
            }
            return -1;
        }
        private int find_index_droneCharge_by_drone(int my_drone_id)
        {
            for (int i = 0; i < DataSource.Config.firstDroneCharge; i++)
            {
                if (DataSource.droneCharges[i].DroneId == my_drone_id)
                    return i;
            }
            return -1;
        }
        #endregion

        #region Get an id of object, and return the object (public)
        public Parcel Find_parcel(int my_id)
        {
            for (int i = 0; i < DataSource.Config.firstParcel; i++)
            {
                if (DataSource.parcels[i].Id == my_id)
                    return DataSource.parcels[i];

            }
            return new Parcel();
        }
        public DroneCharge Find_drone_charge(int my_drone_id)
        {
            for (int i = 0; i < DataSource.Config.firstDroneCharge; i++)
            {
                if (DataSource.droneCharges[i].DroneId == my_drone_id)
                    return DataSource.droneCharges[i];
            }
            return new DroneCharge();
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
        public DroneCharge Find_droneCharge_by_drone(int my_drone_id)
        {
            for (int i = 0; i < DataSource.Config.firstDroneCharge; i++)
            {
                if (DataSource.droneCharges[i].DroneId == my_drone_id)
                    return DataSource.droneCharges[i];
            }

            return new DroneCharge();
        }
        #endregion

        // ctor
        public DalObject()
        {
            DataSource.Initialize();
        }


        #region Return the first free space at the lists (public)
        public int GetFirstFreeBaseStation()
        {
            return DataSource.Config.firstBaseStation;
        }
        public int GetFirstDrone()
        {
            return DataSource.Config.firstDrone;
        }
        public int GetFirstCustomer()
        {
            return DataSource.Config.firstCustomer;
        }
        public int GetFirstFreeParcel()
        {
            return DataSource.Config.firstParcel;
        }
        #endregion

        #region Return array of all of the objects (public)
        public BaseStation[] Get_all_base_stations()
        {
            return DataSource.baseStations;
        }
        public Drone[] Get_all_drones()
        {
            return DataSource.drones;
        }
        public Customer[] Get_all_customers()
        {
            return DataSource.customers;
        }
        public Parcel[] Get_all_parcels()
        {
            return DataSource.parcels;
        }
        #endregion

        #region Return and update the run number of the parcels
        public int GetAndUpdateRunNumber()
        {
            return DataSource.Config.runNumOfParcel++;
        }
        #endregion
    }

}