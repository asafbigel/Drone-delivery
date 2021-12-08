using System;
using IBL.BO;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ConsuleUI_BL
{
    public partial class ConsuleUI_BL
    {
        private static void add_baseStation()
        {
            Console.Write("Enter station num: ");
            if (!int.TryParse(Console.ReadLine(), out int my_id)) { throw new IntReadException(); }
            Console.Write("Enter name: ");
            string my_name = Console.ReadLine();
            Location my_location = input_location();
            Console.Write("Enter slots: ");
            if (!int.TryParse(Console.ReadLine(), out int chargeSlots)) { throw new IntReadException(); }
            BaseStation baseStation = new BaseStation()
            {
                Id = my_id,
                Name = my_name,
                Num_Free_slots_charge = chargeSlots,
                BaseStationLocation = my_location
            };
            mybi.Add_base_station(baseStation);
        }
        private static void Add_parcel()
        {
            Console.Write("Enter sender id: ");
            if (!int.TryParse(Console.ReadLine(), out int sender_id)) { throw new IntReadException(); }
            Console.Write("Enter getter id: ");
            if (!int.TryParse(Console.ReadLine(), out int getter_id)) { throw new IntReadException(); }
            Console.Write("Enter max weight (0: light,  1: medium,  2: heavy): ");
            string input = Console.ReadLine();
            if (input != "0" && input != "1" && input != "2")
                throw new InputException("not invalid weight");
            WeightCategories weight = (WeightCategories)Enum.Parse(typeof(WeightCategories), input);
            Console.Write("Enter priority (0: regular,  1: fast,  2: emergency): ");
            input = Console.ReadLine();
            if (input != "0" && input != "1" && input != "2")
                throw new InputException("not invalid weight");
            Priorities priority = (Priorities)Enum.Parse(typeof(Priorities), input);
            Parcel parcel = new Parcel()
            {
                Priority = priority,
                Weight = weight,
                Requested = DateTime.Now
            };
            mybi.Add_parcel(parcel, sender_id, getter_id);
        }
        private static void Add_customer()
        {
            Customer customer = new Customer();
            Console.Write("Enter id num: ");
            if(!int.TryParse(Console.ReadLine(), out int id)) { throw new IntReadException(); }
            customer.Id = id;
            Console.Write("Enter name: ");
            customer.Name = Console.ReadLine();
            Console.Write("Enter phone: ");
            customer.Phone = Console.ReadLine();
            customer .CustomerLocation = input_location();

            mybi.Add_customer(customer);
        }
        private static void Add_drone()
        {
            Drone drone = new Drone();
            Console.Write("Enter serial num: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { throw new InputException("not invalid num input"); }
            drone.Id = id;
            Console.Write("Enter model: ");
            drone.Model = Console.ReadLine();
            Console.Write("Enter max weight (0: light,  1: medium,  2: heavy): ");
            string input = Console.ReadLine();
            if (input != "0" && input != "1" && input != "2")
                throw new InputException("not invalid weight");
            drone.MaxWeight = (WeightCategories)Enum.Parse(typeof(WeightCategories),input);
            Console.Write("Enter base station number: ");
            if(!int.TryParse(Console.ReadLine(), out int baseStationNum)) { throw new InputException("not invalid num input"); }
            mybi.Add_drone(drone, baseStationNum);
        }

        private static Location input_location()
        {
            Console.Write("Enter longitude: ");
            double my_longitude = double.Parse(Console.ReadLine());
            Console.Write("Enter latitude: ");
            double my_latitude = double.Parse(Console.ReadLine());
            Location space = new Location
            {
                latitude = my_latitude,
                longitude = my_longitude
            };
            return space;
        }
    }

    [Serializable]
    internal class IntReadException : Exception
    {
        public IntReadException()
        {
        }

        public IntReadException(string message) : base(message)
        {
        }

        public IntReadException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IntReadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}


