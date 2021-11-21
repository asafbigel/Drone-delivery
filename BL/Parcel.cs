using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Parcel
    {
        public int id { get; set; }
        public CustomerAtParcel sender { get; set; }
        public CustomerAtParcel getter { get; set; }
        public WeightCategories weight { get; set; }
        public Priorities priority { get; set; }
        public DroneAtParcel droneAtParcel { get; set; }
        // created_parcel
        public DateTime Requested { get; set; }
        // Time of connected between the parcel and a drone
        public DateTime Scheduled { get; set; }
        // Time of taked the parcel from the sender
        public DateTime PickedUp { get; set; }
        // Time of getted the parcel to the sender
        public DateTime Delivered { get; set; }

    }
}
