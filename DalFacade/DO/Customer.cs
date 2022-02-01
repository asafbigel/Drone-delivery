using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Customer struct to represent Customer of Parcels delivery service
    /// </summary>
    public struct Customer 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public double Longitude { get; set; }
        public double Lattitude { get; set; }
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