using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IBL.BO
{
    public class ParcelToList
    {
        public int Id { get; set; }
        public string SenderName { get; set; }
        public string GetterName { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public ParcelStatuses Status { get; set; }

        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id + ",";
            result += " Sender: " + SenderName + ",";
            result += " Target: " + GetterName + ",";
            result += " Weight: " + Weight + ",";
            result += " Priority: " + Priority + ",";
            result += " Status: " + Status + "\n";
            return result;
        }
    }

}
