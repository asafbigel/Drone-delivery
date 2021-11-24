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
            IDAL.DO.Parcel idalParcel = convertor(parcel);
            idalParcel.Id = mydal.GetAndUpdateRunNumber();
            idalParcel.SenderId = sender_id;
            idalParcel.TargetId = getter_id;
            mydal.Add_parcel(idalParcel);
        }
        

    }
}