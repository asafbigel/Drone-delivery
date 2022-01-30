using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

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
            if (customer.Id <= 0) throw new CustomerIdExeption("Invalid Customer Id");
            if (customer.Name == "" || customer.Name == null) throw new CustomerExeption("Enter Name");
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
        public void update_customer(int id, string new_name, string new_phone,string password,double lattitude, double longitude)
        {
            lock (mydal)
            {
                DO.Customer customer = mydal.Find_customer(id);
                if (new_name != "_")
                    customer.Name = new_name;
                if (new_phone != "_")
                    customer.Phone = new_phone;
                customer.Password = password;
                customer.Lattitude = lattitude;
                customer.Longitude = longitude;
                mydal.UpdateCustomer(customer);
            }
        }
        public void updateCustomer(Customer myCustomer)
        {
            DO.Customer customer = convertor(myCustomer);
            mydal.UpdateCustomer(customer);
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
        public CustomerAtParcel GetCustomerAtParcel(int id)
        {
            var customer = mydal.Find_customer(id);
            if(customer.Id == 0)
                throw new CustomerExeption("Customer not found");
            Customer myCustomer = convertor(customer);
            return convertor1(myCustomer);
        }
        public Customer GetCustomer(int id)
        {
            var customer = mydal.Find_customer(id);
            if (customer.Id == 0)
                throw new CustomerExeption("Customer not found");
            return convertor(customer);
            
        }
        public Customer GetCustomer(CustomerToList customer)
        {
            return GetCustomer(customer.Id);
        }


        public bool CheckManagerLogin( string userName,  string password)
        {
            
            if (userName == "admin" && password == "123")
                return true;
            return false;
        }
        public bool CheckCustomerLogin(int id, string password)
        {
            Customer customer = GetCustomer(id);
            if (id == customer.Id && password == customer.Password)
                return true;
            return false;
        }
        
    }

    
}


