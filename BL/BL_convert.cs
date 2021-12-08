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
        // convertor is between IDAL.DO object and IBL.BO object
        // covnertor1 is between IBL.BO object and IBL.BO object

        /// <summary>
        /// convert from IDAL.DO.DroneCharge object to IBL.BO.DroneInCharging object
        /// </summary>
        /// <param name="droneCharge">the drone charge   </param>
        /// <returns> DroneInCharging object  </returns>
        private DroneInCharging convertor(IDAL.DO.DroneCharge droneCharge)
        {
            DroneInCharging new_drone = new DroneInCharging();
            DroneToList drone = my_drones.Find(x => x.Id == droneCharge.DroneId);
            if (drone == null)
                drone = new DroneToList();
            new_drone.Battery = drone.Battery;
            new_drone.Id = droneCharge.DroneId;
            return new_drone;
        }
        /// <summary>
        /// convert from IDAL.DO.BaseStation object to IBL.BO.BaseStation object
        /// </summary>
        /// <param name="idalBaseStation"></param>
        /// <returns>IBL.BO.BaseStation object </returns>
        private BaseStation convertor(IDAL.DO.BaseStation idalBaseStation)
        {

            BaseStation baseStation = new BaseStation
            {
                Id = idalBaseStation.Id,
                Name = idalBaseStation.Name,
                Num_Free_slots_charge = idalBaseStation.ChargeSlots,
                DroneInChargings = new List<DroneInCharging>() 
            };
            IEnumerable<IDAL.DO.DroneCharge> droneCharges = mydal.Get_all_DroneCharge();
            foreach (var item in droneCharges)
            {
                if (item.StationId == baseStation.Id)
                {
                    DroneInCharging drone = convertor(item);
                    baseStation.DroneInChargings.Add(drone);
                }
            }
            //baseStation.Num_Free_slots_charge -= baseStation.DroneInChargings.Count();
            //if (baseStation.Num_Free_slots_charge < 0)
            //    throw new DroneChargeException("There more drone from slots");
            Location location = new Location();
            location.latitude = idalBaseStation.Lattitude;
            location.longitude = idalBaseStation.Longitude;
            baseStation.BaseStationLocation = location;
            return baseStation;
        }
        /// <summary>
        /// convert from IBL.BO.BaseStation object to  IDAL.DO.BaseStation object
        /// </summary>
        /// <param name="baseStation"> IBL.BO.BaseStation object </param>
        /// <returns> IDAL.DO.BaseStation object </returns>
        private IDAL.DO.BaseStation convertor(BaseStation baseStation)
        {
            IDAL.DO.BaseStation idalBaseStation = new IDAL.DO.BaseStation()
            {
                ChargeSlots = baseStation.Num_Free_slots_charge,
                Id = baseStation.Id,
                Lattitude = baseStation.BaseStationLocation.latitude,
                Longitude = baseStation.BaseStationLocation.longitude,
                Name = baseStation.Name
            };
            return idalBaseStation;
        }
        /// <summary>
        /// convert from IBL.BO.parcel object to  IDAL.DO.parcel object
        /// </summary>
        /// <param name="parcel">IBL.BO.parcel object </param>
        /// <returns>IDAL.DO.parcel object </returns>
        private IDAL.DO.Parcel convertor(Parcel parcel)
        {
            int drone_id =0 ;
            int senderId = 0;
            int TargetId = 0;
            if (parcel.TheDrone != null)
                drone_id = parcel.TheDrone.Id;
            if (parcel.Sender != null)
                senderId = parcel.Sender.Id;
            if (parcel.Getter != null)
                TargetId = parcel.Getter.Id;
            IDAL.DO.Parcel idalParcel = new IDAL.DO.Parcel
            {
                Delivered = parcel.Delivered,
                Requested = parcel.Requested,
                Scheduled = parcel.Scheduled,
                PickedUp = parcel.PickedUp,
                DroneId = drone_id,
                Priority = (IDAL.DO.Priorities)parcel.Priority,
                Weight = (IDAL.DO.WeightCategories)parcel.Weight,
                SenderId = senderId,
                TargetId = TargetId,
                Id = parcel.Id
            };
            return idalParcel;
        }
        /// <summary>
        /// convert from IDAL.DO.parcel object to IBL.BO.parcel object   
        /// </summary>
        /// <param name="idalParcel"> IDAL.DO.parcel object</param>
        /// <returns>IBL.BO.parcel object   </returns>
        private Parcel convertor(IDAL.DO.Parcel idalParcel)
        {
            IDAL.DO.Customer idalGetter = mydal.Find_customer(idalParcel.TargetId);
            IDAL.DO.Customer idalSender = mydal.Find_customer(idalParcel.SenderId);
            CustomerAtParcel getter = new CustomerAtParcel()
            {
                CustomerName = idalGetter.Name,
                Id = idalGetter.Id
            };
            CustomerAtParcel sender = new CustomerAtParcel()
            {
                CustomerName = idalSender.Name,
                Id = idalSender.Id
            };
            DroneToList drone = my_drones.Find(item => item.Id == idalParcel.Id);
            if (drone == null)
                drone = new DroneToList();
            return new Parcel()
            {
                Delivered = idalParcel.Delivered,
                Id = idalParcel.Id,
                PickedUp = idalParcel.PickedUp,
                Priority = (IBL.BO.Priorities)idalParcel.Priority,
                Weight = (IBL.BO.WeightCategories)idalParcel.Weight,
                Requested = idalParcel.Requested,
                Scheduled = idalParcel.Scheduled,
                Getter = getter,
                Sender = sender,
                TheDrone = convertor1(drone)
            };

        }
        /// <summary>
        /// convert from Parcel object to ParcelAtTransfer object
        /// </summary>
        /// <param name="parcel">Parcel</param>
        /// <returns>ParcelAtTransfer</returns>
        private ParcelAtTransfer convertor1(Parcel parcel)
        {
            Location senderLlocation = new Location();
            Location getter_location = new Location();
            IDAL.DO.Customer sender = mydal.Find_customer(parcel.Sender.Id);
            IDAL.DO.Customer getter = mydal.Find_customer(parcel.Getter.Id);
            senderLlocation.longitude = sender.Longitude;
            senderLlocation.latitude = sender.Lattitude;
            getter_location.longitude = sender.Longitude;
            getter_location.latitude = sender.Lattitude;
            return new ParcelAtTransfer()
            {
                Id = parcel.Id,
                Priority = parcel.Priority,
                Weight = parcel.Weight,
                Sender = parcel.Sender,
                Getter = parcel.Getter,
                LocationOfPickUp = senderLlocation,
                SateOfParcel = (parcel.PickedUp != DateTime.MinValue),
                DistanceOfDelivery = distance_between_2_points(getter_location, senderLlocation),
                LocationOfTarget = getter_location
            };
        }
        /// <summary>
        /// convert from IDAL.DO.parcel object to IBL.BO.parcel object  
        /// </summary>
        /// <param name="drone">DroneToList object</param>
        /// <returns>DroneAtParcel object</returns>
        private DroneAtParcel convertor1(DroneToList drone)
        {
            if (drone == null)
                return null;
            return new DroneAtParcel()
            {
                Battery = drone.Battery,
                Id = drone.Id
            };
        }
        /// <summary>
        /// convert from IBL.BO.Customer object  to  IDAL.DO.Customer object 
        /// </summary>
        /// <param name="customer"> IBL.BO.Customer object</param>
        /// <returns>IDAL.DO.Customer object </returns>
        private IDAL.DO.Customer convertor(Customer customer)
        {
            IDAL.DO.Customer idalCustomer = new IDAL.DO.Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Lattitude = customer.CustomerLocation.latitude,
                Longitude = customer.CustomerLocation.longitude,
                Phone = customer.Phone
            };
            return idalCustomer;
        }

        /// <summary>
        /// convert from  IDAL.DO.Customer object  to IBL.BO.Customer object  
        /// </summary>
        /// <param name="drone">IDAL.DO.Customer object</param>
        /// <returns>IBL.BO.Customer object</returns>
        private IDAL.DO.Drone convertor(Drone drone)
        {
            IDAL.DO.Drone idalDrone = new IDAL.DO.Drone()
            {
                Id = drone.Id,
                MaxWeight = (IDAL.DO.WeightCategories)drone.MaxWeight,
                Model = drone.Model
            };
            return idalDrone;
        }
        /// <summary>
        /// convert from Drone object  to DroneToList object  
        /// </summary>
        /// <param name="drone">Drone object </param>
        /// <returns>DroneToList object </returns>
        private DroneToList convertor3(Drone drone)
        {
            DroneToList droneToList = new DroneToList()
            {
                Id = drone.Id,
                MaxWeight = drone.MaxWeight,
                Model = drone.Model,
                Battery = drone.Battery,
                DroneLocation = drone.DroneLocation,
                Status = drone.Status,
                NumOfParcel = 0
            };
            return droneToList;
        }
        /// <summary>
        /// convert from IEnumerable<IDAL.DO.BaseStation>  to List<BaseStationToList>
        /// </summary>
        /// <param name="enumerable">the IEnumerable<IDAL.DO.BaseStation> </param>
        /// <returns> List<BaseStationToList> after convert </returns>
        private List<BaseStationToList> convertor1(IEnumerable<IDAL.DO.BaseStation> enumerable)
        {
            List<BaseStationToList> baseStations = new List<BaseStationToList>();
            foreach (var item in enumerable)
            {
                baseStations.Add(convertor4(item));
            }
            return baseStations;
        }
        /// <summary>
        /// convert from IEnumerable<IDAL.DO.BaseStation>  to List<BaseStation>
        /// </summary>
        /// <param name="enumerable">the IEnumerable<IDAL.DO.BaseStation></param>
        /// <returns>List<BaseStation> after convert</returns>
        private List<BaseStation> convertor(IEnumerable<IDAL.DO.BaseStation> enumerable)
        {
            List<BaseStation> baseStations = new List<BaseStation>();
            foreach (var item in enumerable)
            {
                baseStations.Add(convertor(item));
            }
            return baseStations;
        }
        /// <summary>
        /// convert from IDAL.DO.BaseStation object  to  IBL.BO.BaseStationToList object  
        /// </summary>
        /// <param name="item"> IDAL.DO.BaseStation object </param>
        /// <returns>IBL.BO.BaseStationToList object after convert </returns>
        private BaseStationToList convertor4(IDAL.DO.BaseStation item)
        {
            List<DroneInCharging> droneCharge = convertor(mydal.Get_all_DroneCharge().ToList().FindAll(charge => charge.StationId == item.Id));
            return new BaseStationToList()
            {
                Id = item.Id,
                Name = item.Name,
                NumOfBusySlots = droneCharge.Count(),
                NumOfFreeSlots = item.ChargeSlots
            };
        }
        /// <summary>
        /// convert  from List<IDAL.DO.DroneCharge>  to  List<DroneInCharging>  
        /// </summary>
        /// <param name="idal_droneCharges">List<IDAL.DO.DroneCharge></param>
        /// <returns> List<DroneInCharging> after convert </returns>
        private List<DroneInCharging> convertor(List<IDAL.DO.DroneCharge> idal_droneCharges)
        {
            List<DroneInCharging> droneInChargings = new List<DroneInCharging>();
            foreach (var item in idal_droneCharges)
            {
                droneInChargings.Add(convertor(item));
            }
            return droneInChargings;
        }
        /// <summary>
        /// convert  from IEnumerable<IDAL.DO.Parcel>  to  List<ParcelToList>  
        /// </summary>
        /// <param name="enumerable">IEnumerable<IDAL.DO.Parcel></param>
        /// <returns> List<ParcelToList> after convert  </returns>
        private List<ParcelToList> convertor4(IEnumerable<IDAL.DO.Parcel> enumerable)
        {
            List<ParcelToList> parcels = new List<ParcelToList>();
            foreach (var item in enumerable)
            {
                parcels.Add(convertor4(item));
            }
            return parcels;
        }
        /// <summary>
        /// convert  from IEnumerable<IDAL.DO.Parcel>  to List<ParcelToList>  
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        private List<ParcelToList> convertor(IEnumerable<IDAL.DO.Parcel> enumerable)
        {
            List<ParcelToList> parcels = new List<ParcelToList>();
            foreach (var item in enumerable)
            {
                parcels.Add(convertor4(item));
            }
            return parcels;
        }
        /// <summary>
        /// convert from IDAL.DO.Parcel object  to  IBL.BO.ParcelToList object
        /// </summary>
        /// <param name="item">IDAL.DO.Parcel object</param>
        /// <returns> IBL.BO.ParcelToList object after convert</returns>
        private ParcelToList convertor4(IDAL.DO.Parcel item)
        {
            ParcelStatuses parcelStatuses;
            if (item.Delivered != DateTime.MinValue)
                parcelStatuses = ParcelStatuses.delivered;
            else
            {
                if (item.PickedUp != DateTime.MinValue)
                    parcelStatuses = ParcelStatuses.picked_up;
                else
                {
                    if (item.Scheduled != DateTime.MinValue)
                        parcelStatuses = ParcelStatuses.Belongs;
                    else
                        parcelStatuses = ParcelStatuses.Defined;

                }
            }
            string target_name = mydal.Find_customer(item.TargetId).Name;
            string sender_name = mydal.Find_customer(item.SenderId).Name;
            return new ParcelToList()
            {
                GetterName = target_name,
                SenderName = sender_name,
                Id = item.Id,
                Priority = (IBL.BO.Priorities)item.Priority,
                Status = parcelStatuses,
                Weight = (IBL.BO.WeightCategories)item.Weight
            };
        }
        /// <summary>
        /// convert  from IEnumerable<IDAL.DO.Customer>  to List<CustomerToList>> 
        /// </summary>
        /// <param name="enumerable">IEnumerable<IDAL.DO.Customer></param>
        /// <returns>List<CustomerToList> after convert</returns>
        private List<CustomerToList> convertor(IEnumerable<IDAL.DO.Customer> enumerable)
        {
            List<CustomerToList> customers = new List<CustomerToList>();
            foreach (var item in enumerable)
            {
                customers.Add(convertor4(item));
            }
            return customers;
        }
        /// <summary>
        ///  convert  from IEnumerable<IDAL.DO.Customer>  to List<Customer>
        /// </summary>
        /// <param name="enumerable"> IEnumerable<IDAL.DO.Customer> </param>
        /// <returns> List<Customer> after convert </returns>
        private List<Customer> convertor1(IEnumerable<IDAL.DO.Customer> enumerable)
        {
            List<Customer> customers = new List<Customer>();
            foreach (var item in enumerable)
            {
                customers.Add(convertor(item));
            }
            return customers;
        }
        /// <summary>
        /// convert  from IDAL.DO.Customer object  to IBL.BO.CustomerToList object
        /// </summary>
        /// <param name="item">IDAL.DO.Customer object</param>
        /// <returns>IBL.BO.CustomerToList object</returns>
        private CustomerToList convertor4(IDAL.DO.Customer item)
        {
            List<IDAL.DO.Parcel> parcels_got = mydal.Get_all_parcels().ToList().FindAll                 (parcel => parcel.TargetId == item.Id && parcel.Delivered != DateTime.MinValue);
            List<IDAL.DO.Parcel> parcels_to_get = mydal.Get_all_parcels().ToList().FindAll              (parcel => parcel.TargetId == item.Id && parcel.Delivered == DateTime.MinValue);
            List<IDAL.DO.Parcel> parcels_sent_and_arrived = mydal.Get_all_parcels().ToList().FindAll    (parcel => parcel.SenderId == item.Id && parcel.Delivered != DateTime.MinValue);
            List<IDAL.DO.Parcel> parcels_sent_and_not_arrived = mydal.Get_all_parcels().ToList().FindAll(parcel => parcel.SenderId == item.Id && parcel.Delivered == DateTime.MinValue);
            return new CustomerToList()
            {
                Id = item.Id,
                Name = item.Name,
                Phone = item.Phone,
                NumOfParcelsGot = parcels_got.Count(),
                NumOfParcelsSentAndArrived = parcels_sent_and_arrived.Count(),
                NumOfParcelsSentAndNotArrived = parcels_sent_and_not_arrived.Count(),
                numOfParcelsToGet = parcels_to_get.Count()
            };
        }
        /// <summary>
        /// convert  from  IEnumerable<IDAL.DO.Drone>  to  List<DroneToList>
        /// </summary>
        /// <param name="enumerable"> IEnumerable<IDAL.DO.Drone> </param>
        /// <returns>  List<DroneToList> after convert </returns>
        private List<DroneToList> convertor(IEnumerable<IDAL.DO.Drone> enumerable)
        {
            List<DroneToList> drones = new List<DroneToList>();
            foreach (var item in enumerable)
            {
                drones.Add(convertor4(item));
            }
            return drones;
        }
        /// <summary>
        /// convert  from IDAL.DO.Drone item object  to IBL.BO.DroneToList object
        /// </summary>
        /// <param name="item"> IDAL.DO.Drone item object</param>
        /// <returns> IBL.BO.DroneToList object after convert</returns>
        private DroneToList convertor4(IDAL.DO.Drone item)
        {
            Drone drone = convertor(item);
            List<IDAL.DO.Parcel> parcels = mydal.Get_all_parcels().ToList().FindAll(parcel => parcel.DroneId == item.Id);
            return new DroneToList()
            {
                Battery = drone.Battery,
                Id = drone.Id,
                DroneLocation = drone.DroneLocation,
                MaxWeight = drone.MaxWeight,
                Model = drone.Model,
                Status = drone.Status,
                NumOfParcel = parcels.Count()
            };
        }
        /// <summary>
        /// convert  from  List<IDAL.DO.Parcel>  to List<Parcel>
        /// </summary>
        /// <param name="idalparcels">  List<IDAL.DO.Parcel> </param>
        /// <returns> List<Parcel> after convert </returns>
        private List<Parcel> convertor(List<IDAL.DO.Parcel> idalparcels)
        {
            List<Parcel> parcels = new List<Parcel>();
            foreach (var item in idalparcels)
            {
                parcels.Add(convertor(item));
            }
            return parcels;
        }
        /// <summary>
        ///  convert  from List<Parcel>  to List<ParcelAtTransfer>
        /// </summary>
        /// <param name="parcelsHighPriority"> List<Parcel> list </param>
        /// <returns>List<ParcelAtTransfer> after convert </returns>
        private List<ParcelAtTransfer> convertor1(List<Parcel> parcelsHighPriority)
        {
            List<ParcelAtTransfer> new_list = new List<ParcelAtTransfer>();
            foreach (var item in parcelsHighPriority)
            {
                new_list.Add(convertor1(item));
            }
            return new_list;
        }
        /// <summary>
        /// convert  from IDAL.DO.Drone object  to IBL.BO.Drone object
        /// </summary>
        /// <param name="idalDrone">IDAL.DO.Drone object</param>
        /// <returns>IBL.BO.Drone object after convert </returns>
        private Drone convertor(IDAL.DO.Drone idalDrone)
        {
            DroneToList droneToList = my_drones.Find(item => item.Id == idalDrone.Id);
            if (droneToList == null)
                droneToList = new DroneToList();
            return new Drone()
            {
                Battery = droneToList.Battery,
                Id = droneToList.Id,
                MaxWeight = droneToList.MaxWeight,
                Model = droneToList.Model,
                DroneLocation = droneToList.DroneLocation,
                Status = droneToList.Status
            };
        }
        /// <summary>
        /// convert  from IDAL.DO.Customer object  to IBL.BO.Customer object
        /// </summary>
        /// <param name="idal_customer"></param>
        /// <returns></returns>
        private Customer convertor(IDAL.DO.Customer idal_customer)
        {
            Location location = new Location();
            location.latitude = idal_customer.Lattitude;
            location.longitude = idal_customer.Longitude;
            Customer customer = new Customer()
            {
                Id = idal_customer.Id,
                Name = idal_customer.Name,
                Phone = idal_customer.Phone,
                CustomerLocation = location
            };
            customer.parcelsAtCustomerFor = new List<ParcelAtCustomer>();
            customer.ParcelsAtCustomerFrom = new List<ParcelAtCustomer>();
            List<IDAL.DO.Parcel> parcels = mydal.Get_all_parcels().ToList();
            foreach (var parcel in parcels)
            {
                ParcelAtCustomer parcelAtCustomer = convertor2(parcel);
                if (parcel.SenderId == idal_customer.Id)
                {
                    parcelAtCustomer.OtherCustomer = convertor1(convertor(mydal.Find_customer(parcel.TargetId)));
                    customer.ParcelsAtCustomerFrom.Add(parcelAtCustomer);
                }
                if (parcel.TargetId == idal_customer.Id)
                {
                    parcelAtCustomer.OtherCustomer = convertor1(convertor(mydal.Find_customer(parcel.SenderId)));
                    customer.ParcelsAtCustomerFrom.Add(parcelAtCustomer);
                }
            }
            return customer;
        }

        /// <summary>
        /// convert  from ParcelAtCustomer object  to IBL.BO.Customer object
        /// </summary>
        /// <param name="parcel">ParcelAtCustomer object </param>
        /// <returns>IBL.BO.Customer object</returns>
        private ParcelAtCustomer convertor2(IDAL.DO.Parcel parcel)
        {
            ParcelStatuses status = ParcelStatuses.Defined;
            if (parcel.Requested != DateTime.MinValue)
                status = ParcelStatuses.Belongs;
            if (parcel.PickedUp != DateTime.MinValue)
                status = ParcelStatuses.picked_up;
            if (parcel.Delivered != DateTime.MinValue)
                status = ParcelStatuses.delivered;
            //CustomerAtParcel other;
            //if ()
            //other = convertor1(convertor(mydal.Find_customer(parcel.SenderId)));
            return new ParcelAtCustomer()
            {
                Id = parcel.Id,
                Priority = (Priorities)parcel.Priority,
                Status = status,
                Weight = (WeightCategories)parcel.Weight
            };
        }

        /// <summary>
        /// convert  from Customer object  to CustomerAtParcel object
        /// </summary>
        /// <param name="customer"> Customer object</param>
        /// <returns>CustomerAtParcel object after convert </returns>
        private CustomerAtParcel convertor1(Customer customer)
        {
            return new CustomerAtParcel()
            {
                CustomerName = customer.Name,
                Id = customer.Id
            };
        }
    }
}