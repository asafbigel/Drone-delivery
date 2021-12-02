using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class CustomerAtParcel
    {
        public int Id { get; set; }
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

