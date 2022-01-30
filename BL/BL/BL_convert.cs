using BO;
using System.Collections.Generic;
using System.Linq;


namespace BL
{
    public partial class BL
    {
        // convertor is between DO object and BO object
        // covnertor1 is between BO object and BO object

        /// <summary>
        /// convert from DO.DroneCharge object to BO.DroneInCharging object
        /// </summary>
        /// <param name="droneCharge">the drone charge   </param>
        /// <returns> DroneInCharging object  </returns>
        static private DroneInCharging convertor(DO.DroneCharge droneCharge)
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
        /// convert from DO.BaseStation object to BO.BaseStation object
        /// </summary>
        /// <param name="idalBaseStation"></param>
        /// <returns>BO.BaseStation object </returns>
        static private BaseStation convertor(DO.BaseStation idalBaseStation)
        {
            lock (mydal)
            {
                BaseStation baseStation = new BaseStation
                {
                    Id = idalBaseStation.Id,
                    Name = idalBaseStation.Name,
                    Num_Free_slots_charge = idalBaseStation.ChargeSlots,
                    DroneInChargings = new List<DroneInCharging>()
                };
                IEnumerable<DO.DroneCharge> droneCharges = mydal.Get_all_DroneCharge();
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
        }
        /// <summary>
        /// convert from BO.BaseStation object to  DO.BaseStation object
        /// </summary>
        /// <param name="baseStation"> BO.BaseStation object </param>
        /// <returns> DO.BaseStation object </returns>
        static private DO.BaseStation convertor(BaseStation baseStation)
        {
            
                DO.BaseStation idalBaseStation = new DO.BaseStation()
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
        /// convert from BO.parcel object to  DO.parcel object
        /// </summary>
        /// <param name="parcel">BO.parcel object </param>
        /// <returns>DO.parcel object </returns>
        static private DO.Parcel convertor(Parcel parcel)
        {
            int drone_id = 0;
            int senderId = 0;
            int TargetId = 0;
            if (parcel.TheDrone != null)
                drone_id = parcel.TheDrone.Id;
            if (parcel.Sender != null)
                senderId = parcel.Sender.Id;
            if (parcel.Getter != null)
                TargetId = parcel.Getter.Id;
            DO.Parcel idalParcel = new DO.Parcel
            {
                Delivered = parcel.Delivered,
                Requested = parcel.Requested,
                Scheduled = parcel.Scheduled,
                PickedUp = parcel.PickedUp,
                DroneId = drone_id,
                Priority = (DO.Priorities)parcel.Priority,
                Weight = (DO.WeightCategories)parcel.Weight,
                SenderId = senderId,
                TargetId = TargetId,
                Id = parcel.Id
            };
            return idalParcel;
        }
        /// <summary>
        /// convert from DO.parcel object to BO.parcel object   
        /// </summary>
        /// <param name="idalParcel"> DO.parcel object</param>
        /// <returns>BO.parcel object   </returns>
        static private Parcel convertor(DO.Parcel idalParcel)
        {
            lock (mydal)
            {
                if (idalParcel.Id == default(int))
                    return null;
                DO.Customer idalGetter = mydal.Find_customer(idalParcel.TargetId);
                DO.Customer idalSender = mydal.Find_customer(idalParcel.SenderId);
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
                DroneToList drone = my_drones.Find(item => item.Id == idalParcel.DroneId);
                if (drone == null)
                    drone = new DroneToList();
                return new Parcel()
                {
                    Delivered = idalParcel.Delivered,
                    Id = idalParcel.Id,
                    PickedUp = idalParcel.PickedUp,

                    Priority = (Priorities)idalParcel.Priority,
                    Weight = (WeightCategories)idalParcel.Weight,
                    Requested = idalParcel.Requested,
                    Scheduled = idalParcel.Scheduled,
                    Getter = getter,
                    Sender = sender,
                    TheDrone = convertor1(drone)
                };
            }
        }
        /// <summary>
        /// convert from Parcel object to ParcelAtTransfer object
        /// </summary>
        /// <param name="parcel">Parcel</param>
        /// <returns>ParcelAtTransfer</returns>
        static private ParcelAtTransfer convertor1(Parcel parcel)
        {
            lock (mydal)
            {
                if (parcel == null)
                    return null;
                Location senderLlocation = new Location();
                Location getter_location = new Location();
                DO.Customer sender = mydal.Find_customer(parcel.Sender.Id);
                DO.Customer getter = mydal.Find_customer(parcel.Getter.Id);
                senderLlocation.longitude = sender.Longitude;
                senderLlocation.latitude = sender.Lattitude;
                getter_location.longitude = getter.Longitude;
                getter_location.latitude = getter.Lattitude;
                bool pickedUp = parcel.PickedUp != null;
                double disstance;
                if(pickedUp || parcel.TheDrone.Id==0)
                   disstance = distance_between_2_points(getter_location, senderLlocation);
                else
                    disstance = distance_between_2_points(my_drones.Find(d => d.Id == parcel.TheDrone.Id).DroneLocation, getter_location);
                return new ParcelAtTransfer()
                {
                    Id = parcel.Id,
                    Priority = parcel.Priority,
                    Weight = parcel.Weight,
                    Sender = parcel.Sender,
                    Getter = parcel.Getter,
                    LocationOfPickUp = senderLlocation,
                    SateOfParcel = pickedUp,
                    DistanceOfDelivery = disstance,
                    LocationOfTarget = getter_location
                };
            }
        }
        /// <summary>
        /// convert from DO.parcel object to BO.parcel object  
        /// </summary>
        /// <param name="drone">DroneToList object</param>
        /// <returns>DroneAtParcel object</returns>
        static private DroneAtParcel convertor1(DroneToList drone)
        {
            if (drone == null)
                return null;
            return new DroneAtParcel()
            {
                Battery = drone.Battery,
                Id = drone.Id,
                DroneLocation = drone.DroneLocation
            };
        }
        /// <summary>
        /// convert from BO.Customer object  to  DO.Customer object 
        /// </summary>
        /// <param name="customer"> BO.Customer object</param>
        /// <returns>DO.Customer object </returns>
        static private DO.Customer convertor(Customer customer)
        {
            DO.Customer idalCustomer = new DO.Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Lattitude = customer.CustomerLocation.latitude,
                Longitude = customer.CustomerLocation.longitude,
                Phone = customer.Phone,
                Password = customer.Password
            };
            return idalCustomer;
        }

        /// <summary>
        /// convert from  DO.Drone object  to BO.Customer object  
        /// </summary>
        /// <param name="drone">DO.Drone object</param>
        /// <returns>BO.Customer object</returns>
        static private DO.Drone convertor(Drone drone)
        {
            DO.Drone idalDrone = new DO.Drone()
            {
                Id = drone.Id,
                MaxWeight = (DO.WeightCategories)drone.MaxWeight,
                Model = drone.Model
            };
            return idalDrone;
        }

        /// <summary>
        /// convert from IEnumerable<DO.BaseStation>  to List<BaseStationToList>
        /// </summary>
        /// <param name="enumerable">the IEnumerable<DO.BaseStation> </param>
        /// <returns> List<BaseStationToList> after convert </returns>
        static private List<BaseStationToList> convertor1(IEnumerable<DO.BaseStation> enumerable)
        {
            List<BaseStationToList> baseStations = new List<BaseStationToList>();
            foreach (var item in enumerable)
            {
                baseStations.Add(convertor4(item));
            }
            return baseStations;
        }
        /// <summary>
        /// convert from IEnumerable<DO.BaseStation>  to List<BaseStation>
        /// </summary>
        /// <param name="enumerable">the IEnumerable<DO.BaseStation></param>
        /// <returns>List<BaseStation> after convert</returns>
        static private List<BaseStation> convertor(IEnumerable<DO.BaseStation> enumerable)
        {
            List<BaseStation> baseStations = new List<BaseStation>();
            foreach (var item in enumerable)
            {
                baseStations.Add(convertor(item));
            }
            return baseStations;
        }
        /// <summary>
        /// convert from DO.BaseStation object  to  BO.BaseStationToList object  
        /// </summary>
        /// <param name="item"> DO.BaseStation object </param>
        /// <returns>BO.BaseStationToList object after convert </returns>
        static private BaseStationToList convertor4(DO.BaseStation item)
        {
            lock (mydal)
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
        }
        /// <summary>
        /// convert  from List<DO.DroneCharge>  to  List<DroneInCharging>  
        /// </summary>
        /// <param name="idal_droneCharges">List<DO.DroneCharge></param>
        /// <returns> List<DroneInCharging> after convert </returns>
        static private List<DroneInCharging> convertor(List<DO.DroneCharge> idal_droneCharges)
        {
            List<DroneInCharging> droneInChargings = new List<DroneInCharging>();
            foreach (var item in idal_droneCharges)
            {
                droneInChargings.Add(convertor(item));
            }
            return droneInChargings;
        }
        /// <summary>
        /// convert  from IEnumerable<DO.Parcel>  to  List<ParcelToList>  
        /// </summary>
        /// <param name="enumerable">IEnumerable<DO.Parcel></param>
        /// <returns> List<ParcelToList> after convert  </returns>
        static private List<ParcelToList> convertor4(IEnumerable<DO.Parcel> enumerable)
        {
            List<ParcelToList> parcels = new List<ParcelToList>();
            foreach (var item in enumerable)
            {
                parcels.Add(convertor4(item));
            }
            return parcels;
        }
        /// <summary>
        /// convert  from IEnumerable<DO.Parcel>  to List<ParcelToList>  
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        static private List<ParcelToList> convertor(IEnumerable<DO.Parcel> enumerable)
        {
            List<ParcelToList> parcels = new List<ParcelToList>();
            foreach (var item in enumerable)
            {
                parcels.Add(convertor4(item));
            }
            return parcels;
        }
        /// <summary>
        /// convert from DO.Parcel object  to  BO.ParcelToList object
        /// </summary>
        /// <param name="item">DO.Parcel object</param>
        /// <returns> BO.ParcelToList object after convert</returns>
        static private ParcelToList convertor4(DO.Parcel item)
        {
            lock (mydal)
            {
                ParcelStatuses parcelStatuses;
                if (item.Delivered != null)
                    parcelStatuses = ParcelStatuses.delivered;
                else
                {
                    if (item.PickedUp != null)
                        parcelStatuses = ParcelStatuses.picked_up;
                    else
                    {
                        if (item.Scheduled != null)
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
                    Priority = (Priorities)item.Priority,
                    Status = parcelStatuses,
                    Weight = (WeightCategories)item.Weight
                };
            }
        }
        /// <summary>
        /// convert  from IEnumerable<DO.Customer>  to List<CustomerToList>> 
        /// </summary>
        /// <param name="enumerable">IEnumerable<DO.Customer></param>
        /// <returns>List<CustomerToList> after convert</returns>
        static private List<CustomerToList> convertor(IEnumerable<DO.Customer> enumerable)
        {
            List<CustomerToList> customers = new List<CustomerToList>();
            foreach (var item in enumerable)
            {
                customers.Add(convertor4(item));
            }
            return customers;
        }
        /// <summary>
        ///  convert  from IEnumerable<DO.Customer>  to List<Customer>
        /// </summary>
        /// <param name="enumerable"> IEnumerable<DO.Customer> </param>
        /// <returns> List<Customer> after convert </returns>
        static private IEnumerable<Customer> convertor1(IEnumerable<DO.Customer> enumerable)
        {
            List<Customer> customers = new List<Customer>();
            foreach (var item in enumerable)
            {
                customers.Add(convertor(item));
            }
            return customers;
        }
        /// <summary>
        /// convert  from DO.Customer object  to BO.CustomerToList object
        /// </summary>
        /// <param name="item">DO.Customer object</param>
        /// <returns>BO.CustomerToList object</returns>
        static private CustomerToList convertor4(DO.Customer item)
        {
            lock (mydal)
            {
                List<DO.Parcel> parcels_got = mydal.Get_all_parcels(x => true).ToList().FindAll(parcel => parcel.TargetId == item.Id && parcel.Delivered != null);
                List<DO.Parcel> parcels_to_get = mydal.Get_all_parcels(x => true).ToList().FindAll(parcel => parcel.TargetId == item.Id && parcel.Delivered == null);
                List<DO.Parcel> parcels_sent_and_arrived = mydal.Get_all_parcels(x => true).ToList().FindAll(parcel => parcel.SenderId == item.Id && parcel.Delivered != null);
                List<DO.Parcel> parcels_sent_and_not_arrived = mydal.Get_all_parcels(x => true).ToList().FindAll(parcel => parcel.SenderId == item.Id && parcel.Delivered == null);
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
        }
        /// <summary>
        /// convert  from  IEnumerable<DO.Drone>  to  List<DroneToList>
        /// </summary>
        /// <param name="enumerable"> IEnumerable<DO.Drone> </param>
        /// <returns>  List<DroneToList> after convert </returns>
        static private List<DroneToList> convertor(IEnumerable<DO.Drone> enumerable)
        {
            List<DroneToList> drones = new List<DroneToList>();
            foreach (var item in enumerable)
            {
                drones.Add(convertor4(item));
            }
            return drones;
        }
        /// <summary>
        /// convert  from DO.Drone item object  to BO.DroneToList object
        /// </summary>
        /// <param name="item"> DO.Drone item object</param>
        /// <returns> BO.DroneToList object after convert</returns>
        static private DroneToList convertor4(DO.Drone item)
        {
            lock (mydal)
            {
                Drone drone = convertor(item);
                List<DO.Parcel> parcels = mydal.Get_all_parcels(x => true).ToList().FindAll(parcel => parcel.DroneId == item.Id);
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
        }
        /// <summary>
        /// convert  from  List<DO.Parcel>  to List<Parcel>
        /// </summary>
        /// <param name="idalparcels">  List<DO.Parcel> </param>
        /// <returns> List<Parcel> after convert </returns>
        static private List<Parcel> convertor(List<DO.Parcel> idalparcels)
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
        static private List<ParcelAtTransfer> convertor1(List<Parcel> parcelsHighPriority)
        {
            List<ParcelAtTransfer> new_list = new List<ParcelAtTransfer>();
            foreach (var item in parcelsHighPriority)
            {
                new_list.Add(convertor1(item));
            }
            return new_list;
        }
        /// <summary>
        /// convert  from DO.Drone object  to BO.Drone object
        /// </summary>
        /// <param name="idalDrone">DO.Drone object</param>
        /// <returns>BO.Drone object after convert </returns>
        static private Drone convertor(DO.Drone idalDrone)
        {
            DroneToList droneToList = my_drones.Find(item => item.Id == idalDrone.Id);
            return convertor3(droneToList);
        }
        private static ParcelAtTransfer convertor1(DO.Parcel parcel)
        {
            lock (mydal)
            {
                Customer target = convertor(mydal.Find_customer(parcel.TargetId));
                Customer sender = convertor(mydal.Find_customer(parcel.SenderId));
                return new ParcelAtTransfer()
                {
                    Id = parcel.Id,
                    Getter = convertor1(target),
                    Sender = convertor1(sender),
                    Priority = (Priorities)parcel.Priority,
                    Weight = (WeightCategories)parcel.Weight,
                    LocationOfTarget = target.CustomerLocation,
                    LocationOfPickUp = sender.CustomerLocation,
                    DistanceOfDelivery = distance_between_2_points(target.CustomerLocation, sender.CustomerLocation),
                    SateOfParcel = parcel.PickedUp != null
                };
            }
        }
        /// <summary>
        /// convert  from DO.Customer object  to BO.Customer object
        /// </summary>
        /// <param name="idal_customer"></param>
        /// <returns></returns>
        static private Customer convertor(DO.Customer idal_customer)
        {
            lock (mydal)
            {
                Location location = new Location();
                location.latitude = idal_customer.Lattitude;
                location.longitude = idal_customer.Longitude;
                Customer customer = new Customer()
                {
                    Id = idal_customer.Id,
                    Name = idal_customer.Name,
                    Phone = idal_customer.Phone,
                    Password= idal_customer.Password,
                    CustomerLocation = location
                };
                customer.ParcelsAtCustomerFor = new List<ParcelAtCustomer>();
                customer.ParcelsAtCustomerFrom = new List<ParcelAtCustomer>();
                List<DO.Parcel> parcels = mydal.Get_all_parcels(x => true).ToList();
                foreach (var parcel in parcels)
                {
                    ParcelAtCustomer parcelAtCustomer = convertor2(parcel);
                    if (parcel.SenderId == idal_customer.Id)
                    {

                        parcelAtCustomer.OtherCustomer = convertor2(mydal.Find_customer(parcel.TargetId));
                        customer.ParcelsAtCustomerFrom.Add(parcelAtCustomer);
                    }
                    if (parcel.TargetId == idal_customer.Id)
                    {
                        parcelAtCustomer.OtherCustomer = convertor2(mydal.Find_customer(parcel.SenderId));
                        customer.ParcelsAtCustomerFor.Add(parcelAtCustomer);
                    }
                }
                return customer;
            }
        }
        static private CustomerAtParcel convertor2(DO.Customer customer)
        {
            return new CustomerAtParcel()
            {
                CustomerName = customer.Name,
                Id = customer.Id
            };
        }

        /// <summary>
        /// convert  from ParcelAtCustomer object  to BO.Customer object
        /// </summary>
        /// <param name="parcel">ParcelAtCustomer object </param>
        /// <returns>BO.Customer object</returns>
        static private ParcelAtCustomer convertor2(DO.Parcel parcel)
        {
            lock (mydal)
            {
                ParcelStatuses status = ParcelStatuses.Defined;
                if (parcel.Requested != null)
                    status = ParcelStatuses.Belongs;
                if (parcel.PickedUp != null)
                    status = ParcelStatuses.picked_up;
                if (parcel.Delivered != null)
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
        }

        /// <summary>
        /// convert  from Customer object  to CustomerAtParcel object
        /// </summary>
        /// <param name="customer"> Customer object</param>
        /// <returns>CustomerAtParcel object after convert </returns>
        static private CustomerAtParcel convertor1(Customer customer)
        {
            return new CustomerAtParcel()
            {
                CustomerName = customer.Name,
                Id = customer.Id
            };
        }
        private DroneInCharging convertor2(DroneToList drone)
        {
            lock (mydal)
            {
                DO.DroneCharge doDrone = mydal.FindDroneCharge(drone.Id);
                return new DroneInCharging()
                {
                    Battery = drone.Battery,
                    Id = drone.Id,
                    EnterToCharge = doDrone.EnterToCharge
                };
            }
        }
        /// <summary>
        /// convert from Drone object  to DroneToList object  
        /// </summary>
        /// <param name="drone">Drone object </param>
        /// <returns>DroneToList object </returns>
        static private DroneToList convertor3(Drone drone)
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
        static private Drone convertor3(DroneToList droneToList)
        {
            lock (mydal)
            {
                DO.Parcel parcel = new DO.Parcel();
                if (droneToList.Status == DroneStatuses.sending)
                    parcel = mydal.Get_all_parcels(item => item.DroneId == droneToList.Id && item.Delivered == null).First();
                if (parcel.Id != default(int))
                    return new Drone()
                    {
                        Battery = droneToList.Battery,
                        Id = droneToList.Id,
                        MaxWeight = droneToList.MaxWeight,
                        Model = droneToList.Model,
                        DroneLocation = droneToList.DroneLocation,
                        Status = droneToList.Status,
                        Parcel = convertor1(parcel)
                    };
                return new Drone()
                {
                    Battery = droneToList.Battery,
                    Id = droneToList.Id,
                    MaxWeight = droneToList.MaxWeight,
                    Model = droneToList.Model,
                    DroneLocation = droneToList.DroneLocation,
                    Status = droneToList.Status,
                    Parcel = null
                };
            }
        }
        /*
        private Customer convertor3(CustomerToList customer)
        {
            throw new NotNeedToArrivedException("you shouldn't arrived here");
        }*/
    }
}