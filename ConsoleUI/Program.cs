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
       print_all_base_stations_with_free_charge_slot,
       show_options
    }

    
    class Program
    {
        static DalObject.DalObject mydal = new  DalObject.DalObject();
        static void Main(string[] args)
        {

            show_menu();

            Options option = (Options)Enum.Parse(typeof(Options), Console.ReadLine());

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
                        main_take_parcel_by_drone();
                        break;

                    case Options.delivery_parcel_to_customer:
                        main_delivery_parcel_to_customer();

                        break;

                    case Options.send_drone_to_charge:
                        main_send_drone_to_charge();
                        break;

                    case Options.put_out_drone_from_charge:
                        main_put_out_drone_from_charge();
                        break;

                    case Options.print_a_base_station:
                        main_print_a_base_station();
                        break;

                    case Options.print_a_drone:
                        main_print_a_drone();
                        break;

                    case Options.print_a_customer:
                        main_print_a_customer();
                        break;

                    case Options.print_a_parcel:
                        main_print_a_parcel();
                        break;

                    case Options.print_all_base_stations:
                        main_print_all_base_stations();
                        break;

                    case Options.print_all_drones:
                        main_print_all_drones();
                        break;

                    case Options.print_all_customers:
                        main_print_all_customers();
                        break;

                    case Options.print_all_parcels:
                        main_print_all_parcels();
                        break;

                    case Options.print_all_parcels_that_have_not_yet_been_connect_to_drone:
                        main_print_all_parcels_that_have_not_yet_been_connect_to_drone();
                        break;

                    case Options.print_all_base_stations_with_free_charge_slot:
                        main_print_all_base_stations_with_free_charge_slot();
                        break;

                    case Options.show_options:
                        show_menu();
                        break;


                    default:
                        Console.WriteLine("Wrong input. Try again");
                        break;
                }

                Console.WriteLine("Choose what to do:");
                option = (Options)Enum.Parse(typeof(Options), Console.ReadLine());
            }
        }


        #region Printing function
        private static void main_print_all_parcels_that_have_not_yet_been_connect_to_drone()
        {
            Console.WriteLine("parcels that have not yet been connect to drone:");
            Parcel[] all_parcels = mydal.Get_all_parcels();
            for (int i = 0; i < mydal.GetFirstFreeParcel(); i++)
            {
                if (all_parcels[i].DroneId == 0)
                    Console.Write(all_parcels[i]);
            }
        }
       
        private static void main_print_all_base_stations_with_free_charge_slot()
        {
            Console.WriteLine("base stations with free charge slot");
            BaseStation[] all_baseStations = mydal.Get_all_base_stations();
            for (int i = 0; i < mydal.GetFirstFreeBaseStation(); i++)
            {
                if (all_baseStations[i].ChargeSlots > 0)
                    Console.Write(all_baseStations[i]);
            }
        }

        private static void main_print_all_parcels()
        {
            Console.WriteLine("parcels:");
            Parcel[] all_parcels = mydal.Get_all_parcels();

            for (int i = 0; i < mydal.GetFirstFreeParcel(); i++)
            {
                Console.Write(all_parcels[i]);
            }
        }

        private static void main_print_all_customers()
        {
            Console.WriteLine("customers:");
            Customer[] all_customers = mydal.Get_all_customers();
            for (int i = 0; i < mydal.GetFirstCustomer(); i++)
            {
                Console.Write(all_customers[i]);
            }
        }

        private static void main_print_all_drones()
        {
            Console.WriteLine("drones:");
            Drone[] all_drones = mydal.Get_all_drones();

            for (int i = 0; i < mydal.GetFirstDrone(); i++)
            {
                Console.Write(all_drones[i]);
            }
        }

        private static void main_print_all_base_stations()
        {
            Console.WriteLine("base stations:");
            BaseStation[] all_baseStations = mydal.Get_all_base_stations();
            for (int i = 0; i < mydal.GetFirstFreeBaseStation(); i++)
            {
                Console.Write(all_baseStations[i]);
            }
        }

        private static void main_print_a_parcel()
        {
            int my_id;
            Console.Write("Enter id: ");
            my_id = int.Parse(Console.ReadLine());
            Parcel parcel1 = mydal.Find_parcel(my_id);
            Console.Write(parcel1);
        }

        private static void main_print_a_customer()
        {
            int my_id;
            Console.Write("Enter id: ");
            my_id = int.Parse(Console.ReadLine());
            Customer customer1 = mydal.Find_customer(my_id);
            Console.Write(customer1);
        }

        private static void main_print_a_drone()
        {
            int my_id;
            Console.Write("Enter id: ");
            my_id = int.Parse(Console.ReadLine());
            Drone drone1 = mydal.Find_drone(my_id);
            Console.Write(drone1);
        }

        private static void main_print_a_base_station()
        {
            int my_id;
            Console.Write("Enter id: ");
            my_id = int.Parse(Console.ReadLine());
            BaseStation baseStation1 = mydal.Find_baseStation(my_id);
            Console.Write(baseStation1);
        }
        #endregion

        #region Charging options
        private static void main_put_out_drone_from_charge()
        {
            Console.Write("Enter drone ID: ");
            int my_drone_id = int.Parse(Console.ReadLine());
            DroneCharge droneCharge = mydal.Find_drone_charge(my_drone_id);
            int my_baseStation_id = droneCharge.StationId;
            BaseStation baseStation = mydal.Find_baseStation(my_baseStation_id);
            baseStation.ChargeSlots++;
            int previous_DroneId = droneCharge.DroneId;
            droneCharge.DroneId = 0;
            droneCharge.StationId = 0;
            Drone drone = mydal.Find_drone(my_drone_id);
            // change status of the drone to "vacant"
            drone.Status = (DroneStatuses)0;
            mydal.UpdateBaseStation(baseStation);
            mydal.UpdateDrone(drone);
            mydal.UpdateDroneCharge(droneCharge, previous_DroneId);
        }

        private static void main_send_drone_to_charge()
        {
            Console.Write("Enter drone ID: ");
            int my_drone_id = int.Parse(Console.ReadLine());
            Console.WriteLine("Choose base station from the list:");
            main_print_all_base_stations_with_free_charge_slot();
            Console.WriteLine("Enter base station ID: ");
            int my_baseStation_id = int.Parse(Console.ReadLine());

            DroneCharge droneCharge = new DroneCharge()
            {
                DroneId = my_drone_id,
                StationId = my_baseStation_id
            };
            BaseStation baseStation = mydal.Find_baseStation(my_baseStation_id);
            Drone drone = mydal.Find_drone(my_drone_id);
            baseStation.ChargeSlots--;
            // change status of the drone to "maintenance"
            drone.Status = (DroneStatuses)1;
            mydal.Add_DroneCharge(droneCharge);
            mydal.UpdateBaseStation(baseStation);
            mydal.UpdateDrone(drone);
        }
        #endregion

        #region Changes at the data (parcels and drones)
        private static void main_connect_parcel_to_drone()
        {
            int my_id = Input_parcel_id();
            Console.WriteLine("Enter drone Id to connect:");
            int drone_id = int.Parse(Console.ReadLine());
            Parcel parcel = mydal.Find_parcel(my_id);
            parcel.DroneId = drone_id;
            mydal.UpdateParcel(parcel);
        }

        private static void main_take_parcel_by_drone()
        {
            int my_id = Input_parcel_id();
            Parcel parcel = mydal.Find_parcel(my_id);
            parcel.PickedUp = DateTime.Now;
            mydal.UpdateParcel(parcel);
        }

        private static void main_delivery_parcel_to_customer()
        {
            int my_id = Input_parcel_id();
            Parcel parcel = mydal.Find_parcel(my_id);
            parcel.Delivered = DateTime.Now;
            mydal.UpdateParcel(parcel);
        }
        #endregion

        #region Adding objects to the data
        private static void main_add_parcel()
        {
            int my_id = mydal.GetAndUpdateRunNumber();

            Console.Write("Enter sender Id: ");
            int my_senderId = int.Parse(Console.ReadLine());

            Console.Write("Enter target Id: ");
            int my_targetId = int.Parse(Console.ReadLine());

            Console.Write("Enter Weight: ");
            WeightCategories my_Weight = (WeightCategories)Enum.Parse(typeof(WeightCategories), Console.ReadLine());

            Console.Write("Enter Priority: ");
            Priorities my_Priority = (Priorities)Enum.Parse(typeof(Priorities), Console.ReadLine());

            // int my_DroneId = int.Parse(Console.ReadLine());
            int my_DroneId = 0;

            Console.Write("Enter Scheduled: ");
            DateTime my_Scheduled = DateTime.Parse(Console.ReadLine());

           // Console.Write("Enter PickedUp: ");
           // DateTime my_PickedUp = DateTime.Parse(Console.ReadLine());
            DateTime my_PickedUp = default(DateTime);

            // Console.Write("Enter Delivered: ");
            // DateTime my_Delivered = DateTime.Parse(Console.ReadLine());
            DateTime my_Delivered = default(DateTime);

            DateTime my_Requested = DateTime.Now;

            Parcel parcel = new Parcel()
            {
                Id = my_id,
                SenderId = my_senderId,
                TargetId = my_targetId,
                Weight = my_Weight,
                Priority = my_Priority,
                DroneId = my_DroneId,
                Scheduled = my_Scheduled,
                PickedUp = my_PickedUp,
                Delivered = my_Delivered,
                Requested = my_Requested
            };
            mydal.Add_parcel(parcel);
            Console.WriteLine("Parcel ID is: " + my_id);
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
        }
        #endregion


        // show menu at the main
        private static void show_menu()
        {
            Console.WriteLine("Choose what to do:");

            // Adding options
            int length = Enum.GetValues(typeof(Options)).Length;
            for (int i = 0; i < length ; i++)
            {
                Console.WriteLine($"{i}: to {(Options)i}");
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

        // Help function, ask id num frim the user, and return it
        private static int Input_parcel_id()
        {
            Console.WriteLine("Enter parcel Id to connect:");
            return int.Parse(Console.ReadLine());
        }

    }
}
