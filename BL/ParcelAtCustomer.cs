using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelAtCustomer
    {
        public int Id { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public ParcelStatuses Status { get; set; }
        public CustomerAtParcel OtherCustomer { get; set; }

        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id + ",";
            result += " Weight: " + Weight + ",";
            result += " Priority: " + Priority + ",";
            result += " Status: " + Status + ",";
            result += "\nThe other customer:" + OtherCustomer;
            return result;
        }
    }
}
