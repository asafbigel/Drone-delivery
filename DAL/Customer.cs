﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        struct Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public override string ToString()
            {
                string result = "";
                result += "ID: " + Id;
                result += "\nName: " + Name;
                result += "\nPhone: " + Phone;
                result += "\nLongitude: " + Longitude;
                result += "\nLatitude: " + Latitude;
                return result;
            }
        }

    }
}