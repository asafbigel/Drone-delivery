﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneInCharging
    {
        public int Id { get; set; }
     
        public double Battery { get; set; }

        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id +",";
            result += " Battery: " + Battery;
            result += '\n';
            return result;
        }
    }
}
