using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Drone struct to represent Drone of delivery service in the data layer
    /// </summary>
    public struct Drone
    {
        /// <summary>
        /// id number of the Drone
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///  Model of the Drone
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        ///the Max Weight thhat the Drone can take
        /// </summary>
        public WeightCategories MaxWeight { get; set; }
        
        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id;
            result += " Model: " + Model;
            result += " MaxWeight: " + MaxWeight;
            //result += " Status: " + Status;
            //result += " Battery: " + Battery;
            result += '\n';
            return result;
        }
    }
}