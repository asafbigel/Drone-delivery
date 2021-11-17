using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class BaseStationToList
    {
        public int id { get; set; }
        public string name { get; set; }
        public int num_of_free_slots { get; set; }
        public int num_of_busy_slots { get; set; }
    }
}
