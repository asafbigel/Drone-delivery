﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct BaseStation
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
                result += "\nID: "+Id;
                result += " Name: "+Name;
                result += " Longitude: " + Longitude;
                result += " Lattitude: " + Lattitude;
                result += " ChargeSlots: " + ChargeSlots;
                return result;
            }
        }
    }
}