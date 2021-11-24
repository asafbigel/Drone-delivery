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
            IDAL.DO.Parcel idalParcel = new IDAL.DO.Parcel
            {
                Delivered = parcel.Delivered,
                Requested = parcel.Requested,
                Scheduled = parcel.Scheduled,
                PickedUp = parcel.PickedUp,
                DroneId = 0,
                Id = mydal.GetAndUpdateRunNumber(),
                Priority = (IDAL.DO.Priorities)parcel.priority,
                Weight = (IDAL.DO.WeightCategories)parcel.weight,

            };
        }

    }
}