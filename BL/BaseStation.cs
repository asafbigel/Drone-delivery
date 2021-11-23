using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class BaseStation
    {
        public int id { get; set; }
        public string name { get; set; }
        public Location space { get; set; }
        public int Num_Free_slots_charge { get; set; }
        public List<DroneInCharging> DroneInChargings { get; set; }
    }
}
