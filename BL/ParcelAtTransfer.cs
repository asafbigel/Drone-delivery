using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelAtTransfer
    {
        public int id { get; set; }
        public bool sateOfParcel { get; set; }
        public Priorities priority { get; set; }
        public WeightCategories weight { get; set; }
        public CustomerAtParcel sender { get; set; }
        public CustomerAtParcel getter { get; set; }
        public Location spaceOfPickUp { get; set; }
        public Location spaceOfTarget { get; set; }
        public double distanceOfDelivery { get; set; }

    }
}
