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
                throw new DroneChargeExeption("id allready exist");
            DataSource.Parcels.Add(parcel);
        }
        public void Add_DroneCharge(DroneCharge droneCharge)
        {
            if (DataSource.DroneCharges.Any(dr => dr.DroneId == droneCharge.DroneId))
                throw new DroneChargeExeption("drone id allready exist");
            DataSource.DroneCharges.Add(droneCharge);
        }
        #endregion

        #region Get an object, and update the lists (public)
        public void UpdateBaseStation(BaseStation baseStation)
        {
            int i = find_index_baseStation(baseStation.Id);
            DataSource.BaseStations[i] = baseStation;
        }
        public void UpdateDrone(Drone drone)
        {
            int i = find_index_drone(drone.Id);
            DataSource.Drones[i] = drone;
        }
        public void UpdateParcel(Parcel parcel)
        {
            int i = find_index_parcel(parcel.Id);
            DataSource.Parcels[i] = parcel;
        }
        public void UpdateCustomer(Customer customer)
        {
            int i = find_index_customer(customer.Id);
            DataSource.Customers[i] = customer;
        }
        public void UpdateDroneCharge(DroneCharge droneCharge, int DroneId)
        {
            int i = find_index_droneCharge_by_drone(DroneId);
            DataSource.DroneCharges[i] = droneCharge;
        }
        #endregion

        #region Get id of object, and find his index at the array (private)
        private int find_index_parcel(int my_id)
        {
            for (int i = 0; i < DataSource.Parcels.Count; i++)
            {
                if (DataSource.Parcels[i].Id == my_id)
                    return i;
            }
            return -1;
        }
        private int find_index_baseStation(int my_id)
        {
            for (int i = 0; i < DataSource.BaseStations.Count; i++)
            {
                if (DataSource.BaseStations[i].Id == my_id)
                    return i;
            }
            return -1;
        }
        private int find_index_customer(int my_id)
        {
            for (int i = 0; i < DataSource.Customers.Count; i++)
            {
                if (DataSource.Customers[i].Id == my_id)
                    return i;
            }
            return -1;
        }
        private int find_index_drone(int my_id)
        {
            for (int i = 0; i < DataSource.Drones.Count; i++)
            {
                if (DataSource.Drones[i].Id == my_id)
                    return i;
            }
            return -1;
        }
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

        // ctor
        public DalObject()
        {
            DataSource.Initialize();
        }

        #region Return array of all of the objects (public)
        public IEnumerable<BaseStation> Get_all_base_stations()
        {
            return DataSource.BaseStations;
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
        public IEnumerable<Parcel> Get_all_parcels()
        {
            return DataSource.Parcels;
        }
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