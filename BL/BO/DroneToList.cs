using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Drone Class to represent Drone of delivery service in list of drones  in the Business layer
    /// </summary>
    public class DroneToList
    {
        /// <summary>
        ///  id number of the Drone
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///  Model of the Drone
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
        /// the loction of the Drone
        /// </summary>
        public Location DroneLocation { get; set; }
        /// <summary>
        /// the number of Parcels that taken by this drone
        /// </summary>
        public int NumOfParcel { get; set; }
        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id + ",";
            result += " Model: " + Model + ",";
            result += " MaxWeight: " + MaxWeight + ",";
            result += " Battery: " + Battery + ",";
            result += " Status: " + Status + ",";
            result += " Location: " + DroneLocation + ",";
            result += " Number Of the Parcel: " + NumOfParcel;
            //result += '\n';
            return result;
        }
    }


}

