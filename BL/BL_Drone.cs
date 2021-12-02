using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public partial class BL
    {
        Random rand = new Random();
        public void Add_drone(Drone drone, int baseStation_num)
        {
            drone.Battery = rand.Next(20, 41);
            drone.Status = DroneStatuses.maintenance;
            IDAL.DO.BaseStation baseStation = mydal.Find_baseStation(baseStation_num);
            drone.DroneLocation.longitude = baseStation.Longitude;
            drone.DroneLocation.latitude = baseStation.Lattitude;
            my_drones.Add(convertor1(drone));
            IDAL.DO.Drone idalDrone = convertor(drone);
            mydal.Add_drone(idalDrone);
            IDAL.DO.DroneCharge droneCharge = new IDAL.DO.DroneCharge();
            droneCharge.DroneId = drone.Id;
            droneCharge.StationId = baseStation.Id;
            mydal.Add_DroneCharge(droneCharge);
        }
        public void update_model_drone(int drone_id, string model)
        {
            IDAL.DO.Drone my_drone = mydal.Find_drone(drone_id);
            my_drone.Model = model;
            mydal.UpdateDrone(my_drone);
        }
        public void send_drone_to_charge(int id)
        {
            DroneToList drone = my_drones.Find(item => item.Id == id);
            if (drone == null)
                throw new DroneException("Id not faund");
            if (drone.Status != DroneStatuses.vacant)
                throw new DroneException("Drone not vacant");
            List<BaseStation> baseStations = convertor(mydal.Get_all_base_stations_with_free_charge_slot());
            BaseStation baseStation = BaseStation_close_to_location(baseStations, drone.DroneLocation);
            double needen_fual = distance_between_2_points(baseStation.BaseStationLocation, drone.DroneLocation) * Electricity_free;
            if (needen_fual > drone.Battery)
                throw new DroneException("Not enaugh foul");
            drone.Battery -= needen_fual;
            drone.DroneLocation = baseStation.BaseStationLocation;
            drone.Status = DroneStatuses.maintenance;
            IDAL.DO.DroneCharge charge = new IDAL.DO.DroneCharge();
            charge.DroneId = drone.Id;
            charge.StationId = baseStation.Id;
            mydal.send_drone_to_charge(charge);
        }
        public void drone_from_charge(int drone_id, double time)
        {
            DroneToList drone = my_drones.Find(item => item.Id == drone_id);
            if (drone == null)
                throw new DroneException("Id not found");
            if (drone.Status != DroneStatuses.maintenance)
                throw new DroneException("The drone isn't maintenance");
            drone.Battery += time * Charge_at_hour;
            drone.Status = DroneStatuses.vacant;
            mydal.put_out_drone_from_charge(drone.Id);
        }
        public void print_drone(int drone_id)
        {
            Console.WriteLine(convertor(mydal.Find_drone(drone_id)));
        }
        public void print_all_drones()
        {
            List<DroneToList> drones = convertor(mydal.Get_all_drones());
            foreach (var item in drones)
            {
                Console.WriteLine(item);
            }
        }
    }
}