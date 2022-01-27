using System;
using BO;
using System.Threading;
using static BL.BL;
using System.Linq;

namespace BL
{
    internal class Simulator
    {
        int droneSpeed =50;
        int DELAY = 500;
        internal Simulator(BL bl, int id, Action refresh, Func<bool> f)
        {
            DroneToList drone;
            while (!f())
            {
                lock (bl)
                    drone = bl.GetAllDrones(d => d.Id == id).FirstOrDefault();
                try
                {
                    Thread.Sleep(DELAY);
                    switch (drone.Status)
                    {
                        case DroneStatuses.vacant:
                            {
                                try
                                {
                                    lock(bl)
                                        bl.connect_parcel_to_drone(id);
                                }
                                catch (DroneBatteryException)
                                {
                                    lock (bl) 
                                        bl.send_drone_to_charge(id);
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                            break;
                        case DroneStatuses.maintenance:
                            {
                                lock (bl)
                                {
                                    bl.drone_from_charge(id);
                                    if (drone.Battery < 100)
                                        bl.send_drone_to_charge(id);
                                }
                            }
                            break;
                        case DroneStatuses.sending:
                            {
                                ParcelAtTransfer parcelTran = bl.GetCurrectParcelAtTransferOfDrone(id);
                                if (parcelTran.SateOfParcel)
                                {
                                    bl.delivered_parcel_by_drone(id);
                                }
                                else
                                {
                                    bl.pickedUp_parcel_by_drone(id);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    lock (bl)
                    {

                    }

                    refresh();
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
