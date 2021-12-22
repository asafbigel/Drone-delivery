using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneToList
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public double Battery { get; set; }
        public DroneStatuses Status { get; set; }
        public Location DroneLocation { get; set; }
        public int NumOfParcel { get; set; }
        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id + ",";
            result += " Model: " + Model + ",";
            result += " MaxWeight: " + MaxWeight + ",";
            result += " Battery: " + Battery + ",";
            result += " Status: " + Status + ",";
            result += " Location: " + DroneLocation + ",";
            result += " Number Of the Parcel: " + NumOfParcel;
            //result += '\n';
            return result;
        }
    }


}

