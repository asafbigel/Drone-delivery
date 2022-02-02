using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Drone Class to represent Drone of delivery service  in the Business layer
    /// </summary>
    public class Drone
    {
        /// <summary>
        /// id number of the Drone
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Model of the Drone
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// the Max Weight thhat the Drone can take
        /// </summary>
        public WeightCategories MaxWeight { get; set; }
        /// <summary>
        /// level of  Battery that the Drone has
        /// </summary>
        public double Battery { get; set; }
        /// <summary>
        /// Is the Drone in maintenance in charging, in sending or vacant
        /// </summary>
        public DroneStatuses Status { get; set; }
        /// <summary>
        ///the loction of the Drone
        /// </summary>
        public Location DroneLocation { get; set; }
        /// <summary>
        /// the  Parcel that the drone sending now
        /// </summary>
        public ParcelAtTransfer Parcel { get; set; }
        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id + ",";
            result += " Model: " + Model + ",";
            result += " MaxWeight: " + MaxWeight + ",";
            result += " Battery: " + Battery + ",";
            result += " Status: " + Status + ",";
            result += " Location: " + DroneLocation;
            if (Parcel !=null)
                result += "\n\tParcel:\n\t" +Parcel;
            //result += '\n';
            return result;
        }
    }
}
