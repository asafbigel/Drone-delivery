using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;


namespace BL
{
    public partial class BL
    {
        /// <summary>
        /// fanction that update baseStation
        /// </summary>
        /// <param name="id">id of  baseStation </param>
        /// <param name="new_name"> the new name of the update baseStation   </param>
        /// <param name="new_slot">the new number of free slot charge of  the update baseStation</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateBaseStation(int id, string new_name, string new_slot)
        {
            BaseStation baseStation = FindBaseStation(id);
            if (new_name != "_")
                baseStation.Name = new_name;
            if (new_slot != "_")
            {
                if (!int.TryParse(new_slot, out int slot)) { throw new IntReadException("Wrong input"); }
                if (slot >= baseStation.NumFreeSlotsCharge)
                    baseStation.NumFreeSlotsCharge = slot;
                else
                    throw new slotException("There more drone at charge now");
            }
            lock (mydal)
                mydal.UpdateBaseStation(convertor(baseStation));
        }
        /// <summary>
        ///  fanction that update baseStation
        /// </summary>
        /// <param name="baseStation"> the baseStation to update </param>
        public void UpdateBaseStation(BaseStation baseStation)
        { 
            if(baseStation.NumFreeSlotsCharge < 0)
                throw new slotException("Free slots charge can't be less than 0");
            BaseStation OldbaseStation = FindBaseStation(baseStation.Id);
            mydal.UpdateBaseStation(convertor(baseStation));
        }

        /// <summary>
        /// get BL base station
        /// </summary>
        /// <param name="id"> id of the base station</param>
        /// <returns>
        /// BL base station
        /// </returns>
        private BaseStation FindBaseStation(int id)
        {
            lock (mydal)
            {
                DO.BaseStation idalBaseStation = mydal.Find_baseStation(id);
                BaseStation baseStation = convertor(idalBaseStation);
                return baseStation;
            }
        }
        /// <summary>
        /// Add base station to the list
        /// </summary>
        /// <param name="baseStation">
        /// The base station
        /// </param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddBaseStation(BaseStation baseStation)
        {
            if (baseStation.Id <= 0)
                throw new BaseStationIdExeption("Invalid aseStation Id");
            if (baseStation.Name == "")
                throw new BaseStationExeption("Enter Name");
            if (baseStation.Name == null)
                throw new BaseStationExeption("Invalid Name");
            lock (mydal)
            {
                baseStation.DroneInChargings = new List<DroneInCharging>();
                DO.BaseStation idalBaseStation = convertor(baseStation);
                mydal.Add_base_station(idalBaseStation);
            }
        }
        /// <summary>
        /// return string of details of this base station
        /// </summary>
        /// <param name="baseStation_id">
        /// Id of the base station  
        /// </param>
        /// <returns>
        /// string of details of this base station
        /// </returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string StringBaseStation(int baseStation_id)
        {
            return convertor(mydal.Find_baseStation(baseStation_id)).ToString();
        }
        /// <summary>
        /// ToString of all the baseStations
        /// </summary>
        /// <returns> the string of ToString of all the baseStations </returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string StringAllBaseStations()
        {
            lock (mydal)
            {
                List<BaseStationToList> baseStations = convertor1(mydal.Get_all_base_stations(x => true));
                string result = "";
                foreach (var item in baseStations)
                {
                    result += item.ToString();
                    result += "\n";
                }
                return result;
            }
        }
        /// <summary>
        /// ToString of all the baseStations with free slots
        /// </summary>
        /// <returns>the string of ToString of all the baseStations with free slots</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string StringAllBaseStationsWithFreeSlots()
        {
            lock (mydal)
            {
                List<BaseStationToList> baseStations = convertor1(mydal.Get_all_base_stations(x => x.ChargeSlots > 0));
                string result = "";
                foreach (var item in baseStations)
                {
                    result += item.ToString();
                    result += "\n";
                }
                return result;
            }
        }
        public BaseStation GetBaseStation(int id)
        {
            var baseStation = mydal.Find_baseStation(id);
            if (baseStation.Id == 0)
                throw new BaseStationExeption("baseStation not found");
            return convertor(baseStation);

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStationToList> GetAllBaseStations(Predicate<BaseStationToList> match)
        {
            lock (mydal)
            {
                IEnumerable<DO.BaseStation> list = mydal.Get_all_base_stations(item => true);
                return convertor1(list).FindAll(match);
            }
        }
    }
}