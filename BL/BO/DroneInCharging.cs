using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Drone In Charging Class to represent Drone that in Charging in the Business layer
    /// </summary>
    public class DroneInCharging
    {
        /// <summary>
        ///  id number of the Drone
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// level of  Battery that the Drone has
        /// </summary>
        public double Battery { get; set; }
        /// <summary>
        /// the time that the Drone Enter to Charge
        /// </summary>
        public DateTime EnterToCharge{ get; set; }

        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id +",";
            result += " Battery: " + Battery + ",";
            result += " Enter to charging: " + EnterToCharge + ",";
            result += " Time in charging: " + (DateTime.Now-EnterToCharge);
            result += '\n';
            return result;
        }
    }
}
