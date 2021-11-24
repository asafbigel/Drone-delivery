using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public partial class BL
    {
        public void Add_base_station(BaseStation baseStation)
        {
            baseStation.DroneInChargings = new List<DroneInCharging>();
            IDAL.DO.BaseStation idalBaseStation = new IDAL.DO.BaseStation
            {
                ChargeSlots = baseStation.Num_Free_slots_charge,
                Id = baseStation.id,
                Lattitude = baseStation.space.latitude,
                Longitude = baseStation.space.longitude,
                Name = baseStation.name
            };
            mydal.Add_base_station(idalBaseStation);
        }
    }
}