using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// BaseStation struct to represent a drones Base station in the data layer
    /// </summary>
    public struct BaseStation
    {
        /// <summary>
        /// id number of the BaseStation
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// the name of the BaseStation
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Longitude of loction of the BaseStation
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Lattitude of loction of the BaseStation
        /// </summary>
        public double Lattitude { get; set; }
        /// <summary>
        ///number of  free charges slot sin the BaseStation
        /// </summary>
        public int ChargeSlots { get; set; }
        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id;
            result += " Name: " + Name;
            result += " Longitude: " + Longitude;
            result += " Lattitude: " + Lattitude;
            result += " ChargeSlots: " + ChargeSlots + '\n';
            return result;
        }
    }
}