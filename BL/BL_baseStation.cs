using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IBL
{
    public partial class BL
    {
        public void update_baseStation(int id, string new_name, string new_slot)
        {
            BaseStation baseStation = find_baseStation(id);
            if (new_name != "_")
                baseStation.Name = new_name;
            if (new_slot != "_")
            {
                if (!int.TryParse(new_slot, out int slot)) { throw new IntReadException(); }
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
        public void Add_base_station(BaseStation baseStation)
        {
            baseStation.DroneInChargings = new List<DroneInCharging>();
            IDAL.DO.BaseStation idalBaseStation = convertor(baseStation);
            mydal.Add_base_station(idalBaseStation);
        }
        public string Print_baseStation(int baseStation_id)
        {
            return convertor(mydal.Find_baseStation(baseStation_id)).ToString();
        }
        public string print_all_baseStations()
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
        public string print_all_baseStations_with_free_slots()
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