using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{/// <summary>
/// Parcel Class to represent Parcel to sending in the Business layer
/// </summary>
    public class Parcel
    {
        /// <summary>
        /// id number of the Parcel
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///  the Customer that sent the Parcel
        /// </summary>
        public CustomerAtParcel Sender { get; set; }
        /// <summary>
        /// the Customer that is the target of the sending
        /// </summary>
        public CustomerAtParcel Getter { get; set; }
        /// <summary>
        /// Confirmation of Parcel collection was received
        /// </summary>
        public bool CollectionConfirmation { get; set; }
        /// <summary>
        /// Confirmation of Parcel Recive was received
        /// </summary>
        public bool ReciveConfirmation { get; set; }
        /// <summary>
        /// the Weight of theParcel
        /// </summary>
        public WeightCategories Weight { get; set; }
        /// <summary>
        ///  Priority level of the Parcel
        /// </summary>
        public Priorities Priority { get; set; }
        /// <summary>
        /// the drone that take this Parcel
        /// </summary>
        public DroneAtParcel TheDrone { get; set; }

        /// <summary>
        /// Time of created the Parcel
        /// </summary>
        public DateTime? Requested { get; set; }
        /// <summary>
        /// Time of connected between the parcel and a drone
        /// </summary>
        public DateTime? Scheduled { get; set; }

        /// <summary>
        /// Time of taked the parcel from the sender
        /// </summary>
        public DateTime? PickedUp { get; set; }
        /// <summary>
        /// Time of getted the parcel to the getter
        /// </summary>
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
