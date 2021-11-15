using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class ParcelAtCustomer
    {
        int id;
        WeightCategories weight;
        Priorities priority;
        ParcelStatuses status;
        CustomerAtParcel other_customer;
    }
}
