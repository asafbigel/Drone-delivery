using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Customer Class to represent a Customer that sent or target of the Parcrl in the Parcel Class in the Business layer
    /// </summary>
    public class CustomerAtParcel
    {
        /// <summary>
        ///  id number of the Customer
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// the name of the Customer
        /// </summary>
        public string CustomerName { get; set; }

        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id + ",";
            result += " Name: " + CustomerName ;
            return result;
;       }
    }
}

