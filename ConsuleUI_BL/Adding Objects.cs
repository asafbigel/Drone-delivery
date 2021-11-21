using System;
using IBL.BO;
using System.Collections.Generic;

namespace ConsuleUI_BL
{
    public partial class ConsuleUI_BL
    {
        private static void Add_baseStation()
        {
            Console.Write("Enter station num: ");
            int my_id = int.Parse(Console.ReadLine());
            Console.Write("Enter name: ");
            string my_name = Console.ReadLine();
            Space my_space = input_location();
            Console.Write("Enter slots: ");
            int chargeSlots = int.Parse(Console.ReadLine());
            BaseStation baseStation = new BaseStation()
            {
                DroneInChargings = new List<DroneInCharging>(),
                id = my_id,
                name = my_name,
                Num_Free_slots_charge = chargeSlots,
                space = my_space
            };
            mybi.Add_base_station(baseStation);
        }


        private static void Add_parcel()
        {

        }

        private static void Add_customer()
        {
        }

        private static void Add_drone()
        {
            Console.Write("Enter serial num: ");
            int my_id = int.Parse(Console.ReadLine());
            Console.Write("Enter model: ");
            string model = Console.ReadLine();
            Space my_space = input_location();
            Console.Write("Enter slots: ");
            int chargeSlots = int.Parse(Console.ReadLine());
            BaseStation baseStation = new BaseStation()
            {
                DroneInChargings = new List<DroneInCharging>(),
                id = my_id,
                name = my_name,
                Num_Free_slots_charge = chargeSlots,
                space = my_space
            };
            mybi.Add_base_station(baseStation);
        }





        private static Space input_location()
        {
            Console.Write("Enter longitude: ");
            double my_longitude = double.Parse(Console.ReadLine());
            Console.Write("Enter latitude: ");
            double my_latitude = double.Parse(Console.ReadLine());
            Space space = new Space
            {
                latitude = my_latitude,
                longitude = my_longitude
            };
            return space;
        }
    }
}


