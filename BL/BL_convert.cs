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
            IDAL.DO.Parcel idalParcel = new IDAL.DO.Parcel
            {
                Delivered = parcel.Delivered,
                Requested = parcel.Requested,
                Scheduled = parcel.Scheduled,
                PickedUp = parcel.PickedUp,
                DroneId = 0,
                Priority = (IDAL.DO.Priorities)parcel.priority,
                Weight = (IDAL.DO.WeightCategories)parcel.weight
            };
            return idalParcel;
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

    }
}