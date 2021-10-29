using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        struct BaseStation
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }
            // free charges slots
            public int ChargeSlots { get; set; }
            public override string ToString()
            {
                string result = "";
                result += "ID: "+Id;
                result += "\nName: "+Name;
                result += "\nLongitude: " + Longitude;
                result += "\nLattitude: " + Lattitude;
                result += "\nChargeSlots: " + ChargeSlots;
                return result;
            }
        }
    }
}