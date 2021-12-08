using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IBL
{
    public partial class BL
    {
        /// <summary>
        /// fanction that update baseStation
        /// </summary>
        /// <param name="id">id of  baseStation </param>
        /// <param name="new_name"> the new name of the update baseStation   </param>
        /// <param name="new_slot">the new number of free slot charge of  the update baseStation</param>
        public void update_baseStation(int id, string new_name, string new_slot)
        {
            BaseStation baseStation = find_baseStation(id);
            if (new_name != "_")
                baseStation.Name = new_name;
            if (new_slot != "_")
            {
                if (!int.TryParse(new_slot, out int slot)) { throw new IntReadException("Wrong input"); }
                if (slot >= baseStation.Num_Free_slots_charge)
                    baseStation.Num_Free_slots_charge = slot;
                else
                    throw new slotException("There more drone at charge now");
            }
            mydal.UpdateBaseStation(convertor(baseStation));
        }

        /// <summary>
        /// get BL base station
        /// </summary>
        /// <param name="id"> id of the base station</param>
        /// <returns>
        /// BL base station
        /// </returns>
        private BaseStation find_baseStation(int id)
        {
            IDAL.DO.BaseStation idalBaseStation = mydal.Find_baseStation(id);
            BaseStation baseStation = convertor(idalBaseStation);
            return baseStation;
        }
        /// <summary>
        /// Add base station to the list
        /// </summary>
        /// <param name="baseStation">
        /// The base station
        /// </param>
        public void Add_base_station(BaseStation baseStation)
        {
            baseStation.DroneInChargings = new List<DroneInCharging>();
            IDAL.DO.BaseStation idalBaseStation = convertor(baseStation);
            mydal.Add_base_station(idalBaseStation);
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
        public string string_baseStation(int baseStation_id)
        {
            return convertor(mydal.Find_baseStation(baseStation_id)).ToString();
        }
        /// <summary>
        /// ToString of all the baseStations
        /// </summary>
        /// <returns> the string of ToString of all the baseStations </returns>
        public string string_all_baseStations()
        {
            List<BaseStationToList> baseStations = convertor1(mydal.Get_all_base_stations());
            string result = "";
            foreach (var item in baseStations)
            {
                result += item.ToString();
                result += "\n";
            }
            return result;
        }
        /// <summary>
        /// ToString of all the baseStations with free slots
        /// </summary>
        /// <returns>the string of ToString of all the baseStations with free slots</returns>
        public string string_all_baseStations_with_free_slots()
        {
            List<BaseStationToList> baseStations = convertor1(mydal.Get_all_base_stations_with_free_charge_slot());
            string result = "";
            foreach (var item in baseStations)
            {
                result += item.ToString();
                result += "\n";
            }
            return result;
        }
    }
}