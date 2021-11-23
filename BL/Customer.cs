using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Customer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public Space space { get; set; }
        public List<Parcel> parcels_at_customer_from { get; set; }
        public List<Parcel> parcels_at_customer_for { get; set; }
    }
}
