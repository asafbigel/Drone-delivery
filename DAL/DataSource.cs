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
            BaseStations.Add(new BaseStation()
            {
                ChargeSlots = rand.Next(3, 11),
                Id = (B_id++) + 100,
                Name = "Talpiot",
                Lattitude = 35.207745,
                Longitude = 31.750313
            });
            BaseStations.Add(new BaseStation()
            {
                ChargeSlots = rand.Next(3, 11),
                Id = ((B_id++) % 900) + 100,
                Name = "Pat",
                Lattitude = 35.199758,
                Longitude = 31.750590
            });
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

            #region temp items
            BaseStations.Add(new BaseStation()
            {
                ChargeSlots = 5,
                Id = 1,
                Name = "A",
                Lattitude = 35,
                Longitude = 31
            });
            Customers.Add(new Customer()
            {
                Id = 1,
                Name = "Temp1",
                Phone = "0537829463",
                Lattitude = 35.001,
                Longitude = 31.001
            });
            Customers.Add(new Customer()
            {
                Id = 2,
                Name = "Temp2",
                Phone = "0537829463",
                Lattitude = 35.011,
                Longitude = 31.011
            });
            Customers.Add(new Customer()
            {
                Id = 3,
                Name = "Temp3",
                Phone = "0537829463",
                Lattitude = 35.101,
                Longitude = 31.101
            });
            Parcels.Add(new Parcel()
            {
                Id = 1,
                Weight = WeightCategories.medium,
                TargetId = 1,
                SenderId = 2,
                Priority = Priorities.fast,
                Requested = new DateTime(2020, 10, 1, 12, 0, 0),
                Scheduled = new DateTime(2020, 10, 1, 12, 5, 0),
                PickedUp = new DateTime(2020, 10, 1, 12, 10, 0),
                Delivered = new DateTime(2020, 10, 1, 12, 15, 0),
                DroneId = 1
            });
            Parcels.Add(new Parcel()
            {
                Id = 2,
                Weight = WeightCategories.medium,
                TargetId = 2,
                SenderId = 3,
                Priority = Priorities.fast,
                Requested = new DateTime(2020, 10, 1, 13, 0, 0),
                Scheduled = new DateTime(2020, 10, 1, 13, 5, 0),
                PickedUp = new DateTime(2020, 10, 1, 13, 10, 0),
                DroneId = 1
            });
            Parcels.Add(new Parcel()
            {
                Id = 3,
                Weight = WeightCategories.medium,
                TargetId = 2,
                SenderId = 3,
                Priority = Priorities.fast,
                Requested = new DateTime(2020, 10, 1, 14, 0, 0),
                Scheduled = new DateTime(2020, 10, 1, 14, 5, 0),
                PickedUp = new DateTime(2020, 10, 1, 14, 10, 0),
                Delivered = new DateTime(2020, 10, 1, 14, 15, 0),
                DroneId = 2
            });
            Parcels.Add(new Parcel()
            {
                Id = 4,
                Weight = WeightCategories.medium,
                TargetId = 2,
                SenderId = 1,
                Priority = Priorities.fast,
                Requested = new DateTime(2020, 10, 1, 15, 0, 0),
                Scheduled = new DateTime(2020, 10, 1, 15, 5, 0),
                DroneId = 2
            });
            Parcels.Add(new Parcel()
            {
                Id = 5,
                Weight = WeightCategories.medium,
                TargetId = 2,
                SenderId = 1,
                Priority = Priorities.fast,
                Requested = new DateTime(2020, 10, 1, 16, 0, 0),
                Scheduled = new DateTime(2020, 10, 1, 16, 5, 0),
                PickedUp = new DateTime(2020, 10, 1, 16, 15, 0),
                Delivered = new DateTime(2020, 10, 1, 16, 25, 0),
                DroneId = 3
            });
            Parcels.Add(new Parcel()
            {
                Id = 6,
                Weight = WeightCategories.medium,
                TargetId = 1,
                SenderId = 3,
                Priority = Priorities.fast,
                Requested = new DateTime(2020, 10, 1, 17, 0, 0),
                Scheduled = new DateTime(2020, 10, 1, 17, 5, 0),
                PickedUp = new DateTime(2020, 10, 1, 17, 15, 0),
                Delivered = new DateTime(2020, 10, 1, 17, 25, 0),
                DroneId = 3
            });
            Parcels.Add(new Parcel()
            {
                Id = 7,
                Weight = WeightCategories.medium,
                TargetId = 3,
                SenderId = 2,
                Priority = Priorities.fast,
                Requested = new DateTime(2020, 10, 1, 18, 0, 0),
                Scheduled = new DateTime(2020, 10, 1, 18, 5, 0),
                PickedUp = new DateTime(2020, 10, 1, 18, 15, 0),
                Delivered = new DateTime(2020, 10, 1, 18, 25, 0),
                DroneId = 3
            });
            Parcels.Add(new Parcel()
            {
                Id = 8,
                Weight = WeightCategories.medium,
                TargetId = 3,
                SenderId = 2,
                Priority = Priorities.fast,
                Requested = new DateTime(2020, 10, 1, 18, 0, 0)
            });
            Drones.Add(new Drone()
            {
                Id = 1,
                MaxWeight = WeightCategories.heavy,
                Model = "A"
            });
            Drones.Add(new Drone()
            {
                Id = 2,
                MaxWeight = WeightCategories.medium,
                Model = "B"
            }); 
            Drones.Add(new Drone()
            {
                Id = 3,
                MaxWeight = WeightCategories.heavy,
                Model = "C"
            });
            #endregion

            #region Electricity set
            Config.Electricity_free = 50;
            Config.Electricity_light = 60;
            Config.Electricity_medium = 70;
            Config.Electricity_heavy = 80;
            Config.Charge_at_hour = 30;

            #endregion
        }

    }
}