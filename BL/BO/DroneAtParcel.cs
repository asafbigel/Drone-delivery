using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Drone Class to represent Drone in Parcel class  in the Business layer
    /// </summary>
    public class DroneAtParcel
    {
        /// <summary>
        /// id number of the Drone
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// level of  Battery that the Drone has
        /// </summary>
        public double Battery { get; set; }
        /// <summary>
        /// the loction of the Drone
        /// </summary>
        public Location DroneLocation { get; set; }
        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id + ",";
            result += " Battery: " + Battery + ",";
            result += " Location: " + DroneLocation;
            result += '\n';
            return result;
        }

    }
}
