using System.Collections.Generic;

namespace BO
{
    /// <summary>
    /// Customer Class to represent a Customer of Parcels delivery service in the Business layer
    /// </summary>
    public class Customer
    {
        /// <summary>
        ///  id number of the Customer
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// the name of the Customer
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// the Phone of the Customer
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        ///  login Password of the Customer
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// loction of the Customer
        /// </summary>
        public Location CustomerLocation { get; set; }
        /// <summary>
        /// list of  Parcels that the Customer sent
        /// </summary>
        public List<ParcelAtCustomer> ParcelsAtCustomerFrom { get; set; }
        /// <summary>
        /// list of  Parcels that sent tothe Customer
        /// </summary>
        public List<ParcelAtCustomer> ParcelsAtCustomerFor { get; set; }

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
            foreach (var item in ParcelsAtCustomerFor)
            {
                result += "\t";
                result += item.ToString();
                result += "\n";
            }
            return result;
        }
    }
}
