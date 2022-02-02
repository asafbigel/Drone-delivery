using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Runtime.CompilerServices;


namespace BL
{
    public partial class BL
    {
        static Random rand = new Random();
        /// <summary>
        ///  A function that add a new drone
        /// </summary>
        /// <param name="drone">  new drone to add</param>
        /// <param name="baseStationNum"> the num of the baseStation of the new drone   </param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(Drone drone, int baseStationNum)
        {
            if (drone.Id <= 0)
                throw new DroneIdException("invalid Drone id");
            lock (mydal)
            {
                drone.Battery = rand.Next(20, 41);
                drone.Status = DroneStatuses.maintenance;
                DO.BaseStation baseStation = mydal.Find_baseStation(baseStationNum);
                if (baseStation.ChargeSlots < 1)
                    throw new NoFreeChargingException("There are no free charging stations at this station");

                drone.DroneLocation = new Location();
                drone.DroneLocation.Longitude = baseStation.Longitude;
                drone.DroneLocation.Latitude = baseStation.Lattitude;
                my_drones.Add(convertor3(drone));
                DO.Drone idalDrone = convertor(drone);
                mydal.Add_drone(idalDrone);
                DO.DroneCharge droneCharge = new DO.DroneCharge();
                droneCharge.DroneId = drone.Id;
                droneCharge.StationId = baseStation.Id;
                droneCharge.EnterToCharge = DateTime.Now;

                mydal.send_drone_to_charge(droneCharge);
            }
               
        }
        /// <summary>
        /// A function that update model of drone
        /// </summary>
        /// <param name="drone_id"> the id of the drone </param>
        /// <param name="model"> the new model of the drone </param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateModelDrone(int drone_id, string model)
        {
            
            lock (mydal)
            {
                DO.Drone my_drone = mydal.Find_drone(drone_id);
                my_drone.Model = model;
                mydal.UpdateDrone(my_drone);
                my_drones.Find(item => item.Id == drone_id).Model = model;
            }
        }
        /// <summary>
        /// A function that send drone to charge
        /// </summary>
        /// <param name="id"> the id of the drone </param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SendDroneToCharge(int id)
        {
            lock (mydal)
            {
                DroneToList drone = my_drones.Find(item => item.Id == id);
                if (drone == null)
                    throw new DroneBatteryException("Id not faund");
                if (drone.Status != DroneStatuses.vacant)
                    throw new DroneBatteryException("Drone not vacant");
                List<BaseStation> baseStations = convertor(mydal.Get_all_base_stations(x => x.ChargeSlots > 0));
                BaseStation baseStation = BaseStation_close_to_location(baseStations, drone.DroneLocation);
                double needen_fual = distance_between_2_points(baseStation.BaseStationLocation, drone.DroneLocation) * Electricity_free;
                if (needen_fual > drone.Battery)
                    throw new DroneBatteryException("Not enaugh battery");
                drone.Battery -= needen_fual;
                drone.DroneLocation = baseStation.BaseStationLocation;
                drone.Status = DroneStatuses.maintenance;
                DO.DroneCharge charge = new DO.DroneCharge();
                charge.DroneId = drone.Id;
                charge.StationId = baseStation.Id;
                charge.EnterToCharge = DateTime.Now;
                mydal.send_drone_to_charge(charge);
                
            }
        }
        /// <summary>
        /// A function that remove drone to charge
        /// </summary>
        /// <param name="drone_id"> the id of the drone </param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DroneFromCharge(int drone_id)
        {
            lock (mydal)
            {
                DroneToList drone = my_drones.Find(item => item.Id == drone_id);
                if (drone == null)
                    throw new DroneBatteryException("Id not found");
                if (drone.Status != DroneStatuses.maintenance)
                    throw new DroneBatteryException("The drone isn't maintenance");
                DroneInCharging droneCharge = convertor2(drone);
                drone.Battery += (DateTime.Now - droneCharge.EnterToCharge).TotalSeconds * Charge_at_hour;
                if (drone.Battery > 100)
                    drone.Battery = 100;
                drone.Status = DroneStatuses.vacant;
                mydal.put_out_drone_from_charge(drone.Id);
            }
        }
        /// <summary>
        /// A function that returns the ToString of the drone
        /// </summary>
        /// <param name="drone_id"> the id of the drone </param>
        /// <returns> ToString of the drone </returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string StringDrone(int drone_id)
        {
            lock (mydal)
                return convertor(mydal.Find_drone(drone_id)).ToString();
        }
        /// <summary>
        /// A function that returns the ToString of the list of all the drone
        /// </summary>
        /// <returns>  ToString of the list of all the drone </returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string StringAllDrones()
        {
            lock (mydal)
            {
                List<DroneToList> drones = convertor(mydal.Get_all_drones());
                string result = "";
                foreach (var item in drones)
                {
                    result += item.ToString();
                    result += "\n";
                }
                return result;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetAllDrones(Predicate<DroneToList> match)
        {
            lock (mydal)
                return convertor(mydal.Get_all_drones()).FindAll(match);
        }
        public DroneToList GetDroneToList(int id)
        {
            var myDrone = mydal.Find_drone(id);
            Drone drone = convertor(myDrone);
            return convertor3(drone);
        }
   
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(DroneToList drone)
        {
            return convertor3(drone);
        }
        /// <summary>
        /// play the simulator
        /// </summary>
        /// <param name="id">drone's id</param>
        public void auto(int id, Action refresh, Func<bool> f)
        {
            try
            { new Simulator(this, id, refresh, f); }
            catch (Exception ex)
            {
                throw ex;
                throw new Exception("not definished");
            }
        }
    }
}