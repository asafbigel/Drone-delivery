using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    class DataSource
    {
        internal static Drone[] drones = new Drone[10];
        internal static BaseStation[] baseStations = new BaseStation[5];
        internal static Customer[] customers= new Customer[100];
        internal static Parcel[] parcels = new Parcel[1000];
        internal static DroneCharge[] droneCharges = new DroneCharge[50];

        internal class Config
        {
            internal static int firstDrone =0;
            internal static int firstBaseStation =0 ;
            internal static int firstCustomer =0;
            internal static int firstParcel =0;
            internal static int firstDroneCharge =0;
            internal static int runNumOfParcel;
        }

        internal static void Initialize()
        {
            var rand = new Random();

            // B_id is between 100-999
            int B_id = rand.Next(0, 900);
            baseStations[DataSource.Config.firstBaseStation++] = new BaseStation() { ChargeSlots = rand.Next(3, 11), Id = (B_id++) + 100, Name = "Talpiot", Lattitude = 35.207745, Longitude = 31.750313 };
            baseStations[DataSource.Config.firstBaseStation++] = new BaseStation() { ChargeSlots = rand.Next(3, 11), Id = ((B_id++) % 900) + 100, Name = "Pat", Lattitude = 35.199758, Longitude = 31.750590 };


            // D_id is between 1000 - 9999
            // Battery is between 5-100
            int batt = rand.Next(5, 101);
            int D_id = rand.Next(0, 9000);
            drones[DataSource.Config.firstDrone++] = new Drone() { Battery = batt, Id = (D_id) + 1000, MaxWeight = (WeightCategories)rand.Next(0, 3), Model = "1945", Status = (DroneStatuses)rand.Next(0, 3) };
            drones[DataSource.Config.firstDrone++] = new Drone() { Battery = ((batt + 13) % 95) + 5, Id = ((++D_id) % 9000) + 1000, MaxWeight = (WeightCategories)rand.Next(0, 3), Model = "1989", Status = (DroneStatuses)rand.Next(0, 3) };
            drones[DataSource.Config.firstDrone++] = new Drone() { Battery = ((batt + 13) % 95) + 5, Id = ((++D_id) % 9000) + 1000, MaxWeight = (WeightCategories)rand.Next(0, 3), Model = "1989", Status = (DroneStatuses)rand.Next(0, 3) };
            drones[DataSource.Config.firstDrone++] = new Drone() { Battery = ((batt + 13) % 95) + 5, Id = ((++D_id) % 9000) + 1000, MaxWeight = (WeightCategories)rand.Next(0, 3), Model = "2010", Status = (DroneStatuses)rand.Next(0, 3) };
            drones[DataSource.Config.firstDrone++] = new Drone() { Battery = ((batt + 13) % 95) + 5, Id = ((++D_id) % 9000) + 1000, MaxWeight = (WeightCategories)rand.Next(0, 3), Model = "2017", Status = (DroneStatuses)rand.Next(0, 3) };

            // Id is between 001000000 - 399999999
            // Lattitude is between 35.160443 -  35.252793
            // Longitude is between 31.727247 - 31.844377
            int id = rand.Next(1000000, 398999999);
            customers[DataSource.Config.firstCustomer++] = new Customer() { Id = ((id) % 399000000) + 1000000, Name = "Avraham", Phone = "0537829463", Lattitude = rand.Next(35160443, 35252793) * 0.000001, Longitude = rand.Next(31727247, 31844377) * 0.000001 };
            customers[DataSource.Config.firstCustomer++] = new Customer() { Id = ((++id) % 399000000) + 1000000, Name = "Yzhak", Phone = "0552961473", Lattitude = rand.Next(35160443, 35252793) * 0.000001, Longitude = rand.Next(31727247, 31844377) * 0.000001 };
            customers[DataSource.Config.firstCustomer++] = new Customer() { Id = ((++id) % 399000000) + 1000000, Name = "Ya'akov", Phone = "0529573594", Lattitude = rand.Next(35160443, 35252793) * 0.000001, Longitude = rand.Next(31727247, 31844377) * 0.000001 };
            customers[DataSource.Config.firstCustomer++] = new Customer() { Id = ((++id) % 399000000) + 1000000, Name = "Moshe", Phone = "0587192456", Lattitude = rand.Next(35160443, 35252793) * 0.000001, Longitude = rand.Next(31727247, 31844377) * 0.000001 };
            customers[DataSource.Config.firstCustomer++] = new Customer() { Id = ((++id) % 399000000) + 1000000, Name = "David", Phone = "0575684710", Lattitude = rand.Next(35160443, 35252793) * 0.000001, Longitude = rand.Next(31727247, 31844377) * 0.000001 };
            customers[DataSource.Config.firstCustomer++] = new Customer() { Id = ((++id) % 399000000) + 1000000, Name = "Yosef", Phone = "0504783975", Lattitude = rand.Next(35160443, 35252793) * 0.000001, Longitude = rand.Next(31727247, 31844377) * 0.000001 };
            customers[DataSource.Config.firstCustomer++] = new Customer() { Id = ((++id) % 399000000) + 1000000, Name = "Aharon", Phone = "0558395674", Lattitude = rand.Next(35160443, 35252793) * 0.000001, Longitude = rand.Next(31727247, 31844377) * 0.000001 };
            customers[DataSource.Config.firstCustomer++] = new Customer() { Id = ((++id) % 399000000) + 1000000, Name = "Reuven", Phone = "0529879564", Lattitude = rand.Next(35160443, 35252793) * 0.000001, Longitude = rand.Next(31727247, 31844377) * 0.000001 };
            customers[DataSource.Config.firstCustomer++] = new Customer() { Id = ((++id) % 399000000) + 1000000, Name = "Shimon", Phone = "0589675627", Lattitude = rand.Next(35160443, 35252793) * 0.000001, Longitude = rand.Next(31727247, 31844377) * 0.000001 };
            customers[DataSource.Config.firstCustomer++] = new Customer() { Id = ((++id) % 399000000) + 1000000, Name = "Yehuda", Phone = "0509875873", Lattitude = rand.Next(35160443, 35252793) * 0.000001, Longitude = rand.Next(31727247, 31844377) * 0.000001 };



            // drone Id == 0    means that the parcel didn't connected to a drone
            int P_id = 10000;
            parcels[DataSource.Config.firstParcel++] = new Parcel() { Id = (P_id++) , Weight = (WeightCategories)rand.Next(0, 3), TargetId = ((++id) % 399000000) + 1000000, SenderId = ((++id) % 399000000) + 1000000, Priority = (Priorities)rand.Next(0, 3), Requested = DateTime.Now , DroneId=0 };                       
            parcels[DataSource.Config.firstParcel++] = new Parcel() { Id = (P_id++) , Weight = (WeightCategories)rand.Next(0, 3), TargetId = ((++id) % 399000000) + 1000000, SenderId = ((++id) % 399000000) + 1000000, Priority = (Priorities)rand.Next(0, 3), Requested = DateTime.Now , DroneId=0 };                       
            parcels[DataSource.Config.firstParcel++] = new Parcel() { Id = (P_id++) , Weight = (WeightCategories)rand.Next(0, 3), TargetId = ((++id) % 399000000) + 1000000, SenderId = ((++id) % 399000000) + 1000000, Priority = (Priorities)rand.Next(0, 3), Requested = DateTime.Now , DroneId=0 };                       
            parcels[DataSource.Config.firstParcel++] = new Parcel() { Id = (P_id++) , Weight = (WeightCategories)rand.Next(0, 3), TargetId = ((++id) % 399000000) + 1000000, SenderId = ((++id) % 399000000) + 1000000, Priority = (Priorities)rand.Next(0, 3), Requested = DateTime.Now , DroneId=0 };                       
            parcels[DataSource.Config.firstParcel++] = new Parcel() { Id = (P_id++) , Weight = (WeightCategories)rand.Next(0, 3), TargetId = ((++id) % 399000000) + 1000000, SenderId = ((++id) % 399000000) + 1000000, Priority = (Priorities)rand.Next(0, 3), Requested = DateTime.Now , DroneId=0 };                       
            parcels[DataSource.Config.firstParcel++] = new Parcel() { Id = (P_id++) , Weight = (WeightCategories)rand.Next(0, 3), TargetId = ((++id) % 399000000) + 1000000, SenderId = ((++id) % 399000000) + 1000000, Priority = (Priorities)rand.Next(0, 3), Requested = DateTime.Now , DroneId=0 };                       
            parcels[DataSource.Config.firstParcel++] = new Parcel() { Id = (P_id++) , Weight = (WeightCategories)rand.Next(0, 3), TargetId = ((++id) % 399000000) + 1000000, SenderId = ((++id) % 399000000) + 1000000, Priority = (Priorities)rand.Next(0, 3), Requested = DateTime.Now , DroneId=0 };                       
            parcels[DataSource.Config.firstParcel++] = new Parcel() { Id = (P_id++) , Weight = (WeightCategories)rand.Next(0, 3), TargetId = ((++id) % 399000000) + 1000000, SenderId = ((++id) % 399000000) + 1000000, Priority = (Priorities)rand.Next(0, 3), Requested = DateTime.Now , DroneId=0 };                       
            parcels[DataSource.Config.firstParcel++] = new Parcel() { Id = (P_id++) , Weight = (WeightCategories)rand.Next(0, 3), TargetId = ((++id) % 399000000) + 1000000, SenderId = ((++id) % 399000000) + 1000000, Priority = (Priorities)rand.Next(0, 3), Requested = DateTime.Now , DroneId=0 };                       
            parcels[DataSource.Config.firstParcel++] = new Parcel() { Id = (P_id++) , Weight = (WeightCategories)rand.Next(0, 3), TargetId = ((++id) % 399000000) + 1000000, SenderId = ((++id) % 399000000) + 1000000, Priority = (Priorities)rand.Next(0, 3), Requested = DateTime.Now , DroneId=0 };

            Config.runNumOfParcel = 11 ;
            
        }

    }
}