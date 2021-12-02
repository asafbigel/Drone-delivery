using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class BaseStationToList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfFreeSlots { get; set; }
        public int NumOfBusySlots { get; set; }
        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id + ",";
            result += " Name: " + Name + ",";
            result += " num of free slots: " + NumOfFreeSlots + ",";
            result += " num of busy slots: " + NumOfBusySlots;
 
            return result;
        }

    }
}
