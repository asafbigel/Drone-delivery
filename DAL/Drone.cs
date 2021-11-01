﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public DroneStatuses Status { get; set; }
            public double Battery { get; set; }
            public override string ToString()
            {
                string result = "";
                result += "ID: " + Id;
                result += "\nModel: " + Model;
                result += "\nMaxWeight: " + MaxWeight;
                result += "\nStatus: " + Status;
                result += "\nBattery: " + Battery;
                return result;
            }
        }
    }
}