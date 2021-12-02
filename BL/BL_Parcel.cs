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
            parcel.id = mydal.GetAndUpdateRunNumber();
            parcel.droneAtParcel = null;
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
            BaseStation baseStation = BaseStation_close_to_location(convertor(mydal.Get_all_base_stations()), parcelAtTransfer.spaceOfTarget);
            double battery_needed = 0;
            battery_needed += distance_between_2_points(drone.location, parcelAtTransfer.spaceOfPickUp) * Electricity_free;
            if (parcelAtTransfer.weight == WeightCategories.heavy)
                battery_needed += distance_between_2_points(parcelAtTransfer.spaceOfPickUp, parcelAtTransfer.spaceOfTarget) * Electricity_heavy;
            if (parcelAtTransfer.weight == WeightCategories.medium)
                battery_needed += distance_between_2_points(parcelAtTransfer.spaceOfPickUp, parcelAtTransfer.spaceOfTarget) * Electricity_medium;
            if (parcelAtTransfer.weight == WeightCategories.light)
                battery_needed += distance_between_2_points(parcelAtTransfer.spaceOfPickUp, parcelAtTransfer.spaceOfTarget) * Electricity_light;
            battery_needed += distance_between_2_points(parcelAtTransfer.spaceOfTarget, baseStation.space) * Electricity_free;

            if (battery_needed > drone.Battery)
                throw new DroneException("No enaugh battery");
            drone.Status = DroneStatuses.sending;
            IDAL.DO.Parcel parcel = mydal.Find_parcel(parcelAtTransfer.id);
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
            IDAL.DO.Parcel parcel = parcelsOfThisDrone.Find(item =>item.PickedUp == DateTime.MinValue);
            //if (parcel.PickedUp != DateTime.MinValue)
            //    throw new DroneException("The parcel have picken up");
            if (parcel.DroneId != drone_id)
                throw new DroneException("Don't has parcel that don't picked up");
            ParcelAtTransfer parcelAtTransfer = convertor1(convertor(parcel));
            double min_battery = distance_between_2_points(drone.location, parcelAtTransfer.spaceOfPickUp) * Electricity_free;
            if (drone.Battery < min_battery)
                throw new ParcelException("Has not enaugh battery");
            drone.Battery -= min_battery;
            drone.location = parcelAtTransfer.spaceOfPickUp;
            parcel.PickedUp = DateTime.Now;
            mydal.UpdateParcel(parcel);
        }
        public void delivered_parcel_by_drone(int drone_id)
        {
            DroneToList drone = my_drones.Find(item => item.Id == drone_id);
            List<IDAL.DO.Parcel> parcels = mydal.Get_all_parcels().ToList();
            List<IDAL.DO.Parcel> parcelsOfThisDrone = parcels.FindAll(item => item.DroneId == drone.Id);
            IDAL.DO.Parcel parcel = parcelsOfThisDrone.Find(item => item.PickedUp != DateTime.MinValue && item.Delivered == DateTime.MinValue);
            if (parcel.DroneId != drone_id)
                throw new DroneException("Don't has parcel that picked up and not delivered");
            ParcelAtTransfer parcelAtTransfer = convertor1(convertor(parcel));
            double min_battery;
            switch (parcel.Weight)
            {
                case IDAL.DO.WeightCategories.light:
                    min_battery = distance_between_2_points(drone.location, parcelAtTransfer.spaceOfTarget)* Electricity_light;
                    break;
                case IDAL.DO.WeightCategories.medium:
                    min_battery = distance_between_2_points(drone.location, parcelAtTransfer.spaceOfTarget) * Electricity_medium;
                    break;
                case IDAL.DO.WeightCategories.heavy:
                    min_battery = distance_between_2_points(drone.location, parcelAtTransfer.spaceOfTarget) * Electricity_heavy;
                    break;
                default:
                    min_battery = 0;
                    break;
            }
            if (drone.Battery < min_battery)
                throw new ParcelException("Has not enaugh battery");
            drone.Battery -= min_battery;
            drone.location = parcelAtTransfer.spaceOfTarget;
            drone.Status = DroneStatuses.vacant;
            parcel.Delivered = DateTime.Now;
            mydal.UpdateParcel(parcel);
        }
        public void print_parcel(int parcel_id)
        {
            Console.WriteLine(convertor(mydal.Find_parcel(parcel_id)));
        }
        public void print_all_parcels()
        {
            List<ParcelToList> parcels = convertor1(mydal.Get_all_parcels().ToList());
            foreach (var item in parcels)
            {
                Console.WriteLine(item);
            }
        }
        public void print_all_parcels_without_drone()
        {
            List<ParcelToList> parcels = convertor1(mydal.Get_all_parcels_that_have_not_yet_been_connect_to_drone().ToList());
            foreach (var item in parcels)
            {
                Console.WriteLine(item);
            }
        }
    }
}