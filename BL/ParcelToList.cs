using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IBL.BO
{
    class ParcelToList
    {
        public int id { get; set; }
        public string sender { get; set; }
        public string getter { get; set; }
        public WeightCategories weight { get; set; }
        public Priorities priority { get; set; }
        public ParcelStatuses status { get; set; }
    }
}
