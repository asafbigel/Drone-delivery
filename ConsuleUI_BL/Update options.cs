using System;
using IBL.BO;
namespace ConsuleUI_BL
{
    public partial class ConsuleUI_BL
    {
        /// <summary>
        /// recive input to delivered parcel by drone and call to the fancion
        /// </summary>
        private static void delivered_parcel_by_drone()
        {
            Console.Write("Enter drone's id: ");
            if (!int.TryParse(Console.ReadLine(), out int drone_id)) { throw new InputException("Id not valid"); }
            mybi.delivered_parcel_by_drone(drone_id);
        }
        /// <summary>
        /// recive input to pickedUp parcel by drone and call to the fancion
        /// </summary>
        private static void pickedUp_parcel_by_drone()
        {
            Console.Write("Enter drone's id: ");
            if (!int.TryParse(Console.ReadLine(), out int drone_id)) { throw new InputException("Id not valid"); }
            mybi.pickedUp_parcel_by_drone(drone_id);
        }
        /// <summary>
        /// recive input to connect parcel to drone and call to the fancion
        /// </summary>
        private static void connect_parcel_to_drone()
        {
            Console.Write("Enter drone's id: ");
            if(!int.TryParse(Console.ReadLine(), out int drone_id)) { throw new InputException("Id not valid"); }
            mybi.connect_parcel_to_drone(drone_id);
        }
        /// <summary>
        /// recive input to drone from charge and call to the fancion
        /// </summary>
        private static void drone_from_charge()
        {
            Console.Write("Enter drone's id: ");
            if (!int.TryParse(Console.ReadLine(), out int drone_id)) { throw new InputException("Id not valid"); }
            Console.Write("Enter time of charging (at hours): ");
            if (!double.TryParse(Console.ReadLine(), out double time)) { throw new InputException("Time not valid"); }
            mybi.drone_from_charge(drone_id, time);
        }
        /// <summary>
        /// recive input to send drone to charge and call to the fancion
        /// </summary>
        private static void send_drone_to_charge()
        {
            Console.Write("Enter drone's id: ");
            if(!int.TryParse(Console.ReadLine(), out int id)) { throw new InputException("Id not valid"); }
            mybi.send_drone_to_charge(id);
        }
        /// <summary>
        /// recive input to update customer and call to the fancion
        /// </summary>
        private static void update_customer()
        {
            Console.Write("Enter customer id: ");
            if(!int.TryParse(Console.ReadLine(), out int id)) { throw new InputException("The id not valid"); }
            Console.Write("Enter new name ('_' to don't change): ");
            string new_name = Console.ReadLine();
            Console.Write("Enter new phone ('_' to don't change): ");
            string new_phone = Console.ReadLine();
            if (new_phone != "_")
                // only check if the id is number. Although phone is string
                if (!int.TryParse(Console.ReadLine(), out int phone)) { throw new InputException("Phone not valid"); }
            mybi.update_customer(id, new_name, new_phone);
        }
        /// <summary>
        /// recive input to update baseStation and call to the fancion
        /// </summary>
        private static void update_baseStation()
        {
            Console.Write("Enter base station's id");
            if(!int.TryParse(Console.ReadLine(),out int id)) { throw new IntReadException(); }
            Console.Write("Enter new name ('_' to don't change): ");
            string new_name = Console.ReadLine();
            Console.Write("Enter new free slots ('_' to don't change): ");
            string new_slot = Console.ReadLine();
            mybi.update_baseStation(id, new_name, new_slot);
        }
        /// <summary>
        /// recive input to update model drone and call to the fancion
        /// </summary>
        private static void update_model_drone()
        {
            Console.Write("Enter drone's id: ");
            if(!int.TryParse(Console.ReadLine(), out int drone_id)) { throw new InputException("Id not valid"); }
            Console.Write("Enter drone's model: ");
            string model = Console.ReadLine();
            mybi.update_model_drone(drone_id, model);
        }
    }
}
