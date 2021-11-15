using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class Parcel
    {
        int id;
        CustomerAtParcel sender;
        CustomerAtParcel getter;
        WeightCategories weight;
        Priorities priority;
        DroneAtParcel droneAtParcel;
        // created_parcel
        DateTime Requested;
        // Time of connected between the parcel and a drone
        DateTime Scheduled;
        // Time of taked the parcel from the sender
        DateTime PickedUp;
        // Time of getted the parcel to the sender
        DateTime Delivered;

    }
}
