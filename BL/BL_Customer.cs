using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public partial class BL
    {
        public void Add_customer(Customer customer)
        {
            IDAL.DO.Customer idalCustomer = convertor(customer);
            mydal.Add_customer(idalCustomer);
        }


    }
}