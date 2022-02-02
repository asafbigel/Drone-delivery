using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Parcel struct to represent Parcel to sending  in the data layer
    /// </summary>
    public struct Parcel
    {
        /// <summary>
        /// id number of the Parcel
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// id number of the Customer that sent the Parcel
        /// </summary>
        public int SenderId { get; set; }
        /// <summary>
        /// id number of the Customer that is the target of the sending;
        /// </summary>
        public int TargetId { get; set; }
        /// <summary>
        /// Confirmation of Parcel collection was received
        /// </summary>
        public bool CollectionConfirmation  { get; set; }
        /// <summary>
        ///  Confirmation of Parcel Recive was received
        /// </summary>
        public bool ReciveConfirmation { get; set; }
        /// <summary>
        /// the Weight of theParcel
        /// </summary>
        public WeightCategories Weight { get; set; }
        /// <summary>
        /// Priority level of the Parcel
        /// </summary>
        public Priorities Priority { get; set; }
        /// <summary>
        /// the id number of the drone that take this Parcel
        /// </summary>
        public int DroneId { get; set; }

        /// <summary>
        /// Time of created the Parcel
        /// </summary>
        public DateTime? Requested { get; set; }
        /// <summary>
        /// Time of connected between the parcel and a drone
        /// </summary>
        public DateTime? Scheduled { get; set; }
        /// <summary>
        ///  Time of taked the parcel from the sender
        /// </summary>
        public DateTime? PickedUp { get; set; }
        /// <summary>
        ///  Time of getted the parcel to the getter
        /// </summary>
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
