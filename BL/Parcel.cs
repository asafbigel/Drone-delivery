using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Parcel
    {
        public int Id { get; set; }
        public CustomerAtParcel Sender { get; set; }
        public CustomerAtParcel Getter { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public DroneAtParcel TheDrone { get; set; }
        // created_parcel
        public DateTime Requested { get; set; }
        // Time of connected between the parcel and a drone
        public DateTime Scheduled { get; set; }
        // Time of taked the parcel from the sender
        public DateTime PickedUp { get; set; }
        // Time of getted the parcel to the getter
        public DateTime Delivered { get; set; }

        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id + ",";
            result += " Sender: " + Sender + ",";
            result += " Target: " + Getter + ",";
            result += " Weight: " + Weight + ",";
            result += " Priority: " + Priority + ",";
            result += " The Drone: " + TheDrone + ",";
            result += " Requested: " + Requested + ",";
            result += " Scheduled: " + Scheduled + ",";
            result += " PickedUp: " + PickedUp + ",";
            result += " Delivered: " + Delivered;
            //+ "\n";
            return result;
        }

    }
}
