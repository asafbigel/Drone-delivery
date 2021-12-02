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
        public Location TheLocation { get; set; }
        public List<Parcel> parcels_at_customer_from { get; set; }//.///////need to be      List<ParcelAtCustomer>
        public List<Parcel> parcels_at_customer_for { get; set; }//.///////need to be      List<ParcelAtCustomer>

        public override string ToString()
        {
            string result = "";
            result += "ID: " + id;
            result += " Name: " + name;
            result += " Phone: " + phone;
            result += TheLocation;
            result += "parcels from the customer:\n";
            foreach (var item in parcels_at_customer_from)
            {
                result += item.ToString();
            }
            result += "parcels for the customer:\n";
            foreach (var item in parcels_at_customer_for)
            {
                result += item.ToString();
            }
            return result;
        }
    }
}
