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
        public void update_customer(int id, string new_name, string new_phone)
        {
            IDAL.DO.Customer customer = mydal.Find_customer(id);
            if (new_name != "_")
                customer.Name = new_name;
            if (new_phone != "_")
                customer.Phone = new_phone;

        }
        public void print_customer(int customer_id)
        {
            Console.WriteLine(convertor(mydal.Find_customer(customer_id)));
        }
        public void print_all_customers()
        {
            List<CustomerToList> customers = convertor(mydal.Get_all_customers());
            foreach (var item in customers)
            {
                Console.WriteLine(item);
            }
        }
    }
}