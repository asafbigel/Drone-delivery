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
        public string NameSender { get; set; }
        public string NameGetter { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public ParcelStatuses Status { get; set; }

        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id;
            result += " Sender: " + NameSender;
            result += " Target: " + NameGetter;
            result += " Weight: " + Weight;
            result += " Priority: " + Priority;
            result += " Status: " + Status + "\n";
            return result;
        }
    }

}
