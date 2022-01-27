using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Runtime.CompilerServices;


namespace BL
{
    public partial class BL
    {
        /// <summary>
        /// A function that add a new customer
        /// </summary>
        /// <param name="customer"> the name of the new customer to add </param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Add_customer(Customer customer)
        {
            lock (mydal)
            {
                DO.Customer idalCustomer = convertor(customer);
                mydal.Add_customer(idalCustomer);
            }
        }
        /// <summary>
        /// A function that update customer
        /// </summary>
        /// <param name="id">id of the customer </param>
        /// <param name="new_name"> the name after the update </param>
        /// <param name="new_phone">the phone after the update </param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void update_customer(int id, string new_name, string new_phone)
        {
            lock (mydal)
            {
                DO.Customer customer = mydal.Find_customer(id);
                if (new_name != "_")
                    customer.Name = new_name;
                if (new_phone != "_")
                    customer.Phone = new_phone;
                mydal.UpdateCustomer(customer);
            }
        }
        /// <summary>
        /// A function that returns the ToString of the customer
        /// </summary>
        /// <param name="customer_id">id of the customer </param>
        /// <returns>ToString of the customer </returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string StringCustomer(int customer_id)
        {
            lock (mydal)
                return convertor(mydal.Find_customer(customer_id)).ToString();
        }
        /// <summary>
        /// A function that returns the ToString of the list of all the customers
        /// </summary>
        /// <returns> the ToString of the list of all the customers </returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string StringAllCustomers()
        {
            lock (mydal)
            {
                string result = "";
                List<CustomerToList> customers = convertor(mydal.Get_all_customers());
                foreach (var item in customers)
                {
                    result += item.ToString();
                    result += "\n";
                }
                return result;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> GetAllCustomers(Predicate<CustomerToList> match)
        {
            lock (mydal)
                return convertor(mydal.Get_all_customers()).FindAll(match);
        }
    }
}