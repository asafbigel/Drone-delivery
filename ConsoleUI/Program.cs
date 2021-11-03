using System;
using DalObject;
using IDAL.DO;
namespace ConsoleUI
{
    public enum Options
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
        static DalObject.DalObject mydal = new  DalObject.DalObject();
        static void Main(string[] args)
        {

            show_menu();

            string input = Console.ReadLine();
            Options option = (Options)Enum.Parse(typeof(Options), input);
            int my_id;

            while (option != Options.exit)
            {
                switch (option)
                {
                    case Options.new_base_station:
                        main_new_base_station();
                        break;

                    case Options.new_drone:
                        main_add_drone();
                        break;

                    case Options.new_customer:
                        main_add_customer();
                        break;

                    case Options.new_parcel:
                        main_add_parcel();
                        break;

                    case Options.connect_parcel_to_drone:
                        main_connect_parcel_to_drone();
                        break;

                    case Options.take_parcel_by_drone:
                        //main_take_parcel_by_drone();
                        break;

                    case Options.delivery_parcel_to_customer:
                        break;

                    case Options.send_drone_to_charge:
                        Console.Write("Enter drone ID: ");
                        my_id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Choose base station frim the list:");
                        main_print_all_base_stations_with_free_charge_slot();
                        int my_baseStation_id = int.Parse(Console.ReadLine());
                        mydal.drone_to_charge(my_baseStation_id, my_id);
                        break;

                    case Options.put_out_drone_from_charge:
                        Console.Write("Enter drone ID: ");
                        my_id = int.Parse(Console.ReadLine());
                        mydal.drone_from_charge(my_id);
                        break;
                 
                        
                        //\\\//\\//\\///\\\///\\\//\\//\\//\\//\\//\\//\\//\\//\\\///\\\///\\\\
                    case Options.print_a_base_station:
                        Console.Write("Enter id: ");
                        my_id = int.Parse(Console.ReadLine());
                        BaseStation baseStation1 = mydal.Find_baseStation(my_id);
                        Console.Write(baseStation1);
                        break;

                    case Options.print_a_drone:
                        Console.Write("Enter id: ");
                        my_id = int.Parse(Console.ReadLine());
                        Drone drone1 = mydal.Find_drone(my_id);
                        Console.Write(drone1);
                        break;

                    case Options.print_a_customer:
                        Console.Write("Enter id: ");
                        my_id = int.Parse(Console.ReadLine());
                        Customer customer1 = mydal.Find_customer(my_id);
                        Console.Write(customer1);
                        break;
                       
                    case Options.print_a_parcel:
                        Console.Write("Enter id: ");
                        my_id = int.Parse(Console.ReadLine());
                        Parcel parcel1 = mydal.Find_parcel(my_id);
                        Console.Write(parcel1);
                        break;

                    case Options.print_all_base_stations:
                        BaseStation[] all_baseStations = mydal.Get_all_base_stations();
                        for (int i=0; i < mydal.GetFirstFreeBaseStation() ; i++)    
                        {
                            Console.Write(all_baseStations[i]);
                        }
                        break;

                    case Options.print_all_drones:
                        Drone[] all_drones = mydal.Get_all_drones();

                        for (int i = 0; i < mydal.GetFirstDrone(); i++)
                        {
                            Console.Write(all_drones[i]);
                        }
                        break;

                    case Options.print_all_customers:
                        Customer[] all_customers = mydal.Get_all_customers();

                        for (int i = 0; i < mydal.GetFirstCustomer(); i++)
                        {
                            Console.Write(all_customers[i]);
                        }
                        break;

                    case Options.print_all_parcels:
                        Parcel[] all_parcels = mydal.Get_all_parcels();

                        for (int i = 0; i < mydal.GetFirstFreeParcel(); i++)
                        {
                            Console.Write(all_parcels[i]);
                        }
                        break;
                        
                    case Options.print_all_parcels_that_have_not_yet_been_connect_to_drone:
                        all_parcels = mydal.Get_all_parcels();

                        for (int i = 0; i < mydal.GetFirstFreeParcel(); i++)
                        {
                            if (all_parcels[i].DroneId == 0)
                                Console.Write(all_parcels[i]);
                        }
                        break;

                    case Options.print_all_base_stations_with_free_charge_slot:
                        main_print_all_base_stations_with_free_charge_slot();
                        break;


                    default:
                        Console.WriteLine("Wrong input. Try again");
                        break;
                }

                Console.WriteLine("Choose what to do:");
                input = Console.ReadLine();
                option = (Options)Enum.Parse(typeof(Options), input);
            }
        }

        /*
        private static void main_take_parcel_by_drone()
        {
            
        }
        */
        private static void main_print_all_base_stations_with_free_charge_slot()
        {
            BaseStation[] all_baseStations = mydal.Get_all_base_stations();
            for (int i = 0; i < mydal.GetFirstFreeBaseStation(); i++)
            {
                if (all_baseStations[i].ChargeSlots > 0)
                    Console.Write(all_baseStations[i]);
            }
        }

        private static void main_connect_parcel_to_drone()
        {
            int my_id;
            Console.WriteLine("Enter parcel Id to connect:");
            my_id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter drone Id to connect:");
            int drone_id= int.Parse(Console.ReadLine());
            Parcel parcel = mydal.Find_parcel(my_id);
            parcel.DroneId = drone_id;
            mydal.UpdateParcel(parcel);
        }

        private static void main_add_parcel()
        {
            Console.Write("Enter id: ");
            int my_id = int.Parse(Console.ReadLine());

            Parcel parcel;
            main_update_parcel(my_id, out parcel);

            mydal.Add_parcel(parcel);
        }

        private static void main_update_parcel(int my_id, out Parcel parcel)
        {
            Console.Write("Enter sender Id: ");
            int my_senderId = int.Parse(Console.ReadLine());

            Console.Write("Enter target Id: ");
            int my_targetId = int.Parse(Console.ReadLine());

            Console.Write("Enter Weight: ");
            WeightCategories my_Weight = (WeightCategories)Enum.Parse(typeof(WeightCategories), Console.ReadLine());

            Console.Write("Enter Priority: ");
            Priorities my_Priority = (Priorities)Enum.Parse(typeof(Priorities), Console.ReadLine());

            Console.Write("Enter Drone Id: ");
            int my_DroneId = int.Parse(Console.ReadLine());

            Console.Write("Enter Scheduled: ");
            DateTime my_Scheduled = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter PickedUp: ");
            DateTime my_PickedUp = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Delivered: ");
            DateTime my_Delivered = DateTime.Parse(Console.ReadLine());

            parcel = new Parcel()
            {

                Id = my_id,
                SenderId = my_senderId,
                TargetId = my_targetId,
                Weight = my_Weight,
                Priority = my_Priority,
                DroneId = my_DroneId,
                Scheduled = my_Scheduled,
                PickedUp = my_PickedUp,
                Delivered = my_Delivered
            };
        }

        private static void main_add_customer()
        {
            Console.Write("Enter id: ");
            int my_id = int.Parse(Console.ReadLine());
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            Console.Write("Enter phone: ");
            string phone = Console.ReadLine();
            Console.Write("Enter longitude: ");
            double longitude = double.Parse(Console.ReadLine());

            Console.Write("Enter Lattitude: ");

            double lattitude = double.Parse(Console.ReadLine());


            Customer customer = new Customer()
            {

                Id = my_id,
                Name = name,
                Phone = phone,
                Longitude = longitude,
                Lattitude = lattitude
            };

            mydal.Add_customer(customer);
        }

        private static void main_add_drone()
        {
            Console.Write("Enter id: ");
            int my_id = int.Parse(Console.ReadLine());
            Console.Write("Enter model: ");
            string model = Console.ReadLine();
            Console.Write("Enter maxWeight: ");
            WeightCategories maxWeight = (WeightCategories)Enum.Parse(typeof(WeightCategories), Console.ReadLine());
            Console.Write("Enter status: ");
            DroneStatuses status = (DroneStatuses)Enum.Parse(typeof(DroneStatuses), Console.ReadLine());
            Console.Write("Enter battery: ");
            double battery = double.Parse(Console.ReadLine());
            Drone drone = new Drone()
            {

                Id = my_id,
                Model = model,
                MaxWeight = maxWeight,
                Status = status,
                Battery = battery
            };

            mydal.Add_drone(drone);
        }

        private static void main_new_base_station()
        {
            Console.Write("Enter id: ");
            int my_id = int.Parse(Console.ReadLine());
            main_update_baseStation(my_id);
        }

        private static string main_update_baseStation(int my_id)
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            Console.Write("Enter longitude: ");
            double longitude = double.Parse(Console.ReadLine());
            Console.Write("Enter lattitude: ");
            double lattitude = double.Parse(Console.ReadLine());
            Console.Write("Enter slots: ");
            int chargeSlots = int.Parse(Console.ReadLine());
            BaseStation baseStation = new BaseStation()
            {
                Id = my_id,
                ChargeSlots = chargeSlots,
                Lattitude = lattitude,
                Longitude = longitude,
                Name = name
            };
            mydal.Add_base_station(baseStation);
            return name;
        }

        private static void show_menu()
        {
            Console.WriteLine("Choose what to do:");

            // Adding options
            int length = Enum.GetValues(typeof(Options)).Length;
            for (int i = 0; i < length ; i++)
            {
                Console.WriteLine("{0}: : to {1}",i, (Options)i);
            }
            /*
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

            */
        }
    }
}
