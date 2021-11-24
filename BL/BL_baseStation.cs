using IBL.BO;
using System.Collections.Generic;
using System.Linq;

namespace IBL
{
    public partial class BL
    {
        public void update_baseStation(int id, string new_name, string new_slot)
        {
            BaseStation baseStation = new_baseStation(id);
            if (new_name != "_")
                baseStation.name = new_name;
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
        private BaseStation new_baseStation(int id)
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
    }
}