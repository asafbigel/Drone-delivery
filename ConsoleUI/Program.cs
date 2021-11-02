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
        static DalObject.DalObject dalObject1 = new  DalObject.DalObject();
        static void Main(string[] args)
        {
            menu();

            string input = Console.ReadLine();
            options option = (options)Enum.Parse(typeof(options), input);

            string the_input = " ";
            int my_id;

            while (option != options.exit)
            {
                switch (option)
                {
                    case options.new_base_station:
                        Console.Write("Enter id: ");
                        the_input = Console.ReadLine();
                        my_id = int.Parse(the_input);

                        Console.Write("Enter name: ");
                        the_input = Console.ReadLine();
                        string name = the_input;

                        Console.Write("Enter longitude: ");
                       the_input = Console.ReadLine();
                        double longitude = double.Parse(the_input);

                        Console.Write("Enter lattitude: ");
                       the_input = Console.ReadLine();
                        double lattitude = double.Parse(the_input);

                        Console.Write("Enter id: ");
                       the_input = Console.ReadLine();
                        int chargeSlots = int.Parse(the_input);
                        BaseStation baseStation = new BaseStation()
                        {
                            Id = my_id,
                            ChargeSlots = chargeSlots,
                            Lattitude = lattitude,
                            Longitude = longitude,
                            Name = name
                        };
                        dalObject1.Add_base_station(baseStation);
                        break;


                    case options.new_drone:
                        Console.Write("Enter id: ");
                       the_input = Console.ReadLine();
                        id = int.Parse(the_input);

                        Console.Write("Enter model: ");
                       the_input = Console.ReadLine();
                        string model =the_input;

                        Console.Write("Enter maxWeight: ");
                       the_input = Console.ReadLine();
                        WeightCategories maxWeight = WeightCategories.Parse(the_input);

                        Console.Write("Enter status: ");
                       the_input = Console.ReadLine();
                        DroneStatuses status = (DroneStatuses)Enum.Parse(typeof(DroneStatuses),the_input);


                        Console.Write("Enter battery: ");
                       the_input = Console.ReadLine();
                        double battery = double.Parse(the_input);

                        
                        Drone drone = new Drone()
                        {

                            Id = id,
                            Model = model,
                            MaxWeight = maxWeight,
                            Status = status,
                            Battery = battery
                        };

                        dalObject1.Add_drone(drone);
                        break;



                    case options.new_customer:
                        Console.Write("Enter id: ");
                       the_input = Console.ReadLine();
                        id = int.Parse(the_input);

                        Console.Write("Enter name: ");
                       the_input = Console.ReadLine();
                        name =the_input;

                        Console.Write("Enter phone: ");
                       the_input = Console.ReadLine();
                        string phone =the_input;

                        Console.Write("Enter longitude: ");
                       the_input = Console.ReadLine();
                        longitude = double.Parse(the_input);

                        Console.Write("Enter Lattitude: ");
                       the_input = Console.ReadLine();
                        lattitude = double.Parse(the_input);

                        
                        Customer customer = new Customer()
                        {

                            Id = id,
                            Name = name,
                            Phone = phone,
                            Longitude = longitude,
                            Lattitude = lattitude
                        };

                        dalObject1.Add_customer(customer);
                        break;

                        



                    case options.new_parcel:

                        Console.Write("Enter id: ");
                       the_input = Console.ReadLine();
                        id = int.Parse(the_input);

                        Console.Write("Enter sender Id: ");
                       the_input = Console.ReadLine();
                        int my_senderId = int.Parse(the_input);

                        Console.Write("Enter target Id: ");
                       the_input = Console.ReadLine();
                        int my_targetId = int.Parse(the_input);

                        Console.Write("Enter Weight: ");
                       the_input = Console.ReadLine();
                        WeightCategories my_Weight = WeightCategories.Parse(the_input);

                        Console.Write("Enter Priority: ");
                       the_input = Console.ReadLine();
                        Priorities my_Priority = Priorities.Parse(the_input);

                        Console.Write("Enter Drone Id: ");
                       the_input = Console.ReadLine();
                        int my_DroneId = int.Parse(the_input);

                        Console.Write("Enter Scheduled: ");
                       the_input = Console.ReadLine();
                        DateTime my_Scheduled = DateTime.Parse(the_input);

                        Console.Write("Enter PickedUp: ");
                       the_input = Console.ReadLine();
                        DateTime my_PickedUp = DateTime.Parse(the_input);

                        Console.Write("Enter Delivered: ");
                       the_input = Console.ReadLine();
                        DateTime my_Delivered = DateTime.Parse(the_input);
                       
                        Parcel parcel = new Parcel()
                        {

                            Id = id,
                            senderId = my_senderId,
                            targetId = my_targetId,
                            Weight = my_Weight,
                            Priority = my_Priority,
                            DroneId = my_DroneId,
                            Scheduled = my_Scheduled,
                            PickedUp = my_PickedUp,
                            Delivered = my_Delivered
                        };

                        dalObject1.Add_parcel(parcel);
                        break;
                       

                        


                    case options.connect_parcel_to_drone:
                        // הנה הרחפנים, תבחר אחד מהם ותעדכן
                        break;
                    case options.take_parcel_by_drone:
                        break;
                    case options.delivery_parcel_to_customer:
                        break;
                    case options.send_drone_to_charge:
                        break;
                    case options.put_out_drone_from_charge:
                        break;
                        //\\\//\\//\\///\\\///\\\//\\//\\//\\//\\//\\//\\//\\//\\\///\\\///\\\\
                    case options.print_a_base_station:
                        Console.Write("Enter id: ");
                        stringthe_input = Console.ReadLine();
                        int id = int.Parse(the_input);

                        break;

                    case options.print_a_drone:
                        break;
                    case options.print_a_customer:
                        break;
                    case options.print_a_parcel:
                        break;
                    case options.print_all_base_stations:
                        foreach (BaseStation item in dalObject1.GetAllBaseStaition())
                        {
                            Console.WriteLine(item);
                        }
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

        private static void menu()
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
        }
    }
}
