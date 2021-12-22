using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneAtParcel
    {
        public int Id { get; set; }
       
        public double Battery { get; set; }
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
