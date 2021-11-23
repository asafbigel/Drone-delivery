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
        static IDAL.IDal mydal;
        List<DroneToList> my_drones = new List<DroneToList>();
        public BL()
        {
            mydal = new DalObject.DalObject();
            double[] Electricity = mydal.ElectricityUse();
            double Electricity_free = Electricity[0];
            double Electricity_light = Electricity[1];
            double Electricity_medium = Electricity[2];
            double Electricity_heavy = Electricity[3];
            double Charge_at_hour = Electricity[4];
            Random random = new Random();


            #region List of drone from the data layer
            List<IDAL.DO.Drone> idalDrones = mydal.Get_all_drones().ToList();
            foreach (var item in idalDrones)
            {
                my_drones.Add(new DroneToList
                {
                    Id = item.Id,
                    Model = item.Model,
                    MaxWeight = (WeightCategories)item.MaxWeight,
                    numOfParcel = 0,
                    Status = (DroneStatuses)random.Next(0, 1)
                });
            }
            #endregion

            #region List of the parcels
            List<IDAL.DO.Parcel> idalParcel = mydal.Get_all_parcels().ToList();
            #endregion

            #region List of customer from the data layer
            List<IDAL.DO.Customer> idalCustomer = mydal.Get_all_customers().ToList();
            List<Customer> customers = new List<Customer>();
            foreach (var item in idalCustomer)
            {
                Location location = new Location();
                location.latitude = item.Lattitude;
                location.longitude = item.Longitude;
                customers.Add(new Customer
                {
                    id = item.Id,
                    name = item.Name,
                    space = location,
                    phone = item.Phone,
                    parcels_at_customer_for = new List<BO.Parcel>(),
                    parcels_at_customer_from = new List<BO.Parcel>()
                });
            }
            #endregion

            #region List of base station from the data layer
            List<IDAL.DO.BaseStation> idalBaseStation = mydal.Get_all_base_stations().ToList();
            List<BaseStation> baseStations = new List<BaseStation>();
            foreach (var item in idalBaseStation)
            {
                Location location = new Location();
                location.latitude = item.Lattitude;
                location.longitude = item.Longitude;
                baseStations.Add(new BaseStation
                {
                    id = item.Id,
                    name = item.Name,
                    Num_Free_slots_charge = item.ChargeSlots,
                    space = location,
                    DroneInChargings = new List<DroneInCharging>()
                });
            }
            #endregion


            foreach (var drone in my_drones)
            {
                List<IDAL.DO.Parcel> parcel_of_this_drone = idalParcel.FindAll(x => x.DroneId == drone.Id);
                List<IDAL.DO.Parcel> parcel_of_this_drone_Delivered = parcel_of_this_drone.FindAll(x => x.Delivered != DateTime.MinValue);
                drone.numOfParcel = parcel_of_this_drone.Count();
                if (parcel_of_this_drone.Count() - parcel_of_this_drone_Delivered.Count() != 0)
                    drone.Status = DroneStatuses.sending;

                // choose location and random the battery ofeach drone
                foreach (var parcel in parcel_of_this_drone)
                {
                    // case the parcel has not delivere
                    if (parcel.Delivered == DateTime.MinValue)
                    {
                        Customer sender = find_customer(customers, parcel.SenderId);
                        Customer getter = find_customer(customers, parcel.TargetId);
                        BaseStation baseStation_neer_geeter = BaseStation_close_to_location(baseStations, getter.space);
                        if (parcel.Scheduled != DateTime.MinValue && parcel.PickedUp == DateTime.MinValue)
                        {
                            BaseStation baseStation_neer_sender = BaseStation_close_to_location(baseStations, sender.space);
                            drone.location = baseStation_neer_sender.space;

                            double distance1 = pow_of_distance_between_2_points(baseStation_neer_sender.space, sender.space);
                            double distance2 = pow_of_distance_between_2_points(sender.space, getter.space);
                            double distance3 = pow_of_distance_between_2_points(baseStation_neer_geeter.space, getter.space);
                            double min_battery = (distance1 + distance3) * Electricity_free;
                            switch (parcel.Weight)
                            {
                                case IDAL.DO.WeightCategories.light:
                                    min_battery += distance2 * Electricity_light;
                                    break;
                                case IDAL.DO.WeightCategories.medium:
                                    min_battery += distance2 * Electricity_medium;
                                    break;
                                case IDAL.DO.WeightCategories.heavy:
                                    min_battery += distance2 * Electricity_heavy;
                                    break;
                                default:
                                    break;
                            }
                            drone.Battery = random.Next((int)min_battery + 1, 100);
                        }
                        if (parcel.PickedUp != DateTime.MinValue)
                        {
                            drone.location = sender.space;
                            double distance1 = pow_of_distance_between_2_points(sender.space, getter.space);
                            double distance2 = pow_of_distance_between_2_points(getter.space, baseStation_neer_geeter.space);
                            double min_battery = distance2 * Electricity_free;
                            switch (parcel.Weight)
                            {
                                case IDAL.DO.WeightCategories.light:
                                    min_battery += distance1 * Electricity_light;
                                    break;
                                case IDAL.DO.WeightCategories.medium:
                                    min_battery += distance1 * Electricity_medium;
                                    break;
                                case IDAL.DO.WeightCategories.heavy:
                                    min_battery += distance1 * Electricity_heavy;
                                    break;
                                default:
                                    break;
                            }
                            drone.Battery = random.Next((int)min_battery + 1, 100);
                        }
                    }

                }
                // cases all of this drine have dlivere
                // case status is maintenance
                if (drone.Status == DroneStatuses.maintenance)
                {
                    int i = random.Next(0, baseStations.Count);
                    drone.location = baseStations[i].space;
                    drone.Battery = random.Next(0, 21);
                }
                // case status is vacant
                if (drone.Status == DroneStatuses.vacant)
                {
                    int i = random.Next(0, parcel_of_this_drone_Delivered.Count);
                    Customer getter = find_customer(customers, parcel_of_this_drone_Delivered[i].TargetId);
                    drone.location = getter.space;
                    BaseStation baseStation_neer_geeter = BaseStation_close_to_location(baseStations, getter.space);
                    double distance = pow_of_distance_between_2_points(getter.space, baseStation_neer_geeter.space);
                    double min_battery = distance * Electricity_free;
                    drone.Battery = random.Next((int)distance + 1, 100);
                }


            }
        }

        private BaseStation BaseStation_close_to_location(List<BaseStation> baseStations, Location space)
        {
            if (baseStations.Count == 0)
                throw new BaseStationExeption("The list is empty");
            BaseStation baseStation = baseStations[0];
            double min_distance = pow_of_distance_between_2_points(baseStation.space, space);
            foreach (var item in baseStations)
            {
                double distance = pow_of_distance_between_2_points(item.space, space);
                if (distance < min_distance)
                {
                    min_distance = distance;
                    baseStation = item;
                }
            }
            return baseStation;


        }

        private double pow_of_distance_between_2_points(Location space1, Location space2)
        {
            double latitude = (space1.latitude - space2.latitude) * (space1.latitude - space2.latitude);
            double longitude = (space1.longitude - space2.longitude) * (space1.longitude - space2.longitude);
            return latitude + longitude;
        }

        private Customer find_customer(List<Customer> customers, int senderId)
        {
            foreach (var item in customers)
            {
                if (item.id == senderId)
                    return item;
            }
            throw new CustomerExeption("id not found");
        }
    }
}