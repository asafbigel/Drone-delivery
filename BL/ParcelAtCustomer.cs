using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelAtCustomer
    {
        public int id { get; set; }
        public WeightCategories weight { get; set; }
        public Priorities priority { get; set; }
        public ParcelStatuses status { get; set; }
        public CustomerAtParcel other_customer { get; set; }
    }
}
