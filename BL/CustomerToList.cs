using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class CustomerToList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int NumOfParcelsSentAndArrived { get; set; }
        public int NumOfParcelsSentAndNotArrived { get; set; }
        public int NumOfParcelsGot { get; set; }
        public int numOfParcelsToGet { get; set; }

        public override string ToString()
        {
            string result = "";
            result += "ID: " + Id +",";
            result += " Name: " + Name + ",";
            result += " Phone: " + Phone + ",";
            result += " num of parcels sent and arrived: " + NumOfParcelsSentAndArrived + ",";
            result += " num of parcels sent and not arrived: " + NumOfParcelsSentAndNotArrived + ",";
            result += " num of parcels that the customer got: " + NumOfParcelsGot + ",";
            result += " num of parcels that the customer  will get: " + numOfParcelsToGet + "\n";
            
            return result;
        }
    }
}
