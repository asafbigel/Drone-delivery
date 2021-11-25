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
            drone.Space.longitude = baseStation.Longitude;
            drone.Space.latitude = baseStation.Lattitude;
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
            BaseStation baseStation = BaseStation_close_to_location(baseStations, drone.location);
            double needen_fual = distance_between_2_points(baseStation.space, drone.location) * Electricity_free;
            if (needen_fual > drone.Battery)
                throw new DroneException("Not enaugh foul");
            drone.Battery -= needen_fual;
            drone.location = baseStation.space;
            drone.Status = DroneStatuses.maintenance;
            IDAL.DO.DroneCharge charge = new IDAL.DO.DroneCharge();
            charge.DroneId = drone.Id;
            charge.StationId = baseStation.id;
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
        public void connect_parcel_to_drone(int drone_id)
        {
            IDAL.DO.Drone idalDrone = mydal.Find_drone(drone_id);
            DroneToList drone = my_drones.Find(item => item.Id == drone_id);
            if (drone.Status != DroneStatuses.vacant)
                throw new DroneException("The drone isn't vacant");
            List<IDAL.DO.Parcel> idalparcels = mydal.Get_all_parcels_that_have_not_yet_been_connect_to_drone().ToList();
            if (idalparcels.Count == 0)
                throw new ParcelException("No parcel exist");
            List<Parcel> parcels = convertor(idalparcels);
            List<Parcel> parcelsHighPriority = parcels.FindAll(item => item.priority == Priorities.emergency);
            if (parcelsHighPriority.Count == 0)
            {
                parcelsHighPriority = parcels.FindAll(item => item.priority == Priorities.fast);
                if (parcelsHighPriority.Count == 0)
                    parcelsHighPriority = parcels.FindAll(item => item.priority == Priorities.regular);
            }
            List<Parcel> parcelsHighWeigth = parcelsHighPriority.FindAll(item => item.weight == WeightCategories.heavy);
            if (parcelsHighWeigth.Count == 0)
            {
                parcelsHighWeigth = parcelsHighPriority.FindAll(item => item.weight == WeightCategories.medium);
                if (parcelsHighWeigth.Count == 0)
                    parcelsHighWeigth = parcelsHighPriority.FindAll(item => item.weight == WeightCategories.light);
            }
            List<ParcelAtTransfer> parcelsAtTransfer = convertor1(parcelsHighPriority);
            ParcelAtTransfer parcelAtTransfer = parcelsAtTransfer[0];
            double min_distance = distance_between_2_points(drone.location, parcelAtTransfer.spaceOfPickUp);
            double this_distance;
            foreach (var item in parcelsAtTransfer)
            {
                this_distance = distance_between_2_points(drone.location, item.spaceOfPickUp);
                if (this_distance < min_distance)
                {
                    parcelAtTransfer = item;
                    min_distance = this_distance;
                }
            }
            BaseStation baseStation = BaseStation_close_to_location(convertor(mydal.Get_all_base_stations().ToList()),parcelAtTransfer.spaceOfTarget);
            double battery_needed = 0;
            battery_needed += distance_between_2_points(drone.location, parcelAtTransfer.spaceOfPickUp) * Electricity_free;
            if(parcelAtTransfer.weight == WeightCategories.heavy)
                battery_needed += distance_between_2_points(parcelAtTransfer.spaceOfPickUp, parcelAtTransfer.spaceOfTarget) * Electricity_heavy;
            if (parcelAtTransfer.weight == WeightCategories.medium)
                battery_needed += distance_between_2_points(parcelAtTransfer.spaceOfPickUp, parcelAtTransfer.spaceOfTarget) * Electricity_medium;
            if (parcelAtTransfer.weight == WeightCategories.light)
                battery_needed += distance_between_2_points(parcelAtTransfer.spaceOfPickUp, parcelAtTransfer.spaceOfTarget) * Electricity_light;
            battery_needed += distance_between_2_points(parcelAtTransfer.spaceOfTarget,baseStation.space ) * Electricity_free;

            if (battery_needed > drone.Battery)
                throw new DroneException("No enaugh battery");
            drone.Status = DroneStatuses.sending;
            IDAL.DO.Parcel parcel = mydal.Find_parcel(parcelAtTransfer.id);
            parcel.DroneId = drone.Id;
            parcel.Scheduled = DateTime.Now;
            mydal.UpdateParcel(parcel);
        }
    }
}