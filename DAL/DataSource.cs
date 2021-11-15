using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using System.Collections.Generic;

namespace DalObject
{
    class DataSource
    {
        #region The array and lists of the data
        internal static List<Drone>Drones = new List<Drone>();
        internal static List<BaseStation> BaseStations = new List<BaseStation>();
        internal static List<Customer> Customers = new List<Customer>();
        internal static List<Parcel> Parcels = new List<Parcel>();
        internal static List<DroneCharge> DroneCharges = new List<DroneCharge>();
        //internal static BaseStation[] baseStations = new BaseStation[5];
        //internal static Customer[] customers= new Customer[100];
        //internal static Parcel[] parcels = new Parcel[1000];
        //internal static DroneCharge[] droneCharges = new DroneCharge[50];

        #endregion

        // internal data (the first free index at the arrays, and the run number of the parcels)
        internal class Config
        {
            internal static int runNumOfParcel;
            internal static double Electricity_free;
            internal static double Electricity_light;
            internal static double Electricity_medium;
            internal static double Electricity_heavy;
            internal static double Charge_at_hour;

        }

        //ctor
        internal static void Initialize()
        {
            var rand = new Random();

            #region Adding base stations
            // B_id is between 100-999
            int B_id = rand.Next(0, 900);
            BaseStations.Add(new BaseStation() { ChargeSlots = rand.Next(3, 11), Id = (B_id++) + 100, Name = "Talpiot", Lattitude = 35.207745, Longitude = 31.750313 });
            BaseStations.Add(new BaseStation() { ChargeSlots = rand.Next(3, 11), Id = ((B_id++) % 900) + 100, Name = "Pat", Lattitude = 35.199758, Longitude = 31.750590 } );
            #endregion

            #region Adding drones
            // D_id is between 1000 - 9999
            // Battery is between 5-100
            int batt = rand.Next(5, 101);
            int D_id = rand.Next(0, 9000);
            Drones.Add(new Drone() { Id = (D_id) + 1000, MaxWeight = (WeightCategories)rand.Next(0, 3), Model = "1945"});
            Drones.Add(new Drone() { Id = ((++D_id) % 9000) + 1000, MaxWeight = (WeightCategories)rand.Next(0, 3), Model = "1989"});
            Drones.Add(new Drone() { Id = ((++D_id) % 9000) + 1000, MaxWeight = (WeightCategories)rand.Next(0, 3), Model = "1989"});
            Drones.Add(new Drone() { Id = ((++D_id) % 9000) + 1000, MaxWeight = (WeightCategories)rand.Next(0, 3), Model = "2010"});
            Drones.Add(new Drone() { Id = ((++D_id) % 9000) + 1000, MaxWeight = (WeightCategories)rand.Next(0, 3), Model = "2017"});
            #endregion

            #region Adding customers
            // Id is between 001000000 - 399999999
            // Lattitude is between 35.160443 -  35.252793
            // Longitude is between 31.727247 - 31.844377
            int id = rand.Next(1000000, 398999999);
            Customers.Add(new Customer() { Id = ((id) % 399000000) + 1000000, Name = "Avraham", Phone = "0537829463", Lattitude = rand.Next(35160443, 35252793) * 0.000001, Longitude = rand.Next(31727247, 31844377) * 0.000001 });
            Customers.Add(new Customer() { Id = ((++id) % 399000000) + 1000000, Name = "Yzhak", Phone = "0552961473", Lattitude = rand.Next(35160443, 35252793) * 0.000001, Longitude = rand.Next(31727247, 31844377) * 0.000001 });
            Customers.Add(new Customer() { Id = ((++id) % 399000000) + 1000000, Name = "Ya'akov", Phone = "0529573594", Lattitude = rand.Next(35160443, 35252793) * 0.000001, Longitude = rand.Next(31727247, 31844377) * 0.000001 });
            Customers.Add(new Customer() { Id = ((++id) % 399000000) + 1000000, Name = "Moshe", Phone = "0587192456", Lattitude = rand.Next(35160443, 35252793) * 0.000001, Longitude = rand.Next(31727247, 31844377) * 0.000001 });
            Customers.Add(new Customer() { Id = ((++id) % 399000000) + 1000000, Name = "David", Phone = "0575684710", Lattitude = rand.Next(35160443, 35252793) * 0.000001, Longitude = rand.Next(31727247, 31844377) * 0.000001 });
            Customers.Add(new Customer() { Id = ((++id) % 399000000) + 1000000, Name = "Yosef", Phone = "0504783975", Lattitude = rand.Next(35160443, 35252793) * 0.000001, Longitude = rand.Next(31727247, 31844377) * 0.000001 });
            Customers.Add(new Customer() { Id = ((++id) % 399000000) + 1000000, Name = "Aharon", Phone = "0558395674", Lattitude = rand.Next(35160443, 35252793) * 0.000001, Longitude = rand.Next(31727247, 31844377) * 0.000001 });
            Customers.Add(new Customer() { Id = ((++id) % 399000000) + 1000000, Name = "Reuven", Phone = "0529879564", Lattitude = rand.Next(35160443, 35252793) * 0.000001, Longitude = rand.Next(31727247, 31844377) * 0.000001 });
            Customers.Add(new Customer() { Id = ((++id) % 399000000) + 1000000, Name = "Shimon", Phone = "0589675627", Lattitude = rand.Next(35160443, 35252793) * 0.000001, Longitude = rand.Next(31727247, 31844377) * 0.000001 });
            Customers.Add(new Customer() { Id = ((++id) % 399000000) + 1000000, Name = "Yehuda", Phone = "0509875873", Lattitude = rand.Next(35160443, 35252793) * 0.000001, Longitude = rand.Next(31727247, 31844377) * 0.000001 });
            #endregion

            #region Adding parcels
            // drone Id == 0    means that the parcel didn't connected to a drone
            Config.runNumOfParcel = 10000;
            Parcels.Add(new Parcel() { Id = (Config.runNumOfParcel++) , Weight = (WeightCategories)rand.Next(0, 3), TargetId = ((++id) % 399000000) + 1000000, SenderId = ((++id) % 399000000) + 1000000, Priority = (Priorities)rand.Next(0, 3), Requested = DateTime.Now , DroneId=0 });                       
            Parcels.Add(new Parcel() { Id = (Config.runNumOfParcel++) , Weight = (WeightCategories)rand.Next(0, 3), TargetId = ((++id) % 399000000) + 1000000, SenderId = ((++id) % 399000000) + 1000000, Priority = (Priorities)rand.Next(0, 3), Requested = DateTime.Now , DroneId=0 });                       
            Parcels.Add(new Parcel() { Id = (Config.runNumOfParcel++) , Weight = (WeightCategories)rand.Next(0, 3), TargetId = ((++id) % 399000000) + 1000000, SenderId = ((++id) % 399000000) + 1000000, Priority = (Priorities)rand.Next(0, 3), Requested = DateTime.Now , DroneId=0 });                       
            Parcels.Add(new Parcel() { Id = (Config.runNumOfParcel++) , Weight = (WeightCategories)rand.Next(0, 3), TargetId = ((++id) % 399000000) + 1000000, SenderId = ((++id) % 399000000) + 1000000, Priority = (Priorities)rand.Next(0, 3), Requested = DateTime.Now , DroneId=0 });                       
            Parcels.Add(new Parcel() { Id = (Config.runNumOfParcel++) , Weight = (WeightCategories)rand.Next(0, 3), TargetId = ((++id) % 399000000) + 1000000, SenderId = ((++id) % 399000000) + 1000000, Priority = (Priorities)rand.Next(0, 3), Requested = DateTime.Now , DroneId=0 });                       
            Parcels.Add(new Parcel() { Id = (Config.runNumOfParcel++) , Weight = (WeightCategories)rand.Next(0, 3), TargetId = ((++id) % 399000000) + 1000000, SenderId = ((++id) % 399000000) + 1000000, Priority = (Priorities)rand.Next(0, 3), Requested = DateTime.Now , DroneId=0 });                       
            Parcels.Add(new Parcel() { Id = (Config.runNumOfParcel++) , Weight = (WeightCategories)rand.Next(0, 3), TargetId = ((++id) % 399000000) + 1000000, SenderId = ((++id) % 399000000) + 1000000, Priority = (Priorities)rand.Next(0, 3), Requested = DateTime.Now , DroneId=0 });                       
            Parcels.Add(new Parcel() { Id = (Config.runNumOfParcel++) , Weight = (WeightCategories)rand.Next(0, 3), TargetId = ((++id) % 399000000) + 1000000, SenderId = ((++id) % 399000000) + 1000000, Priority = (Priorities)rand.Next(0, 3), Requested = DateTime.Now , DroneId=0 });                       
            Parcels.Add(new Parcel() { Id = (Config.runNumOfParcel++) , Weight = (WeightCategories)rand.Next(0, 3), TargetId = ((++id) % 399000000) + 1000000, SenderId = ((++id) % 399000000) + 1000000, Priority = (Priorities)rand.Next(0, 3), Requested = DateTime.Now , DroneId=0 });                       
            Parcels.Add(new Parcel() { Id = (Config.runNumOfParcel++) , Weight = (WeightCategories)rand.Next(0, 3), TargetId = ((++id) % 399000000) + 1000000, SenderId = ((++id) % 399000000) + 1000000, Priority = (Priorities)rand.Next(0, 3), Requested = DateTime.Now , DroneId=0 });
            #endregion       
        }

    }
}