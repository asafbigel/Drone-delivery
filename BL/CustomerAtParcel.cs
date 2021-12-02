using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class CustomerAtParcel
    {
        public int id { get; set; }
        public string customer_name { get; set; }

        public override string ToString()
        {
            string result = "";
            result += "ID: " + id;
            result += " Name: " + customer_name;
            return result;
;       }
    }
}

