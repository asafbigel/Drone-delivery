using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {

        public struct Parcel
        {
            public int Id { get; set; }
            public int senderId { get; set; }
            public int targetId { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public int DroneId { get; set; }
            
            // Time of created the request
            public DateTime Requested { get; set; }
            // Time of connected between the parcel and a drone
            public DateTime Scheduled { get; set; }
            // Time of taked the parcel from the sender
            public DateTime PickedUp { get; set; }
            // Time of getted the parcel to the sender
            public DateTime Delivered { get; set; }
            public override string ToString()
            {
                string result = "";
                result += "ID: " + Id;
                result += "\nsenderId: " + senderId;
                result += "\ntargetId: " + targetId;
                result += "\nWeight: " + Weight;
                result += "\nPriority: " + Priority;
                result += "\nDroneId: " + DroneId;
                result += "\nScheduled: " + Scheduled;
                result += "\nPickedUp: " + PickedUp;
                result += "\nDelivered: " + Priority;
                return result;
            }
        }
    }
}