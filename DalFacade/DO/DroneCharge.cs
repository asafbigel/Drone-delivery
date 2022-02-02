using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Drone Charge struct to represent Charge of Drone in the data layer
    /// </summary>
    public struct DroneCharge
    {
        /// <summary>
        /// id number of the Drone that in Chargeing 
        /// </summary>
        public int DroneId { get; set; }
        /// <summary>
        /// the number of the Station that the Charge slot in it
        /// </summary>
        public int StationId { get; set; }
        /// <summary>
        /// the time that the Drone Enter to Charge
        /// </summary>
        public DateTime EnterToCharge { get; set; }
        public override string ToString()
        {
            string result = "";
            result += "DroneId: " + DroneId;
            result += " StationId: " + StationId;
            result += " Enter to charge: " + EnterToCharge + '\n';
            return result;
        }
    }
}