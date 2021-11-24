using System;
using IBL.BO;
namespace ConsuleUI_BL
{
    public partial class ConsuleUI_BL
    {
        // to do
        private static void delivered_parcel_by_drone()
        {
            throw new NotImplementedException();
        }
        // to do
        private static void pickedUp_parcel_by_drone()
        {
            throw new NotImplementedException();
        }
        // to do
        private static void connect_parcel_to_drone()
        {
            throw new NotImplementedException();
        }
        // to do
        private static void drone_from_charge()
        {
            throw new NotImplementedException();
        }
        // to do
        private static void drone_to_charge()
        {
            throw new NotImplementedException();
        }
        // to do
        private static void update_customer()
        {
            throw new NotImplementedException();
        }
        // to do
        private static void update_baseStation()
        {
            Console.Write("Enter base station's id");
            if(!int.TryParse(Console.ReadLine(),out int id)) { throw new IntReadException(); }
            Console.Write("Enter new name ('_' to don't change");
            string new_name = Console.ReadLine();
            Console.Write("Enter new free slots ('_' to don't change");
            string new_slot = Console.ReadLine();
            mybi.update_baseStation(id, new_name, new_slot);
        }

        private static void update_model_drone()
        {
            Console.Write("Enter drone's id: ");
            int drone_id = int.Parse(Console.ReadLine());
            Console.Write("Enter drone's model: ");
            string model = Console.ReadLine();
            mybi.update_model_drone(drone_id, model);
        }

    }
}
