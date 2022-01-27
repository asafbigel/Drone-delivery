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
            if (customer.Id == 0) throw new CustomerExeption("Invalid Id");
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
        public void updateCustomer(Customer myCustomer)
        {
            update_customer(myCustomer.Id, myCustomer.Name, myCustomer.Phone);
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
            return convertor3(customer);
        }


        //\////////////////////////////////////////////////////////////////////baseStation/////////////////////////////////////////
        /*
        /// <summary>
        /// fanction that update baseStation
        /// </summary>
        /// <param name="id">id of  baseStation </param>
        /// <param name="new_name"> the new name of the update baseStation   </param>
        /// <param name="new_slot">the new number of free slot charge of  the update baseStation</param>
        public void update_baseStation(int id, string new_name, string new_slot)
        {
            BaseStation baseStation = find_baseStation(id);
            if (new_name != "_")
                baseStation.Name = new_name;
            if (new_slot != "_")
            {
                if (!int.TryParse(new_slot, out int slot)) { throw new IntReadException("Wrong input"); }
                if (slot >= baseStation.Num_Free_slots_charge)
                    baseStation.Num_Free_slots_charge = slot;
                else
                    throw new slotException("There more drone at charge now");
            }
            mydal.UpdateBaseStation(convertor(baseStation));
        }
        /// <summary>
        ///  fanction that update baseStation
        /// </summary>
        /// <param name="baseStation"> the baseStation to update </param>
        public void UpdateBaseStation(BaseStation baseStation)
        {
            BaseStation OldbaseStation = find_baseStation(baseStation.Id);
            mydal.UpdateBaseStation(convertor(baseStation));
        }

        /// <summary>
        /// get BL base station
        /// </summary>
        /// <param name="id"> id of the base station</param>
        /// <returns>
        /// BL base station
        /// </returns>
        private BaseStation find_baseStation(int id)
        {
            DO.BaseStation idalBaseStation = mydal.Find_baseStation(id);
            BaseStation baseStation = convertor(idalBaseStation);
            return baseStation;
        }
        /// <summary>
        /// Add base station to the list
        /// </summary>
        /// <param name="baseStation">
        /// The base station
        /// </param>
        public void Add_base_station(BaseStation baseStation)
        {
            if (baseStation.Id == 0)
                throw new BaseStationExeption("Invalid Id");
            if (baseStation.Name == "")
                throw new BaseStationExeption("Enter Name");
            if (baseStation.Name == null)
                throw new BaseStationExeption("Invalid Name");

            baseStation.DroneInChargings = new List<DroneInCharging>();
            DO.BaseStation idalBaseStation = convertor(baseStation);
            mydal.Add_base_station(idalBaseStation);
        }
        /// <summary>
        /// return string of details of this base station
        /// </summary>
        /// <param name="baseStation_id">
        /// Id of the base station  
        /// </param>
        /// <returns>
        /// string of details of this base station
        /// </returns>
        public string string_baseStation(int baseStation_id)
        {
            return convertor(mydal.Find_baseStation(baseStation_id)).ToString();
        }
        /// <summary>
        /// ToString of all the baseStations
        /// </summary>
        /// <returns> the string of ToString of all the baseStations </returns>
        public string string_all_baseStations()
        {
            List<BaseStationToList> baseStations = convertor1(mydal.Get_all_base_stations(x => true));
            string result = "";
            foreach (var item in baseStations)
            {
                result += item.ToString();
                result += "\n";
            }
            return result;
        }
        /// <summary>
        /// ToString of all the baseStations with free slots
        /// </summary>
        /// <returns>the string of ToString of all the baseStations with free slots</returns>
        public string string_all_baseStations_with_free_slots()
        {
            List<BaseStationToList> baseStations = convertor1(mydal.Get_all_base_stations(x => x.ChargeSlots > 0));
            string result = "";
            foreach (var item in baseStations)
            {
                result += item.ToString();
                result += "\n";
            }
            return result;
        }
        public BaseStation GetBaseStation(int id)
        {
            var baseStation = mydal.Find_baseStation(id);
            if (baseStation.Id == 0)
                throw new BaseStationExeption("baseStation not found");
            return convertor(baseStation);

        }

        public IEnumerable<BaseStationToList> GetAllBaseStations(Predicate<BaseStationToList> match)
        {
            IEnumerable<DO.BaseStation> list = mydal.Get_all_base_stations(item => true);
            return convertor1(list).FindAll(match);
        }*/
    }











}


