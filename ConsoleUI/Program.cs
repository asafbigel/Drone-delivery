using System;

namespace ConsoleUI
{
    public enum options
    {
       exit, new_base_station, new_drone, new_customer, new_parcel, 
       connect_parcel_to_drone, take_parcel_by_drone, delivery_parcel_to_customer, 
       send_drone_to_charge, put_out_drone_from_charge, print_a_base_station, print_a_drone, print_a_customer, print_a_parcel, 
       print_all_base_stations, print_all_drones, print_all_customers, 
       print_all_parcels, print_all_parcels_that_have_not_yet_been_connect_to_drone, 
       print_all_base_stations_with_free_charge_slot
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose what to do:");
           
            // Adding options
            Console.WriteLine("1: to add new base station");
            Console.WriteLine("2: to add new drone");
            Console.WriteLine("3: to add new customer");
            Console.WriteLine("4: to add new parcel");
            
            // Update options
            Console.WriteLine("5: to connect parcel to drone");
            Console.WriteLine("6: to take parcel by drone");
            Console.WriteLine("7: to delivery parcel to customer");
            Console.WriteLine("8: to send drone to charge");
            Console.WriteLine("9: to put out drone from charge");

            // disply options
            Console.WriteLine("10: to print a base station");
            Console.WriteLine("11: to print a drone");
            Console.WriteLine("12: to print a customer");
            Console.WriteLine("13: to print a parcel");

            // disply list options
            Console.WriteLine("14: to print all base stations");
            Console.WriteLine("15: to print all drones");
            Console.WriteLine("16: to print all customers");
            Console.WriteLine("17: to print all parcels");
            Console.WriteLine("18: to print all parcels that have not yet been connect to drone");
            Console.WriteLine("19: to print all base stations with free charge slot");
            
            
            Console.WriteLine("0: to exit");

            string input =Console.ReadLine();
            options option = (options) Enum.Parse(typeof(options), input);

            while (option != options.exit)
            {
                switch (option)
                {
                    case options.new_base_station:
                        break;
                    case options.new_drone:
                        break;
                    case options.new_customer:
                        break;
                    case options.new_parcel:
                        break;
                    case options.connect_parcel_to_drone:
                        break;
                    case options.take_parcel_by_drone:
                        break;
                    case options.delivery_parcel_to_customer:
                        break;
                    case options.send_drone_to_charge:
                        break;
                    case options.put_out_drone_from_charge:
                        break;
                    case options.print_a_base_station:
                        break;
                    case options.print_a_drone:
                        break;
                    case options.print_a_customer:
                        break;
                    case options.print_a_parcel:
                        break;
                    case options.print_all_base_stations:
                        break;
                    case options.print_all_drones:
                        break;
                    case options.print_all_customers:
                        break;
                    case options.print_all_parcels:
                        break;
                    case options.print_all_parcels_that_have_not_yet_been_connect_to_drone:
                        break;
                    case options.print_all_base_stations_with_free_charge_slot:
                        break;

                    default:
                        Console.WriteLine("Wrong input. Try again");
                        break;
                }

                Console.WriteLine("Choose what to do:");
                input = Console.ReadLine();
                option = (options)Enum.Parse(typeof(options), input);
            }
        }
    }
}
