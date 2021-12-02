using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class CustomerToList
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public int num_of_parcels_sent_and_arrived { get; set; }
        public int num_of_parcels_sent_and_not_arrived { get; set; }
        public int num_of_parcels_got { get; set; }
        public int num_of_parcels_to_get { get; set; }

        public override string ToString()
        {
            string result = "";
            result += "ID: " + id;
            result += " Name: " + name;
            result += " Phone: " + phone;
            result += " num of parcels sent and arrived: " + num_of_parcels_sent_and_arrived;
            result += " num of parcels sent and not arrived: " + num_of_parcels_sent_and_not_arrived;
            result += " num of parcels that the customer got: " + num_of_parcels_got;
            result += " num of parcels that the customer  will get: " + num_of_parcels_to_get + "\n";
            
            return result;
        }
    }
}
