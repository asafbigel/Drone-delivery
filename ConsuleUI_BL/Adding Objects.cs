using System;
using IBL.BO;
using System.Collections.Generic;

namespace ConsuleUI_BL
{
    public partial class ConsuleUI_BL
    {
        private static void add_baseStation()
        {
            Console.Write("Enter station num: ");
            int my_id = int.Parse(Console.ReadLine());
            Console.Write("Enter name: ");
            string my_name = Console.ReadLine();
            Location my_location = input_location();
            Console.Write("Enter slots: ");
            int chargeSlots = int.Parse(Console.ReadLine());
            BaseStation baseStation = new BaseStation()
            {
                id = my_id,
                name = my_name,
                Num_Free_slots_charge = chargeSlots,
                space = my_location
            };
            mybi.Add_base_station(baseStation);
        }


        private static void Add_parcel()
        {
            Console.Write("Enter sender id: ");
            int sender_id = int.Parse(Console.ReadLine());
            Console.Write("Enter getter id: ");
            int getter_id = int.Parse(Console.ReadLine());
            Console.Write("Enter weight: ");
            WeightCategories weight = (WeightCategories)Enum.Parse(typeof(WeightCategories), Console.ReadLine());
            Console.Write("Enter priority: ");
            Priorities priority = (Priorities)Enum.Parse(typeof(Priorities), Console.ReadLine());
            Console.Write("Enter getter id: "); 
            Parcel parcel= new Parcel()
            {
                priority = priority,
                 weight = weight
            };
            mybi.Add_parcel(parcel, sender_id, getter_id);
        }

        private static void Add_customer()
        {
            Console.Write("Enter id num: ");
            int my_id = int.Parse(Console.ReadLine());
            Console.Write("Enter name: ");
            string my_name = Console.ReadLine();
            Console.Write("Enter phone: ");
            string my_phone = Console.ReadLine();
            Location my_location = input_location();
            Customer customer = new Customer()
            {
                id = my_id,
                name = my_name,
                phone = my_phone,
                space = my_location,
                parcels_at_customer_for = new List<Parcel>(),
                parcels_at_customer_from = new List<Parcel>()
            };
            mybi.Add_customer(customer);
        }

        private static void Add_drone()
        {
            Console.Write("Enter serial num: ");
            int my_id = int.Parse(Console.ReadLine());
            Console.Write("Enter model: ");
            string model = Console.ReadLine();
            Console.Write("Enter max weight: ");
            WeightCategories weight = (WeightCategories)Enum.Parse(typeof(WeightCategories), Console.ReadLine());
            Console.Write("Enter base station number: ");
            int baseStation_num = int.Parse(Console.ReadLine());
            Drone drone = new Drone()
            {
                Id = my_id,
                MaxWeight = weight,
                Model = model
            };
            mybi.Add_drone(drone, baseStation_num);
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
}


