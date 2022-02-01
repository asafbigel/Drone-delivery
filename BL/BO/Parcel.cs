using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Parcel
    {
        public int Id { get; set; }
        public CustomerAtParcel Sender { get; set; }
        public CustomerAtParcel Getter { get; set; }
        public bool CollectionConfirmation { get; set; }
        public bool ReciveConfirmation { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public DroneAtParcel TheDrone { get; set; }
        // created_parcel
        public DateTime? Requested { get; set; }
        // Time of connected between the parcel and a drone
        public DateTime? Scheduled { get; set; }
        // Time of taked the parcel from the sender
        public DateTime? PickedUp { get; set; }
        // Time of getted the parcel to the getter
        public DateTime? Delivered { get; set; }

        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id + ",";
            result += " Weight: " + Weight + ",";
            result += " Priority: " + Priority + ",";
            result += " Requested: " + Requested + ",";
            result += " Scheduled: " + Scheduled + ",";
            result += " PickedUp: " + PickedUp + ",";
            result += " Delivered: " + Delivered + "\n";
            result += "Sender:\t" + Sender + "\n";
            result += "Target:\t" + Getter + "\n";
            result += "The Drone: " + TheDrone;
            //+ "\n";
            return result;
        }

    }
}
