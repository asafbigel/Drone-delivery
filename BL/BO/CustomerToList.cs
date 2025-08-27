using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{/// <summary>
/// Customer Class to represent a Customer of Parcels delivery service in list of customers in the Business layer
/// </summary>
    public class CustomerToList
    {
        /// <summary>
        /// id number of the Customer
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// the name of the Customer
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///  the Phone of the Customer
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// number Of Parcels that the Customer sent and they arrived to their target 
        /// </summary>
        public int NumOfParcelsSentAndArrived { get; set; }
        /// <summary>
        /// number Of Parcels that the Customer sent and they still not arrived to their target 
        /// </summary>
        public int NumOfParcelsSentAndNotArrived { get; set; }
        /// <summary>
        /// number Of Parcels that the Customer got 
        /// </summary>
        public int NumOfParcelsGot { get; set; }
        /// <summary>
        /// number Of Parcels that sent to the  Customer that still not arrived to the Customer
        /// </summary>
        public int numOfParcelsToGet { get; set; }
        public Location CustomerLocation { get; set; }

        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id +",";
            result += " Name: " + Name + ",";
            result += " Phone: " + Phone + ",";
            result += " num of parcels sent and arrived: " + NumOfParcelsSentAndArrived + ",";
            result += " num of parcels sent and not arrived: " + NumOfParcelsSentAndNotArrived + ",";
            result += " num of parcels that the customer got: " + NumOfParcelsGot + ",";
            result += " num of parcels that the customer  will get: " + numOfParcelsToGet;
            //+ "\n";
            
            return result;
        }
    }
}
