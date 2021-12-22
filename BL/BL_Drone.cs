using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi
{
    public partial class BL
    {
        static Random rand = new Random();
        /// <summary>
        ///  A function that add a new drone
        /// </summary>
        /// <param name="drone">  new drone to add</param>
        /// <param name="baseStation_num"> the num of the baseStation of the new drone   </param>
        public void Add_drone(Drone drone, int baseStation_num)
        {
            drone.Battery = rand.Next(20, 41);
            drone.Status = DroneStatuses.maintenance;
            DO.BaseStation baseStation = mydal.Find_baseStation(baseStation_num);
            drone.DroneLocation = new Location();
            drone.DroneLocation.longitude = baseStation.Longitude;
            drone.DroneLocation.latitude = baseStation.Lattitude;
            my_drones.Add(convertor3(drone));
            DO.Drone idalDrone = convertor(drone);
            mydal.Add_drone(idalDrone);
            DO.DroneCharge droneCharge = new DO.DroneCharge();
            droneCharge.DroneId = drone.Id;
            droneCharge.StationId = baseStation.Id;
            mydal.Add_DroneCharge(droneCharge);
        }
        /// <summary>
        /// A function that update model of drone
        /// </summary>
        /// <param name="drone_id"> the id of the drone </param>
        /// <param name="model"> the new model of the drone </param>
        public void update_model_drone(int drone_id, string model)
        {
            DO.Drone my_drone = mydal.Find_drone(drone_id);
            my_drone.Model = model;
            mydal.UpdateDrone(my_drone);
            my_drones.Find(item => item.Id == drone_id).Model = model;
        }
        /// <summary>
        /// A function that send drone to charge
        /// </summary>
        /// <param name="id"> the id of the drone </param>
        public void send_drone_to_charge(int id)
        {
            DroneToList drone = my_drones.Find(item => item.Id == id);
            if (drone == null)
                throw new DroneException("Id not faund");
            if (drone.Status != DroneStatuses.vacant)
                throw new DroneException("Drone not vacant");
            List<BaseStation> baseStations = convertor(mydal.Get_all_base_stations(x => x.ChargeSlots>0));
            BaseStation baseStation = BaseStation_close_to_location(baseStations, drone.DroneLocation);
            double needen_fual = distance_between_2_points(baseStation.BaseStationLocation, drone.DroneLocation) * Electricity_free;
            if (needen_fual > drone.Battery)
                throw new DroneException("Not enaugh foul");
            drone.Battery -= needen_fual;
            drone.DroneLocation = baseStation.BaseStationLocation;
            drone.Status = DroneStatuses.maintenance;
            DO.DroneCharge charge = new DO.DroneCharge();
            charge.DroneId = drone.Id;
            charge.StationId = baseStation.Id;
            mydal.send_drone_to_charge(charge);
        }
        /// <summary>
        /// A function that remove drone to charge
        /// </summary>
        /// <param name="drone_id"> the id of the drone </param>
        /// <param name="time"> the time that the drone was in charge</param>
        public void drone_from_charge(int drone_id, double time)
        {
            DroneToList drone = my_drones.Find(item => item.Id == drone_id);
            if (drone == null)
                throw new DroneException("Id not found");
            if (drone.Status != DroneStatuses.maintenance)
                throw new DroneException("The drone isn't maintenance");
            drone.Battery += time * Charge_at_hour;
            if (drone.Battery > 100)
                drone.Battery = 100;
                drone.Status = DroneStatuses.vacant;
            mydal.put_out_drone_from_charge(drone.Id);
        }
        /// <summary>
        /// A function that returns the ToString of the drone
        /// </summary>
        /// <param name="drone_id"> the id of the drone </param>
        /// <returns> ToString of the drone </returns>
        public string StringDrone(int drone_id)
        {
            return convertor(mydal.Find_drone(drone_id)).ToString();
        }
        /// <summary>
        /// A function that returns the ToString of the list of all the drone
        /// </summary>
        /// <returns>  ToString of the list of all the drone </returns>
        public string StringAllDrones()
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

        public List<DroneToList> GetAllDrones(Predicate<DroneToList> match)
        {
            return convertor(mydal.Get_all_drones()).FindAll(match);
        }
    }
}