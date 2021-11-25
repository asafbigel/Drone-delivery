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
                id = idalBaseStation.Id,
                name = idalBaseStation.Name,
                Num_Free_slots_charge = idalBaseStation.ChargeSlots,
                DroneInChargings = new List<DroneInCharging>()
            };
            IEnumerable<IDAL.DO.DroneCharge> droneCharges = mydal.Get_all_DroneCharge();
            foreach (var item in droneCharges)
            {
                if (item.StationId == baseStation.id)
                {
                    DroneInCharging drone = convertor(item);
                    baseStation.DroneInChargings.Add(drone);
                }
            }
            baseStation.Num_Free_slots_charge -= baseStation.DroneInChargings.Count();
            if (baseStation.Num_Free_slots_charge < 0)
                throw new DroneChargeException("There more drone from slots");
            return baseStation;
        }
        private IDAL.DO.BaseStation convertor(BaseStation baseStation)
        {
            IDAL.DO.BaseStation idalBaseStation = new IDAL.DO.BaseStation()
            {
                ChargeSlots = baseStation.Num_Free_slots_charge,
                Id = baseStation.id,
                Lattitude = baseStation.space.latitude,
                Longitude = baseStation.space.longitude,
                Name = baseStation.name
            };
            return idalBaseStation;
        }
        private IDAL.DO.Parcel convertor(Parcel parcel)
        {
            int drone_id;
            if (parcel.droneAtParcel == null)
                drone_id = 0;
            else
                drone_id = parcel.droneAtParcel.Id;
            IDAL.DO.Parcel idalParcel = new IDAL.DO.Parcel
            {
                Delivered = parcel.Delivered,
                Requested = parcel.Requested,
                Scheduled = parcel.Scheduled,
                PickedUp = parcel.PickedUp,
                DroneId = drone_id,
                Priority = (IDAL.DO.Priorities)parcel.priority,
                Weight = (IDAL.DO.WeightCategories)parcel.weight,
                SenderId = parcel.sender.id,
                TargetId = parcel.getter.id
            };
            return idalParcel;
        }
        private Parcel convertor(IDAL.DO.Parcel idalParcel)
        {
            IDAL.DO.Customer idalGetter = mydal.Find_customer(idalParcel.TargetId);
            IDAL.DO.Customer idalSender = mydal.Find_customer(idalParcel.SenderId);
            CustomerAtParcel getter = new CustomerAtParcel()
            {
                customer_name = idalGetter.Name,
                id = idalGetter.Id
            };
            CustomerAtParcel sender = new CustomerAtParcel()
            {
                customer_name = idalSender.Name,
                id = idalSender.Id
            };
            DroneToList drone = my_drones.Find(item => item.Id == idalParcel.Id);
            return new Parcel()
            {
                Delivered = idalParcel.Delivered,
                id = idalParcel.Id,
                PickedUp = idalParcel.PickedUp,
                priority = (IBL.BO.Priorities)idalParcel.Priority,
                weight = (IBL.BO.WeightCategories)idalParcel.Weight,
                Requested = idalParcel.Requested,
                Scheduled = idalParcel.Scheduled,
                getter = getter,
                sender = sender,
                droneAtParcel = convertor1(drone)
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
                Id = customer.id,
                Name = customer.name,
                Lattitude = customer.space.latitude,
                Longitude = customer.space.longitude,
                Phone = customer.phone
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
                location = drone.Space,
                Status = drone.Status,
                numOfParcel = 0
            };
            return droneToList;
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
        private List<Parcel> convertor(List<IDAL.DO.Parcel> idalparcels)
        {
            List<Parcel> parcels = new List<Parcel>();
            foreach (var item in idalparcels)
            {
                parcels.Add(convertor(item));
            }
            return parcels;
        }

        private ParcelAtTransfer covnertor1(Parcel parcel)
        {
            Location getterLlocation = new Location();
            Location senderLlocation = new Location();
            IDAL.DO.Customer iadlGetter = mydal.Find_customer(parcel.getter.id);
            IDAL.DO.Customer iadlSender = mydal.Find_customer(parcel.sender.id);
            getterLlocation.latitude = iadlGetter.Lattitude;
            getterLlocation.longitude = iadlGetter.Longitude;
            senderLlocation.latitude = iadlSender.Lattitude;
            senderLlocation.longitude = iadlSender.Longitude;
            return new ParcelAtTransfer()
            {
                id = parcel.id,
                weight = parcel.weight,
                getter = parcel.getter,
                sender = parcel.sender,
                priority = parcel.priority,
                sateOfParcel = (parcel.PickedUp != DateTime.MinValue),
                spaceOfPickUp = senderLlocation,
                spaceOfTarget = getterLlocation,
                distanceOfDelivery = distance_between_2_points(getterLlocation, senderLlocation)
            };
        }

        private List<ParcelAtTransfer> convertor1(List<Parcel> parcelsHighPriority)
        {
            List<ParcelAtTransfer> new_list = new List<ParcelAtTransfer>();
            foreach (var item in parcelsHighPriority)
            {
                new_list.Add(covnertor1(item));
            }
            return new_list;
        }

    }
}