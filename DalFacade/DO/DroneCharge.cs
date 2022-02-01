using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Drone Charge struct to represent Drone of delivery service
    /// </summary>
    public struct DroneCharge
    {
        public int DroneId { get; set; }
        public int StationId { get; set; }
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