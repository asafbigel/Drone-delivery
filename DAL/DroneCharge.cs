﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        struct DroneCharge
        {
            public int DroneId { get; set; }
            public int StationId { get; set; }
            public override string ToString()
            {
                string result = "";
                result += "DroneId: " + DroneId;
                result += "\nStationId: " + StationId;
                return result;
            }
        }
    }
}