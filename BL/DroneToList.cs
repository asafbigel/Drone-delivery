using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneToList
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public double Battery { get; set; }
        public DroneStatuses Status { get; set; }
        Space location { get; set; };
        int numOfParcel { get; set; };
        /*public override string ToString()
        {
            string result = "";
            result += "ID: " + Id;
            result += " Model: " + Model;
            result += " MaxWeight: " + MaxWeight;
            result += " Status: " + Status;
            result += " Battery: " + Battery;
            result += '\n';
            return result;
        }*/
    }


}
}
