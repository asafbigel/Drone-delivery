using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Location CustomerLocation { get; set; }
        public List<ParcelAtCustomer> ParcelsAtCustomerFrom { get; set; }
        public List<ParcelAtCustomer> parcelsAtCustomerFor { get; set; }
        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id;
            result += " Name: " + Name;
            result += " Phone: " + Phone;
            result += CustomerLocation;
            result += "\nparcels from the customer:\n";
            foreach (var item in ParcelsAtCustomerFrom)
            {
                result += "\t";
                result += item.ToString();
                result += "\n";

            }
            result += "\nparcels for the customer:\n";
            foreach (var item in parcelsAtCustomerFor)
            {
                result += "\t";
                result += item.ToString();
            }
            return result;
        }
    }
}
