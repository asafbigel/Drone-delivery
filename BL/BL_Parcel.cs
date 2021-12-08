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
        public void Add_parcel(Parcel parcel, int sender_id, int getter_id)
        {
            parcel.Delivered = DateTime.MinValue;
            parcel.PickedUp = DateTime.MinValue;
            parcel.Scheduled = DateTime.MinValue;
            parcel.Requested = DateTime.Now;
            parcel.Id = mydal.GetAndUpdateRunNumber();
            parcel.TheDrone = null;
            IDAL.DO.Parcel idalParcel = convertor(parcel);
            idalParcel.SenderId = sender_id;
            idalParcel.TargetId = getter_id;
            mydal.Add_parcel(idalParcel);
        }
        public void connect_parcel_to_drone(int drone_id)
        {
            IDAL.DO.Drone idalDrone = mydal.Find_drone(drone_id);
            DroneToList drone = my_drones.Find(item => item.Id == drone_id);
            if (drone.Status != DroneStatuses.vacant)
                throw new DroneException("The drone isn't vacant");
            List<IDAL.DO.Parcel> idalparcels = mydal.Get_all_parcels_that_have_not_yet_been_connect_to_drone().ToList().FindAll(item => (int)item.Weight <= (int)drone.MaxWeight);
            if (idalparcels.Count == 0)
                throw new ParcelException("No parcel exist");
            List<Parcel> parcels = convertor(idalparcels);
            List<Parcel> parcelsHighPriority = parcels.FindAll(item => item.Priority == Priorities.emergency);
            if (parcelsHighPriority.Count == 0)
            {
                parcelsHighPriority = parcels.FindAll(item => item.Priority == Priorities.fast);
                if (parcelsHighPriority.Count == 0)
                    parcelsHighPriority = parcels.FindAll(item => item.Priority == Priorities.regular);
            }
            List<Parcel> parcelsHighWeigth = parcelsHighPriority.FindAll(item => item.Weight == WeightCategories.heavy);
            if (parcelsHighWeigth.Count == 0)
            {
                parcelsHighWeigth = parcelsHighPriority.FindAll(item => item.Weight == WeightCategories.medium);
                if (parcelsHighWeigth.Count == 0)
                    parcelsHighWeigth = parcelsHighPriority.FindAll(item => item.Weight == WeightCategories.light);
            }
            List<ParcelAtTransfer> parcelsAtTransfer = convertor1(parcelsHighPriority);
            ParcelAtTransfer parcelAtTransfer = parcelsAtTransfer[0];
            double min_distance = distance_between_2_points(drone.DroneLocation, parcelAtTransfer.LocationOfPickUp);
            double this_distance;
            foreach (var item in parcelsAtTransfer)
            {
                this_distance = distance_between_2_points(drone.DroneLocation, item.LocationOfPickUp);
                if (this_distance < min_distance)
                {
                    parcelAtTransfer = item;
                    min_distance = this_distance;
                }
            }
            BaseStation baseStation = BaseStation_close_to_location(convertor(mydal.Get_all_base_stations()), parcelAtTransfer.LocationOfTarget);
            double battery_needed = 0;
            battery_needed += distance_between_2_points(drone.DroneLocation, parcelAtTransfer.LocationOfPickUp) * Electricity_free;
            if (parcelAtTransfer.Weight == WeightCategories.heavy)
                battery_needed += distance_between_2_points(parcelAtTransfer.LocationOfPickUp, parcelAtTransfer.LocationOfTarget) * Electricity_heavy;
            if (parcelAtTransfer.Weight == WeightCategories.medium)
                battery_needed += distance_between_2_points(parcelAtTransfer.LocationOfPickUp, parcelAtTransfer.LocationOfTarget) * Electricity_medium;
            if (parcelAtTransfer.Weight == WeightCategories.light)
                battery_needed += distance_between_2_points(parcelAtTransfer.LocationOfPickUp, parcelAtTransfer.LocationOfTarget) * Electricity_light;
            battery_needed += distance_between_2_points(parcelAtTransfer.LocationOfTarget, baseStation.BaseStationLocation) * Electricity_free;

            if (battery_needed > drone.Battery)
                throw new DroneException("No enaugh battery");
            drone.Status = DroneStatuses.sending;
            IDAL.DO.Parcel parcel = mydal.Find_parcel(parcelAtTransfer.Id);
            parcel.DroneId = drone.Id;
            parcel.Scheduled = DateTime.Now;
            mydal.UpdateParcel(parcel);
        }
        public void pickedUp_parcel_by_drone(int drone_id)
        {
            DroneToList drone = my_drones.Find(item => item.Id == drone_id);
            if (drone.Status != DroneStatuses.sending)
                throw new DroneException("The drone isn't at transfer");
            List<IDAL.DO.Parcel> parcels = mydal.Get_all_parcels().ToList();
            List<IDAL.DO.Parcel> parcelsOfThisDrone = parcels.FindAll(item => item.DroneId == drone.Id);
            //if (parcelsOfThisDrone.Count > 1)
            //    throw new DroneException("There are more than 1 parcels scheduled with this drone");
            if (parcelsOfThisDrone.Count == 0)
                throw new DroneException("There aren't parcel ");
            List<IDAL.DO.Parcel> parcelsNotPickedUp = parcelsOfThisDrone.FindAll(item =>item.PickedUp == DateTime.MinValue);
            if (parcelsNotPickedUp.Count == 0)
                throw new DroneException("Don't has parcel that don't picked up");
            IDAL.DO.Parcel parcel = parcelsNotPickedUp[0];
            ParcelAtTransfer parcelAtTransfer = convertor1(convertor(parcel));
            double min_battery = distance_between_2_points(drone.DroneLocation, parcelAtTransfer.LocationOfPickUp) * Electricity_free;
            if (drone.Battery < min_battery)
                throw new ParcelException("Has not enaugh battery");
            drone.Battery -= min_battery;
            drone.DroneLocation = parcelAtTransfer.LocationOfPickUp;
            parcel.PickedUp = DateTime.Now;
            mydal.UpdateParcel(parcel);
        }
        public void delivered_parcel_by_drone(int drone_id)
        {
            DroneToList drone = my_drones.Find(item => item.Id == drone_id);
            List<IDAL.DO.Parcel> parcels = mydal.Get_all_parcels().ToList();
            List<IDAL.DO.Parcel> parcelsOfThisDrone = parcels.FindAll(item => item.DroneId == drone.Id);
            List<IDAL.DO.Parcel> parcelsNotDelivered = parcelsOfThisDrone.FindAll(item => item.Delivered == DateTime.MinValue);
            if (parcelsNotDelivered.Count == 0)
                throw new DroneException("Don't has parcel that picked up and not delivered");
            IDAL.DO.Parcel parcel = parcelsNotDelivered[0];
            ParcelAtTransfer parcelAtTransfer = convertor1(convertor(parcel));
            double min_battery;
            switch (parcel.Weight)
            {
                case IDAL.DO.WeightCategories.light:
                    min_battery = distance_between_2_points(drone.DroneLocation, parcelAtTransfer.LocationOfTarget)* Electricity_light;
                    break;
                case IDAL.DO.WeightCategories.medium:
                    min_battery = distance_between_2_points(drone.DroneLocation, parcelAtTransfer.LocationOfTarget) * Electricity_medium;
                    break;
                case IDAL.DO.WeightCategories.heavy:
                    min_battery = distance_between_2_points(drone.DroneLocation, parcelAtTransfer.LocationOfTarget) * Electricity_heavy;
                    break;
                default:
                    min_battery = 0;
                    break;
            }
            if (drone.Battery < min_battery)
                throw new ParcelException("Has not enaugh battery");
            drone.Battery -= min_battery;
            drone.DroneLocation = parcelAtTransfer.LocationOfTarget;
            drone.Status = DroneStatuses.vacant;
            parcel.Delivered = DateTime.Now;
            mydal.UpdateParcel(parcel);
        }
        public string print_parcel(int parcel_id)
        {
            return convertor(mydal.Find_parcel(parcel_id)).ToString();
        }
        public string print_all_parcels()
        {
            string result = "";
            List<ParcelToList> parcels = convertor1(mydal.Get_all_parcels().ToList());
            foreach (var item in parcels)
            {
                result += item.ToString();
                result += "\n";
            }
            return result;
        }
        public string print_all_parcels_without_drone()
        {
            string result = "";
            List<ParcelToList> parcels = convertor1(mydal.Get_all_parcels_that_have_not_yet_been_connect_to_drone().ToList());
            foreach (var item in parcels)
            {
                result += item.ToString();
                result += "\n";
            }
            return result;
        }
    }
}