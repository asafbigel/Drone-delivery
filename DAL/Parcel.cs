using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {

        struct Parcel
        {
            public int Id { get; set; }
            public int senderId { get; set; }
            public int targetId { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public int DroneId { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
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