using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Location
    {
        // KAV ORECH 
        public double longitude { get; set; }
        // KAV ROHAV
        public double latitude { get; set; }

        public override string ToString()
        {
            string result = "";
            result += " longitude:" + longitude + ",";
            result += " Lattitude: " + latitude;
            return result;
        }
    }

}

