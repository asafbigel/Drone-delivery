using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BO
{
    /// <summary>
    /// BaseStation Class to represent a drones Base station in the Business layer
    /// </summary>
    public class BaseStation
    {
        /// <summary>
        ///  id number of the BaseStation
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///  the name of the BaseStation
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// loction of the BaseStation
        /// </summary>
        public Location BaseStationLocation { get; set; }
        /// <summary>
        /// the number of Frees Slots Charge in the BaseStation
        /// </summary>
        public int NumFreeSlotsCharge { get; set; }
        /// <summary>
        /// list of the Drones in Charg naw in this BaseStation
        /// </summary>
        public List<DroneInCharging> DroneInChargings { get; set; }        

        public override string ToString()

        {
           
            string result = "";
            result += "ID: " + Id +",";
            result += " Name: " + Name + ",";
            result += " Location: " + BaseStationLocation + ",";
            result += " Num Free slots charge: " + NumFreeSlotsCharge + "\n";
            result += "Drones in charge:\n";
            foreach (var item in DroneInChargings)
            {
                result += "\t";
                result += item.ToString();
            }
            //result += "\n";
            return result;
        }
    }
}
