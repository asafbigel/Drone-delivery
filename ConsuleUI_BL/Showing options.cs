using System;
using BO;
namespace ConsuleUI_BL
{
    public partial class ConsuleUI_BL
    {
        /// <summary>
        /// print parcel
        /// </summary>
        private static void print_parcel()
        {
            Console.Write("Enter Id number: ");
            if (!int.TryParse(Console.ReadLine(), out int parcel_id)) { throw new InputException("Id not valid"); }
            Console.WriteLine(mybi.StringParcel(parcel_id) );
        }
        /// <summary>
        /// print customer
        /// </summary>
        private static void print_customer()
        {
            Console.Write("Enter Id number: ");
            if (!int.TryParse(Console.ReadLine(), out int customer_id)) { throw new InputException("Id not valid"); }
            Console.WriteLine(mybi.StringCustomer(customer_id) ); 
        }
        /// <summary>
        /// print drone
        /// </summary>
        private static void print_drone()
        {
            Console.Write("Enter Id number: ");
            if (!int.TryParse(Console.ReadLine(), out int drone_id)) { throw new InputException("Id not valid"); }
            Console.WriteLine(mybi.StringDrone(drone_id));
        }
        /// <summary>
        /// print baseStation
        /// </summary>
        private static void print_baseStation()
        {
            Console.Write("Enter Id number: ");
            if (!int.TryParse(Console.ReadLine(), out int baseStation_id)) { throw new InputException("Id not valid"); }
            Console.WriteLine(mybi.string_baseStation(baseStation_id)); 
        }
    }
}
