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
        // convertor is between IDAL.DO object and IBL.BO object
        // covnertor1 is between IBL.BO object and IBL.BO object
        private DroneInCharging convertor(IDAL.DO.DroneCharge droneCharge)
        {
            DroneInCharging new_drone = new DroneInCharging();
            DroneToList drone = my_drones.Find(x => x.Id == droneCharge.DroneId);
            new_drone.Battery = drone.Battery;
            new_drone.Id = droneCharge.DroneId;
            return new_drone;
        }
        private BaseStation convertor(IDAL.DO.BaseStation idalBaseStation)
        {
            BaseStation baseStation = new BaseStation
            {
                Id = idalBaseStation.Id,
                Name = idalBaseStation.Name,
                Num_Free_slots_charge = idalBaseStation.ChargeSlots,
                DroneInChargings = new List<DroneInCharging>() 
            };
            IEnumerable<IDAL.DO.DroneCharge> droneCharges = mydal.Get_all_DroneCharge();
            foreach (var item in droneCharges)
            {
                if (item.StationId == baseStation.Id)
                {
                    DroneInCharging drone = convertor(item);
                    baseStation.DroneInChargings.Add(drone);
                }
            }
            baseStation.Num_Free_slots_charge -= baseStation.DroneInChargings.Count();
            if (baseStation.Num_Free_slots_charge < 0)
                throw new DroneChargeException("There more drone from slots");
            Location location = new Location();
            location.latitude = idalBaseStation.Lattitude;
            location.longitude = idalBaseStation.Longitude;
            baseStation.BaseStationLocation = location;
            return baseStation;
        }
        private IDAL.DO.BaseStation convertor(BaseStation baseStation)
        {
            IDAL.DO.BaseStation idalBaseStation = new IDAL.DO.BaseStation()
            {
                ChargeSlots = baseStation.Num_Free_slots_charge,
                Id = baseStation.Id,
                Lattitude = baseStation.BaseStationLocation.latitude,
                Longitude = baseStation.BaseStationLocation.longitude,
                Name = baseStation.Name
            };
            return idalBaseStation;
        }
        private IDAL.DO.Parcel convertor(Parcel parcel)
        {
            int drone_id;
            if (parcel.TheDrone == null)
                drone_id = 0;
            else
                drone_id = parcel.TheDrone.Id;
            IDAL.DO.Parcel idalParcel = new IDAL.DO.Parcel
            {
                Delivered = parcel.Delivered,
                Requested = parcel.Requested,
                Scheduled = parcel.Scheduled,
                PickedUp = parcel.PickedUp,
                DroneId = drone_id,
                Priority = (IDAL.DO.Priorities)parcel.Priority,
                Weight = (IDAL.DO.WeightCategories)parcel.Weight,
                SenderId = parcel.Sender.Id,
                TargetId = parcel.Getter.Id
            };
            return idalParcel;
        }
        private Parcel convertor(IDAL.DO.Parcel idalParcel)
        {
            IDAL.DO.Customer idalGetter = mydal.Find_customer(idalParcel.TargetId);
            IDAL.DO.Customer idalSender = mydal.Find_customer(idalParcel.SenderId);
            CustomerAtParcel getter = new CustomerAtParcel()
            {
                CustomerName = idalGetter.Name,
                Id = idalGetter.Id
            };
            CustomerAtParcel sender = new CustomerAtParcel()
            {
                CustomerName = idalSender.Name,
                Id = idalSender.Id
            };
            DroneToList drone = my_drones.Find(item => item.Id == idalParcel.Id);
            return new Parcel()
            {
                Delivered = idalParcel.Delivered,
                Id = idalParcel.Id,
                PickedUp = idalParcel.PickedUp,
                Priority = (IBL.BO.Priorities)idalParcel.Priority,
                Weight = (IBL.BO.WeightCategories)idalParcel.Weight,
                Requested = idalParcel.Requested,
                Scheduled = idalParcel.Scheduled,
                Getter = getter,
                Sender = sender,
                TheDrone = convertor1(drone)
            };

        }
        private ParcelAtTransfer convertor1(Parcel parcel)
        {
            Location senderLlocation = new Location();
            Location getter_location = new Location();
            IDAL.DO.Customer sender = mydal.Find_customer(parcel.Sender.Id);
            IDAL.DO.Customer getter = mydal.Find_customer(parcel.Getter.Id);
            senderLlocation.longitude = sender.Longitude;
            senderLlocation.latitude = sender.Lattitude;
            getter_location.longitude = sender.Longitude;
            getter_location.latitude = sender.Lattitude;
            return new ParcelAtTransfer()
            {
                Id = parcel.Id,
                Priority = parcel.Priority,
                Weight = parcel.Weight,
                Sender = parcel.Sender,
                Getter = parcel.Getter,
                LocationOfPickUp = senderLlocation,
                SateOfParcel = (parcel.PickedUp != DateTime.MinValue),
                DistanceOfDelivery = distance_between_2_points(getter_location, senderLlocation),
                LocationOfTarget = getter_location
            };
        }
        private DroneAtParcel convertor1(DroneToList drone)
        {
            return new DroneAtParcel()
            {
                Battery = drone.Battery,
                Id = drone.Id
            };
        }
        private IDAL.DO.Customer convertor(Customer customer)
        {
            IDAL.DO.Customer idalCustomer = new IDAL.DO.Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Lattitude = customer.CustomerLocation.latitude,
                Longitude = customer.CustomerLocation.longitude,
                Phone = customer.Phone
            };
            return idalCustomer;
        }
        private IDAL.DO.Drone convertor(Drone drone)
        {
            IDAL.DO.Drone idalDrone = new IDAL.DO.Drone()
            {
                Id = drone.Id,
                MaxWeight = (IDAL.DO.WeightCategories)drone.MaxWeight,
                Model = drone.Model
            };
            return idalDrone;
        }
        private DroneToList convertor1(Drone drone)
        {
            DroneToList droneToList = new DroneToList()
            {
                Id = drone.Id,
                MaxWeight = drone.MaxWeight,
                Model = drone.Model,
                Battery = drone.Battery,
                DroneLocation = drone.DroneLocation,
                Status = drone.Status,
                NumOfParcel = 0
            };
            return droneToList;
        }
        private List<BaseStationToList> convertor1(IEnumerable<IDAL.DO.BaseStation> enumerable)
        {
            List<BaseStationToList> baseStations = new List<BaseStationToList>();
            foreach (var item in enumerable)
            {
                baseStations.Add(convertor1(item));
            }
            return baseStations;
        }
        private List<BaseStation> convertor(IEnumerable<IDAL.DO.BaseStation> enumerable)
        {
            List<BaseStation> baseStations = new List<BaseStation>();
            foreach (var item in enumerable)
            {
                baseStations.Add(convertor(item));
            }
            return baseStations;
        }
        private BaseStationToList convertor1(IDAL.DO.BaseStation item)
        {
            List<DroneInCharging> droneCharge = convertor(mydal.Get_all_DroneCharge().ToList().FindAll(charge => charge.StationId == item.Id));
            return new BaseStationToList()
            {
                Id = item.Id,
                Name = item.Name,
                NumOfBusySlots = droneCharge.Count(),
                NumOfFreeSlots = item.ChargeSlots
            };
        }
        private List<DroneInCharging> convertor(List<IDAL.DO.DroneCharge> idal_droneCharges)
        {
            List<DroneInCharging> droneInChargings = new List<DroneInCharging>();
            foreach (var item in idal_droneCharges)
            {
                droneInChargings.Add(convertor(item));
            }
            return droneInChargings;
        }
        private List<ParcelToList> convertor1(IEnumerable<IDAL.DO.Parcel> enumerable)
        {
            List<ParcelToList> parcels = new List<ParcelToList>();
            foreach (var item in enumerable)
            {
                parcels.Add(convertor1(item));
            }
            return parcels;
        }
        private List<ParcelToList> convertor(IEnumerable<IDAL.DO.Parcel> enumerable)
        {
            List<ParcelToList> parcels = new List<ParcelToList>();
            foreach (var item in enumerable)
            {
                parcels.Add(convertor1(item));
            }
            return parcels;
        }
        private ParcelToList convertor1(IDAL.DO.Parcel item)
        {
            ParcelStatuses parcelStatuses;
            if (item.Delivered != DateTime.MinValue)
                parcelStatuses = ParcelStatuses.delivered;
            else
            {
                if (item.PickedUp != DateTime.MinValue)
                    parcelStatuses = ParcelStatuses.picked_up;
                else
                {
                    if (item.Scheduled != DateTime.MinValue)
                        parcelStatuses = ParcelStatuses.Belongs;
                    else
                        parcelStatuses = ParcelStatuses.Defined;

                }
            }
            string target_name = mydal.Find_customer(item.TargetId).Name;
            string sender_name = mydal.Find_customer(item.SenderId).Name;
            return new ParcelToList()
            {
                GetterName = target_name,
                SenderName = sender_name,
                Id = item.Id,
                Priority = (IBL.BO.Priorities)item.Priority,
                Status = parcelStatuses,
                Weight = (IBL.BO.WeightCategories)item.Weight
            };
        }
        private List<CustomerToList> convertor(IEnumerable<IDAL.DO.Customer> enumerable)
        {
            List<CustomerToList> customers = new List<CustomerToList>();
            foreach (var item in enumerable)
            {
                customers.Add(convertor1(item));
            }
            return customers;
        }
        private List<Customer> convertor1(IEnumerable<IDAL.DO.Customer> enumerable)
        {
            List<Customer> customers = new List<Customer>();
            foreach (var item in enumerable)
            {
                customers.Add(convertor(item));
            }
            return customers;
        }
        private CustomerToList convertor1(IDAL.DO.Customer item)
        {
            List<IDAL.DO.Parcel> parcels_got = mydal.Get_all_parcels().ToList().FindAll                 (parcel => parcel.TargetId == item.Id && parcel.Delivered != DateTime.MinValue);
            List<IDAL.DO.Parcel> parcels_to_get = mydal.Get_all_parcels().ToList().FindAll              (parcel => parcel.TargetId == item.Id && parcel.Delivered == DateTime.MinValue);
            List<IDAL.DO.Parcel> parcels_sent_and_arrived = mydal.Get_all_parcels().ToList().FindAll    (parcel => parcel.SenderId == item.Id && parcel.Delivered != DateTime.MinValue);
            List<IDAL.DO.Parcel> parcels_sent_and_not_arrived = mydal.Get_all_parcels().ToList().FindAll(parcel => parcel.SenderId == item.Id && parcel.Delivered == DateTime.MinValue);
            return new CustomerToList()
            {
                Id = item.Id,
                Name = item.Name,
                Phone = item.Phone,
                NumOfParcelsGot = parcels_got.Count(),
                NumOfParcelsSentAndArrived = parcels_sent_and_arrived.Count(),
                NumOfParcelsSentAndNotArrived = parcels_sent_and_not_arrived.Count(),
                numOfParcelsToGet = parcels_to_get.Count()
            };
        }
        private List<DroneToList> convertor(IEnumerable<IDAL.DO.Drone> enumerable)
        {
            List<DroneToList> drones = new List<DroneToList>();
            foreach (var item in enumerable)
            {
                drones.Add(convertor1(item));
            }
            return drones;
        }
        private DroneToList convertor1(IDAL.DO.Drone item)
        {
            Drone drone = convertor(item);
            List<IDAL.DO.Parcel> parcels = mydal.Get_all_parcels().ToList().FindAll(parcel => parcel.DroneId == item.Id);
            return new DroneToList()
            {
                Battery = drone.Battery,
                Id = drone.Id,
                DroneLocation = drone.DroneLocation,
                MaxWeight = drone.MaxWeight,
                Model = drone.Model,
                Status = drone.Status,
                NumOfParcel = parcels.Count()
            };
        }
        private List<Parcel> convertor(List<IDAL.DO.Parcel> idalparcels)
        {
            List<Parcel> parcels = new List<Parcel>();
            foreach (var item in idalparcels)
            {
                parcels.Add(convertor(item));
            }
            return parcels;
        }
        private List<ParcelAtTransfer> convertor1(List<Parcel> parcelsHighPriority)
        {
            List<ParcelAtTransfer> new_list = new List<ParcelAtTransfer>();
            foreach (var item in parcelsHighPriority)
            {
                new_list.Add(convertor1(item));
            }
            return new_list;
        }
        private Drone convertor(IDAL.DO.Drone idalDrone)
        {
            DroneToList droneToList = my_drones.Find(item => item.Id == idalDrone.Id);
            return new Drone()
            {
                Battery = droneToList.Battery,
                Id = droneToList.Id,
                MaxWeight = droneToList.MaxWeight,
                Model = droneToList.Model,
                DroneLocation = droneToList.DroneLocation,
                Status = droneToList.Status
            };
        }
        private Customer convertor(IDAL.DO.Customer idal_customer)
        {
            Location location = new Location();
            location.latitude = idal_customer.Lattitude;
            location.longitude = idal_customer.Longitude;
            Customer customer = new Customer()
            {
                Id = idal_customer.Id,
                Name = idal_customer.Name,
                Phone = idal_customer.Phone,
                CustomerLocation = location
            };
            customer.parcelsAtCustomerFor = new List<Parcel>();
            customer.ParcelsAtCustomerFrom = new List<Parcel>();
            List<IDAL.DO.Parcel> parcels = mydal.Get_all_parcels().ToList();
            foreach (var parcel in parcels)
            {
                if (parcel.SenderId == idal_customer.Id)
                    customer.ParcelsAtCustomerFrom.Add(convertor(parcel));
                if (parcel.TargetId == idal_customer.Id)
                    customer.parcelsAtCustomerFor.Add(convertor(parcel));
            }
            return customer;
        }
    }
}