using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using IDAL.DO;
using IBL.BO;


namespace BL
{
    internal partial class BL : IBL.IBL
    {
        BL()
        {
            IDAL.IDal mydal = new DalObject.DalObject();
            double[] Electricity = mydal.ElectricityUse();
            double Electricity_free = Electricity[0];
            double Electricity_light = Electricity[1];
            double Electricity_medium = Electricity[2];
            double Electricity_heavy = Electricity[3];
            double Charge_at_hour = Electricity[4];
        }
        public void AddBaseStation(BaseStation baseStation)
        {
            
        }
    }
}