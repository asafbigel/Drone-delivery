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
        /// <summary>
        /// fanction that update parcel
        /// </summary>
        /// <param name="parcel">new parcel to add</param>
        /// <param name="sender_id">  the id of the sender of the new parcel </param>
        /// <param name="getter_id"></param>
        public void Add_parcel(Parcel parcel, int sender_id, int getter_id)
        {
            parcel.Delivered = null;
            parcel.PickedUp = null;
            parcel.Scheduled = null;
            parcel.Requested = DateTime.Now;
            parcel.Id = mydal.GetAndUpdateRunNumber();
            parcel.TheDrone = null;
            DO.Parcel idalParcel = convertor(parcel);
            idalParcel.SenderId = sender_id;
            idalParcel.TargetId = getter_id;
            mydal.Add_parcel(idalParcel);
        }
        /// <summary>
        /// fanction that connect parcel to specific drone
        /// </summary>
        /// <param name="drone_id"> the id of the drone </param>
        public void connect_parcel_to_drone(int drone_id)
        {
            DO.Drone idalDrone = mydal.Find_drone(drone_id);
            DroneToList drone = my_drones.Find(item => item.Id == drone_id);
            if (drone.Status != DroneStatuses.vacant)
                throw new DroneException("The drone isn't vacant");
            List<DO.Parcel> idalparcels = mydal.Get_all_parcels(x => x.DroneId == 0).ToList().FindAll(item => (int)item.Weight <= (int)drone.MaxWeight);
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
            BaseStation baseStation = BaseStation_close_to_location(convertor(mydal.Get_all_base_stations(x => true)), parcelAtTransfer.LocationOfTarget);
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
            DO.Parcel parcel = mydal.Find_parcel(parcelAtTransfer.Id);
            parcel.DroneId = drone.Id;
            parcel.Scheduled = DateTime.Now;
            mydal.UpdateParcel(parcel);
        }
        /// <summary>
        /// pickedUp parcel by the drone
        /// </summary>
        /// <param name="drone_id">  the id of the drone </param>
        public void pickedUp_parcel_by_drone(int drone_id)
        {
            DroneToList drone = my_drones.Find(item => item.Id == drone_id);
            if (drone.Status != DroneStatuses.sending)
                throw new DroneException("The drone isn't at transfer");
            List<DO.Parcel> parcels = mydal.Get_all_parcels(x => true).ToList();
            List<DO.Parcel> parcelsOfThisDrone = parcels.FindAll(item => item.DroneId == drone.Id);
            //if (parcelsOfThisDrone.Count > 1)
            //    throw new DroneException("There are more than 1 parcels scheduled with this drone");
            if (parcelsOfThisDrone.Count == 0)
                throw new DroneException("There aren't parcel ");
            List<DO.Parcel> parcelsNotPickedUp = parcelsOfThisDrone.FindAll(item =>item.PickedUp == null);
            if (parcelsNotPickedUp.Count == 0)
                throw new DroneException("Don't has parcel that don't picked up");
            DO.Parcel parcel = parcelsNotPickedUp[0];
            ParcelAtTransfer parcelAtTransfer = convertor1(convertor(parcel));
            double min_battery = distance_between_2_points(drone.DroneLocation, parcelAtTransfer.LocationOfPickUp) * Electricity_free;
            if (drone.Battery < min_battery)
                throw new ParcelException("Has not enaugh battery");
            drone.Battery -= min_battery;
            drone.DroneLocation = parcelAtTransfer.LocationOfPickUp;
            parcel.PickedUp = DateTime.Now;
            mydal.UpdateParcel(parcel);
        }
        /// <summary>
        /// delivered parcel by the drone
        /// </summary>
        /// <param name="drone_id"> the id of the drone </param>
        public void delivered_parcel_by_drone(int drone_id)
        {
            DroneToList drone = my_drones.Find(item => item.Id == drone_id);
            List<DO.Parcel> parcels = mydal.Get_all_parcels(x => true).ToList();
            List<DO.Parcel> parcelsOfThisDrone = parcels.FindAll(item => item.DroneId == drone.Id);
            List<DO.Parcel> parcelsNotDelivered = parcelsOfThisDrone.FindAll(item => item.Delivered == null);
            if (parcelsNotDelivered.Count == 0)
                throw new DroneException("Don't has parcel that picked up and not delivered");
            DO.Parcel parcel = parcelsNotDelivered[0];
            ParcelAtTransfer parcelAtTransfer = convertor1(convertor(parcel));
            double min_battery;
            switch (parcel.Weight)
            {
                case DO.WeightCategories.light:
                    min_battery = distance_between_2_points(drone.DroneLocation, parcelAtTransfer.LocationOfTarget)* Electricity_light;
                    break;
                case DO.WeightCategories.medium:
                    min_battery = distance_between_2_points(drone.DroneLocation, parcelAtTransfer.LocationOfTarget) * Electricity_medium;
                    break;
                case DO.WeightCategories.heavy:
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
        /// <summary>
        /// A function that returns the ToString of parcel
        /// </summary>
        /// <param name="parcel_id"> the id of the parcel </param>
        /// <returns> ToString of the parcel</returns>
        public string StringParcel(int parcel_id)
        {
            return convertor(mydal.Find_parcel(parcel_id)).ToString();
        }
        /// <summary>
        /// A function that returns the ToString of the list of all the Parcels
        /// </summary>
        /// <returns> ToString of the list of all the Parcels </returns>
        public string StringAllParcels()
        {
            string result = "";
            List<ParcelToList> parcels = convertor4(mydal.Get_all_parcels(x => true).ToList());
            foreach (var item in parcels)
            {
                result += item.ToString();
                result += "\n";
            }
            return result;
        }
        /// <summary>
        /// A function that returns the ToString of the list of all the Parcels that dons't connected to drone
        /// </summary>
        /// <returns> ToString of the list of all the Parcels that dons't connected to drone </returns>
        public string StringAllParcelsWithout_drone()
        {
            string result = "";
            List<ParcelToList> parcels = convertor4(mydal.Get_all_parcels(x => x.DroneId == 0).ToList());
            foreach (var item in parcels)
            {
                result += item.ToString();
                result += "\n";
            }
            return result;
        }
        /// <summary>
        /// Get a parcel which this drone is sending it now
        /// </summary>
        /// <param name="id">Drone's id</param>
        /// <returns>The parcel</returns>
        public Parcel GetCurrectParcelOfDrone(int id)
        {
            return convertor(mydal.Find_parcel(item => item.DroneId == id && item.Delivered == null));            
        }
    }
}