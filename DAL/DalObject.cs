using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;


namespace DalObject
{
   
    class DalObject : IDal
    {
        static readonly DalObject instance = new DalObject();
        // The public Instance property to use 
        public static DalObject Instance { get { return instance; } }

        // Explicit static constructor to ensure instance initialization
        // is done just before first usage
        static DalObject()
        {
            DataSource.Initialize();
        }

        DalObject() { } // default => private

        #region Get a object, and add to the lists (public)
        /// <summary>
        /// Add base station
        /// </summary>
        /// <param name="baseStation">the base station to  add </param>
        public void Add_base_station(BaseStation baseStation)
        {
            if (DataSource.BaseStations.Any(bs => bs.Id == baseStation.Id))
            {
                throw new BaseStationExeption("id allready exist");
            }
            DataSource.BaseStations.Add(baseStation);
        }
        /// <summary>
        /// Add new base drone
        /// </summary>
        /// <param name="drone"> the drone to  add</param>
        public void Add_drone(Drone drone)
        {
            if (DataSource.Drones.Any(dr => dr.Id == drone.Id))
                throw new DroneExeption("id allready exist");
            DataSource.Drones.Add(drone);
        }
        /// <summary>
        /// Add new customer
        /// </summary>
        /// <param name="customer">the customer to  add</param>
        public void Add_customer(Customer customer)
        {
            if (DataSource.Customers.Any(cs => cs.Id == customer.Id))
                throw new CustomerExeption("id allready exist");
            DataSource.Customers.Add(customer);
        }
        /// <summary>
        /// Add new parcel
        /// </summary>
        /// <param name="parcel">the parcel to  add</param>
        public void Add_parcel(Parcel parcel)
        {
            if (DataSource.Parcels.Any(pr => pr.Id == parcel.Id))
                throw new DroneChargeExeption("id allready exist");
            DataSource.Parcels.Add(parcel);
        }
        /// <summary>
        /// Add new parcel Drone Charge
        /// </summary>
        /// <param name="droneCharge"> the Drone Charge to  add</param>
        public void Add_DroneCharge(DroneCharge droneCharge)
        {
            if (DataSource.DroneCharges.Any(dr => dr.DroneId == droneCharge.DroneId))
                throw new DroneChargeExeption("drone id allready exist");
            DataSource.DroneCharges.Add(droneCharge);
        }
        #endregion

        #region Get an object, and update the lists (public)
        /// <summary>
        /// Update Base Station
        /// </summary>
        /// <param name="baseStation"> the Drone Charge to update</param>
        public void UpdateBaseStation(BaseStation baseStation)
        {
            int i = find_index_baseStation(baseStation.Id);
            DataSource.BaseStations[i] = baseStation;
        }
        /// <summary>
        /// Update Drone
        /// </summary>
        /// <param name="drone"> the Drone to update</param>
        public void UpdateDrone(Drone drone)
        {
            int i = find_index_drone(drone.Id);
            DataSource.Drones[i] = drone;
        }
        /// <summary>
        /// Update Parcel
        /// </summary>
        /// <param name="parcel"> the Parcel to update </param>
        public void UpdateParcel(Parcel parcel)
        {
            int i = find_index_parcel(parcel.Id);
            DataSource.Parcels[i] = parcel;
        }
        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="customer"> the Customer to update</param>
        public void UpdateCustomer(Customer customer)
        {
            int i = find_index_customer(customer.Id);
            DataSource.Customers[i] = customer;
            
        }
        /// <summary>
        /// Update Drone Charge
        /// </summary>
        /// <param name="droneCharge">the drone Charge to update</param>
        /// <param name="DroneId"> the id of the drone</param>
        public void UpdateDroneCharge(DroneCharge droneCharge, int DroneId)
        {
            int i = find_index_droneCharge_by_drone(DroneId);
            DataSource.DroneCharges[i] = droneCharge;
        }
        #endregion

        #region Get id of object, and find his index at the array (private)
        /// <summary>
        /// find index parcel in list
        /// </summary>
        /// <param name="my_id">id of parcel</param>
        /// <returns>index</returns>
        private int find_index_parcel(int my_id)
        {
            for (int i = 0; i < DataSource.Parcels.Count; i++)
            {
                if (DataSource.Parcels[i].Id == my_id)
                    return i;
            }
            return -1;
        }
        /// <summary>
        ///  find index baseStation
        /// </summary>
        /// <param name="my_id">id of baseStation< /param>
        /// <returns>index </returns>
        private int find_index_baseStation(int my_id)
        {
            for (int i = 0; i < DataSource.BaseStations.Count; i++)
            {
                if (DataSource.BaseStations[i].Id == my_id)
                    return i;
            }
            return -1;
        }
        /// <summary>
        ///  find index customer
        /// </summary>
        /// <param name="my_id">id of customer< /param>
        /// <returns>index </returns>
        private int find_index_customer(int my_id)
        {
            for (int i = 0; i < DataSource.Customers.Count; i++)
            {
                if (DataSource.Customers[i].Id == my_id)
                    return i;
            }
            return -1;
        }
        /// <summary>
        ///  find index drone
        /// </summary>
        /// <param name="my_id">id of drone < /param>
        /// <returns>index </returns>
        private int find_index_drone(int my_id)
        {
            for (int i = 0; i < DataSource.Drones.Count; i++)
            {
                if (DataSource.Drones[i].Id == my_id)
                    return i;
            }
            return -1;
        }
        /// <summary>
        ///  find index droneCharge
        /// </summary>
        /// <param name="my_id">id of drone < /param>
        /// <returns>index </returns>
        private int find_index_droneCharge_by_drone(int my_drone_id)
        {
            for (int i = 0; i < DataSource.DroneCharges.Count; i++)
            {
                if (DataSource.DroneCharges[i].DroneId == my_drone_id)
                    return i;
            }
            return -1;
        }
        #endregion

        #region Get an id of object, and return the object (public)
        
        public Parcel Find_parcel(int my_id)
        {
            for (int i = 0; i < DataSource.Parcels.Count(); i++)
            {
                if (DataSource.Parcels[i].Id == my_id)
                    return DataSource.Parcels[i];
            }
            return new Parcel();
        }
        public DroneCharge Find_drone_charge(int my_drone_id)
        {
            for (int i = 0; i < DataSource.DroneCharges.Count; i++)
            {
                if (DataSource.DroneCharges[i].DroneId == my_drone_id)
                    return DataSource.DroneCharges[i];
            }
            return new DroneCharge();
        }
        public BaseStation Find_baseStation(int my_id)
        {
            for (int i = 0; i < DataSource.BaseStations.Count; i++)
            {
                if (DataSource.BaseStations[i].Id == my_id)
                    return DataSource.BaseStations[i];
            }
            throw new BaseStationExeption("id not found");
        }
        public Customer Find_customer(int my_id)
        {
            for (int i = 0; i < DataSource.Customers.Count; i++)
            {
                if (DataSource.Customers[i].Id == my_id)
                    return DataSource.Customers[i];
            }
            return new Customer();
        }
        public Drone Find_drone(int my_id)
        {
            for (int i = 0; i < DataSource.Drones.Count; i++)
            {
                if (DataSource.Drones[i].Id == my_id)
                    return DataSource.Drones[i];
            }
            return new Drone();
        }
        public DroneCharge Find_droneCharge_by_drone(int my_drone_id)
        {
            for (int i = 0; i < DataSource.DroneCharges.Count; i++)
            {
                if (DataSource.DroneCharges[i].DroneId == my_drone_id)
                    return DataSource.DroneCharges[i];
            }

            return new DroneCharge();
        }
        #endregion

        #region Get a predicate and return a item
        public Parcel Find_parcel(Predicate<Parcel> match)
        {
            return DataSource.Parcels.Find(match);
        }
        #endregion



        #region Return array of all of the objects (public)
        public IEnumerable<BaseStation> Get_all_base_stations(Predicate<BaseStation> match) 
        {
            return DataSource.BaseStations.FindAll(match);
        }
        public IEnumerable<DroneCharge> Get_all_DroneCharge()
        {
            return DataSource.DroneCharges;
        }
        public IEnumerable<Drone> Get_all_drones()
        {
            return DataSource.Drones;
        }
        public IEnumerable<Customer> Get_all_customers()
        {
            return DataSource.Customers;
        }
        public IEnumerable<Parcel> Get_all_parcels(Predicate<Parcel> match)
        {
            return DataSource.Parcels.FindAll(match);
        }
        /*
        public IEnumerable<Parcel> Get_all_parcels_that_have_not_yet_been_connect_to_drone()
        {
            List<Parcel> all_parcels = DataSource.Parcels;
            List<Parcel> returned_all_parcels = new List<Parcel>();
            foreach (var item in all_parcels)
            {
                if (item.DroneId == 0)
                    returned_all_parcels.Add(item);
            }
            return returned_all_parcels;
        }
        public IEnumerable<BaseStation> Get_all_base_stations_with_free_charge_slot()
        {
            List<BaseStation> all_BaseStations = DataSource.BaseStations;
            List<BaseStation> returned_all_BaseStation = new List<BaseStation>();
            foreach (var item in all_BaseStations)
            {
                if (item.ChargeSlots > 0)
                    returned_all_BaseStation.Add(item);
            }
            return returned_all_BaseStation;
        }
        */
        #endregion

        #region Return and update the run number of the parcels
        public int GetAndUpdateRunNumber()
        {
            return DataSource.Config.runNumOfParcel++;
        }
        #endregion

        public double[] ElectricityUse()
        {
            double[] arr = new double[]
            {
                DataSource.Config.Electricity_free,
                DataSource.Config.Electricity_light,
                DataSource.Config.Electricity_medium,
                DataSource.Config.Electricity_heavy,
                DataSource.Config.Charge_at_hour
            };
            return arr;
    }

        public void send_drone_to_charge(DroneCharge droneCharge)
        {
            BaseStation baseStation = Find_baseStation(droneCharge.StationId);
            Drone drone = Find_drone(droneCharge.DroneId);
            baseStation.ChargeSlots--;
            Add_DroneCharge(droneCharge);
            UpdateBaseStation(baseStation);
            UpdateDrone(drone);
        }

        public void put_out_drone_from_charge(int my_drone_id)
        {
            DroneCharge droneCharge = Find_drone_charge(my_drone_id);
            int my_baseStation_id = droneCharge.StationId;
            BaseStation baseStation = Find_baseStation(my_baseStation_id);
            baseStation.ChargeSlots++;
            UpdateBaseStation(baseStation);
            DataSource.DroneCharges.Remove(droneCharge);
            //int DroneId = droneCharge.DroneId;
            //Drone drone = Find_drone(my_drone_id);
            //UpdateDroneCharge(droneCharge, DroneId);
        }
    }

}