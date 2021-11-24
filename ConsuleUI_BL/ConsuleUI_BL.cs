using System;
using IBL.BO;
using System.Collections.Generic;
namespace ConsuleUI_BL

{
    #region enums
    public enum Options
    {
        exit, Adding_options, Update_options, Showing_options, Show_list_options, options
    }
    public enum Adding_Options
    {
        main_menu ,add_baseStation, add_drone, add_customer, add_parcel, options
    }
    public enum Update_options
    {
        main_menu,
        update_model_drone, update_baseStation, update_customer,
        drone_to_charge, drone_from_charge,
        connect_parcel_to_drone, pickedUp_parcel_by_drone,
        delivered_parcel_by_drone
    }
    public enum Showing_options
    {
        main_menu, print_baseStation, print_drone, print_customer, print_parcel
    }
    public enum Show_list_options
    {
        main_menu,
        print_all_baseStations, print_all_drones, print_all_customers,
        print_all_parcels, print_all_parcels_without_drone,
        print_all_baseStations_with_free_slots
    }
    #endregion

    public partial class ConsuleUI_BL
    {
        // need change to the interface MISHNANE
        static IBL.BL mybi = new IBL.BL();
        static void Main(string[] args)
        {
            show_menu();
            Options option = (Options)Enum.Parse(typeof(Options), Console.ReadLine());
            while (option != Options.exit)
            {
                switch (option)
                {
                    case Options.Adding_options:
                        main_Adding_options();
                        break;
                    case Options.Update_options:
                        main_Update_options();
                        break;
                    case Options.Showing_options:
                        main_Showing_options();
                        break;
                    case Options.Show_list_options:
                        main_Show_list_options();
                        break;
                    case Options.options:
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



        #region show menu at the main
        private static void show_menu()
        {
            Console.WriteLine("Choose what to do:");
            int length = Enum.GetValues(typeof(Options)).Length;
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine($"{i}: to {(Options)i}");
            }

        }
        private static void show_adding_options()
        {
            Console.WriteLine("Choose what to do:");
            int length = Enum.GetValues(typeof(Adding_Options)).Length;
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine($"{i}: to {(Adding_Options)i}");
            }

        }
        private static void show_update_options()
        {
            Console.WriteLine("Choose what to do:");
            int length = Enum.GetValues(typeof(Update_options)).Length;
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine($"{i}: to {(Update_options)i}");
            }

        }
        private static void show_showing_options()
        {
            Console.WriteLine("Choose what to do:");
            int length = Enum.GetValues(typeof(Showing_options)).Length;
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine($"{i}: to {(Showing_options)i}");
            }

        }
        private static void show_show_list_options()
        {
            Console.WriteLine("Choose what to do:");
            // Adding options
            int length = Enum.GetValues(typeof(Show_list_options)).Length;
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine($"{i}: to {(Show_list_options)i}");
            }

        }
        #endregion
        #region Active functions
        private static void main_Adding_options()
        {
            show_adding_options();
            Adding_Options option = (Adding_Options)Enum.Parse(typeof(Adding_Options), Console.ReadLine());
            if (option != Adding_Options.main_menu)
            {
                switch (option)
                {
                    case Adding_Options.add_baseStation:
                        add_baseStation();
                        break;
                    case Adding_Options.add_drone:
                        Add_drone();
                        break;
                    case Adding_Options.add_customer:
                        Add_customer();
                        break;
                    case Adding_Options.add_parcel:
                        Add_parcel();
                        break;

                    default:
                        Console.WriteLine("Wrong input. Try again");
                        break;
                }
            }
        }



        private static void main_Update_options()
        {
            show_update_options();
            Update_options option = (Update_options)Enum.Parse(typeof(Update_options), Console.ReadLine());
            if (option != Update_options.main_menu)
            {
                switch (option)
                {
                    case Update_options.update_model_drone:
                        break;
                    case Update_options.update_baseStation:
                        break;
                    case Update_options.update_customer:
                        break;
                    case Update_options.drone_to_charge:
                        break;
                    case Update_options.drone_from_charge:
                        break;
                    case Update_options.connect_parcel_to_drone:
                        break;
                    case Update_options.pickedUp_parcel_by_drone:
                        break;
                    case Update_options.delivered_parcel_by_drone:
                        break;
                    default:
                        Console.WriteLine("Wrong input. Try again");
                        break;
                }
            }
        }
        private static void main_Showing_options()
        {
            show_showing_options();
            Showing_options option = (Showing_options)Enum.Parse(typeof(Showing_options), Console.ReadLine());
            if (option != Showing_options.main_menu)
            {
                switch (option)
                {
                    case Showing_options.print_baseStation:
                        break;
                    case Showing_options.print_drone:
                        break;
                    case Showing_options.print_customer:
                        break;
                    case Showing_options.print_parcel:
                        break;

                    default:
                        Console.WriteLine("Wrong input. Try again");
                        break;
                }
            }
        }
        private static void main_Show_list_options()
        {
            show_show_list_options();
            Show_list_options option = (Show_list_options)Enum.Parse(typeof(Show_list_options), Console.ReadLine());
            if (option != Show_list_options.main_menu)
            {
                switch (option)
                {
                    case Show_list_options.print_all_baseStations:
                        break;
                    case Show_list_options.print_all_drones:
                        break;
                    case Show_list_options.print_all_customers:
                        break;
                    case Show_list_options.print_all_parcels:
                        break;
                    case Show_list_options.print_all_parcels_without_drone:
                        break;
                    case Show_list_options.print_all_baseStations_with_free_slots:
                        break;
                   
                    default:
                        Console.WriteLine("Wrong input. Try again");
                        break;
                }
            }
        }
        #endregion
    }
}
