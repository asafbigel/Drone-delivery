using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class CustomerToList
    {
        public int id { get; set; }
        public string name { get; set; }
        public int phone { get; set; }
        public int num_of_parcels_sent_and_arrived { get; set; }
        public int num_of_parcels_sent_and_not_arrived { get; set; }
        public int num_of_parcels_got { get; set; }
        public int num_of_parcels_to_get { get; set; }
    }
}
