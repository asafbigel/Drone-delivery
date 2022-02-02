using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Customer struct to represent Customer of Parcels delivery service in the data layer
    /// </summary>
    public struct Customer 
    {
        /// <summary>
        /// id number of the Customer
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// the name of the Customer
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// the Phone of the Customer
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Lattitude of loction of the Customer
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Longitude of loction of the Customer
        /// </summary>
        public double Lattitude { get; set; }
        /// <summary>
        /// login Password of the Customer
        /// </summary>
        public string Password { get; set; }
        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id;
            result += " Name: " + Name;
            result += " Phone: " + Phone;
            result += " Longitude: " + Longitude;
            result += " Lattitude: " + Lattitude + '\n';
            return result;
        }
    }
}