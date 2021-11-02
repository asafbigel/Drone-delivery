using System;
using DalObject;
using IDAL.DO;
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
            DalObject.DalObject dalObject1;
            
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
                        Console.Write("Enter id: ");
                        string my_input = Console.ReadLine();
                        int id = int.Parse(my_input);

                       Console.Write("Enter name: ");
                        my_input = Console.ReadLine();
                        string name = my_input;

                       Console.Write("Enter longitude: ");
                        my_input = Console.ReadLine();
                        double longitude = double.Parse(my_input);

                       Console.Write("Enter lattitude: ");
                        my_input = Console.ReadLine();
                        double lattitude = double.Parse(my_input);
                        
                       Console.Write("Enter id: ");
                        my_input = Console.ReadLine();
                        int chargeSlots = int.Parse(my_input);

                        dalObject1.add_base_station(id, name, longitude, lattitude, chargeSlots);
                        break;


                    case options.new_drone:
                        Console.Write("Enter id: ");
                        my_input = Console.ReadLine();
                        id = int.Parse(my_input);

                        Console.Write("Enter model: ");
                        my_input = Console.ReadLine();
                        string model = my_input;

                        Console.Write("Enter maxWeight: ");
                        my_input = Console.ReadLine();
                        WeightCategories maxWeight = WeightCategories.Parse(my_input);

                        Console.Write("Enter status: ");
                        my_input = Console.ReadLine();
                        DroneStatuses status = DroneStatuses.Parse(my_input);

                        Console.Write("Enter battery: ");
                        my_input = Console.ReadLine();
                        double battery = double.Parse(my_input);

                        DalObject.DalObject.add_drone(id, model, maxWeight, status, battery);

                        break;



                    case options.new_customer:
                        Console.Write("Enter id: ");
                        my_input = Console.ReadLine();
                        id = int.Parse(my_input);

                        Console.Write("Enter name: ");
                        my_input = Console.ReadLine();
                        name = my_input;

                        Console.Write("Enter phone: ");
                        my_input = Console.ReadLine();
                        string phone = my_input;

                        Console.Write("Enter longitude: ");
                        my_input = Console.ReadLine();
                        longitude = double.Parse(my_input);

                        Console.Write("Enter latitude: ");
                        my_input = Console.ReadLine();
                        lattitude = double.Parse(my_input);

                        DalObject.DalObject.add_customer(id, name, phone, longitude, lattitude);
                        break;



                    case options.new_parcel:

                        Console.Write("Enter id: ");
                        my_input = Console.ReadLine();
                        id = int.Parse(my_input);

                        Console.Write("Enter sender Id: ");
                        my_input = Console.ReadLine();
                        int my_senderId = int.Parse(my_input);

                        Console.Write("Enter target Id: ");
                        my_input = Console.ReadLine();
                        int my_targetId = int.Parse(my_input);

                        Console.Write("Enter Weight: ");
                        my_input = Console.ReadLine();
                        WeightCategories my_Weight = WeightCategories.Parse(my_input);

                        Console.Write("Enter Priority: ");
                        my_input = Console.ReadLine();
                        Priorities my_Priority = Priorities.Parse(my_input);

                        Console.Write("Enter Drone Id: ");
                        my_input = Console.ReadLine();
                        int my_DroneId = int.Parse(my_input);

                        Console.Write("Enter Scheduled: ");
                        my_input = Console.ReadLine();
                        DateTime my_Scheduled = DateTime.Parse(my_input);
                       
                        Console.Write("Enter PickedUp: ");
                        my_input = Console.ReadLine();
                        DateTime my_PickedUp = DateTime.Parse(my_input);

                        Console.Write("Enter Delivered: ");
                        my_input = Console.ReadLine();
                        DateTime my_Delivered = DateTime.Parse(my_input);

                        DalObject.DalObject.add_parcel(id, my_senderId, my_targetId,  my_Weight,  my_Priority, my_DroneId,  my_Scheduled, my_PickedUp, my_Delivered);
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
