using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneInCharging
    {
        public int Id { get; set; }
     
        public double Battery { get; set; }

        public DateTime EnterToCharge{ get; set; }

        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id +",";
            result += " Battery: " + Battery + ",";
            result += " Enter to charging: " + EnterToCharge + ",";
            result += " Time in charging: " + (DateTime.Now-EnterToCharge);
            result += '\n';
            return result;
        }
    }
}
