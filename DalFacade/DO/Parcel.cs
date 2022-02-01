using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{

    public struct Parcel
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int TargetId { get; set; }
        public bool CollectionConfirmation  { get; set; }        
        public bool ReciveConfirmation { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public int DroneId { get; set; }

        // Time of created the request
        public DateTime? Requested { get; set; }
        // Time of connected between the parcel and a drone
        public DateTime? Scheduled { get; set; }
        // Time of taked the parcel from the sender
        public DateTime? PickedUp { get; set; }
        // Time of getted the parcel to the sender
        public DateTime? Delivered { get; set; }
        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id;
            result += " SenderId: " + SenderId;
            result += " TargetId: " + TargetId;
            result += " Weight: " + Weight;
            result += " Priority: " + Priority;
            result += " DroneId: " + DroneId;
            result += " Requested: " + Requested;
            result += " Scheduled: " + Scheduled;
            result += " PickedUp: " + PickedUp;
            result += " Delivered: " + Delivered + '\n';
            return result;
        }
    }
}
