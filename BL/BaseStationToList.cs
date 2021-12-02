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
        public string name { get; set; }
        public int num_of_free_slots { get; set; }
        public int num_of_busy_slots { get; set; }
        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id;
            result += " Name: " + name;
            result += " num of free slots: " + num_of_free_slots;
            result += " num of busy slots: " + num_of_busy_slots;
 
            return result;
        }

    }
}
