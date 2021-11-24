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
        Random rand = new Random();
        public void Add_drone(Drone drone, int baseStation_num)
        {
            drone.Battery = rand.Next(20, 41);
            drone.Status = DroneStatuses.maintenance;
            IDAL.DO.BaseStation baseStation = mydal.Find_baseStation(baseStation_num);
            drone.Space.longitude = baseStation.Longitude;
            drone.Space.latitude = baseStation.Lattitude;

            IDAL.DO.Drone idalDrone = new IDAL.DO.Drone
            {
                Id = drone.Id,
                MaxWeight = (IDAL.DO.WeightCategories)drone.MaxWeight,
                Model = drone.Model
            };
            mydal.Add_drone(idalDrone);
        }
    }
}