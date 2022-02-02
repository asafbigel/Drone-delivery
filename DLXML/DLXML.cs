using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DalApi;
using DL;
using DO;
using System.Runtime.CompilerServices;

namespace Dal
{
    sealed class DLXML : IDal
    {

        #region singelton
        static readonly IDal instance = new DLXML();
        static DLXML() { }// static ctor to ensure instance init is done just before first usage
        DLXML() { } // default => private
        public static IDal Instance { get => instance; }// The public Instance property to use
        #endregion

        #region DS XML Files
        string customersPath = @"CustomersXml.xml"; //XElement
        string detailsPath = @"DataXml.xml"; //XElement

        string dronesPath = @"DronesXml.xml"; //XMLSerializer
        string baseStationsPath = @"BaseStationsXml.xml"; //XMLSerializer
        string parcelsPath = @"ParcelsXml.xml"; //XMLSerializer
        string droneChargesPath = @"DroneChargesXml.xml"; //XMLSerializer
        #endregion



        #region customer
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer Find_customer(int id)
        {
            XElement customersRootElem = XMLTools.LoadListFromXMLElement(customersPath);

            Customer? c = (from customer in customersRootElem.Elements()
                        where int.Parse(customer.Element("Id").Value) == id
                        select new Customer()
                        {
                            Id = int.Parse(customer.Element("Id").Value),
                            Lattitude = double.Parse(customer.Element("Lattitude").Value),
                            Longitude = double.Parse(customer.Element("Longitude").Value),
                            Phone = customer.Element("Phone").Value,
                            Password = customer.Element("Password").Value,
                            Name = customer.Element("Name").Value
                        }
                        ).FirstOrDefault();

            if (c == null)
                throw new BadCustomerIdException(id, $"bad customer id: {id}");

            return (Customer)c;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> Get_all_customers()
        {
            XElement customersRootElem = XMLTools.LoadListFromXMLElement(customersPath);

            return from customer in customersRootElem.Elements()
                    select new Customer()
                    {
                        Id = int.Parse(customer.Element("Id").Value),
                        Lattitude = double.Parse(customer.Element("Lattitude").Value),
                        Longitude = double.Parse(customer.Element("Longitude").Value),
                        Phone = customer.Element("Phone").Value,
                        Password = customer.Element("Password").Value,
                        Name = customer.Element("Name").Value
                    };
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetAllCustomersBy(Predicate<Customer> predicate)
        {
            XElement customersRootElem = XMLTools.LoadListFromXMLElement(customersPath);

            return from cus in customersRootElem.Elements()
                   let customer = new Customer()
                   {
                       Id = int.Parse(cus.Element("Id").Value),
                       Lattitude = double.Parse(cus.Element("Lattitude").Value),
                       Longitude = double.Parse(cus.Element("Longitude").Value),
                       Phone = cus.Element("Phone").Value,
                       Password = cus.Element("Password").Value,
                       Name = cus.Element("Name").Value
                   }
                   where predicate(customer)
                   select customer;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Add_customer(Customer customer)
        {
            XElement customersRootElem = XMLTools.LoadListFromXMLElement(customersPath);

            XElement customer1 = (from c in customersRootElem.Elements()
                             where int.Parse(c.Element("Id").Value) == customer.Id
                             select c).FirstOrDefault();

            if (customer1 != null)
                throw new BadCustomerIdException(customer.Id, "Duplicate customer ID");

            XElement customerElem = new XElement("Customer",
                                    new XElement("Id", customer.Id),
                                    new XElement("Phone", customer.Phone),
                                    new XElement("Password", customer.Password),
                                    new XElement("Name", customer.Name),
                                    new XElement("Longitude", customer.Longitude),
                                    new XElement("Lattitude", customer.Lattitude));

            customersRootElem.Add(customerElem);

            XMLTools.SaveListToXMLElement(customersRootElem, customersPath);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(int id)
        {
            XElement customersRootElem = XMLTools.LoadListFromXMLElement(customersPath);

            XElement cus = (from p in customersRootElem.Elements()
                            where int.Parse(p.Element("Id").Value) == id
                            select p).FirstOrDefault();

            if (cus != null)
            {
                cus.Remove(); //<==>   Remove per from customersRootElem

                XMLTools.SaveListToXMLElement(customersRootElem, customersPath);
            }
            else
                throw new DO.BadCustomerIdException(id, $"bad customer id: {id}");
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(DO.Customer customer)
        {
            XElement customersRootElem = XMLTools.LoadListFromXMLElement(customersPath);

            XElement cus = (from c in customersRootElem.Elements()
                            where int.Parse(c.Element("Id").Value) == customer.Id
                            select c).FirstOrDefault();

            if (cus != null)
            {
                cus.Element("Id").Value = customer.Id.ToString();
                cus.Element("Name").Value = customer.Name;
                cus.Element("Phone").Value = customer.Phone;
                cus.Element("Password").Value = customer.Password;
                cus.Element("Longitude").Value = customer.Longitude.ToString();
                cus.Element("Lattitude").Value = customer.Lattitude.ToString();

                XMLTools.SaveListToXMLElement(customersRootElem, customersPath);
            }
            else
                throw new DO.BadCustomerIdException(customer.Id, $"bad customer id: {customer.Id}");
        }
        #endregion Customer



        #region drone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone Find_drone(int id)
        {
           return Get_all_drones().ToList().Find(d => d.Id == id);
          // return XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath).Find(d => d.Id == id);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Add_drone(Drone drone)
        {
            List<Drone> Listdrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);
            Drone drone1 = Listdrones.Find(d => d.Id == drone.Id);
            if (drone1.Id != default(int))
                throw new BadDroneIdException(drone.Id, "Duplicate drone ID");

            Listdrones.Add(drone); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(Listdrones, dronesPath);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> Get_all_drones()
        {
            List<Drone> Listdrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);

            return from drone in Listdrones
                   select drone; //no need to Clone()
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(Drone newDrone)
        {
            List<Drone> Listdrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);

            Drone oldDrone = Listdrones.Find(d => d.Id == newDrone.Id);
            if (oldDrone.Id != default(int))
            {
                Listdrones.Remove(oldDrone);
                Listdrones.Add(newDrone); //no nee to Clone()
            }
            else
                throw new BadDroneIdException(newDrone.Id, $"bad drone id: {newDrone.Id}");

            XMLTools.SaveListToXMLSerializer(Listdrones, dronesPath);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(int id)
        {
            List<Drone> Listdrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);

            Drone drone = Listdrones.Find(p => p.Id == id);

            if (drone.Id != default(int))
            {
                Listdrones.Remove(drone);
            }
            else
                throw new BadDroneIdException(id, $"bad drone id: {id}");

            XMLTools.SaveListToXMLSerializer(Listdrones, dronesPath);
        }
        /*
      public IEnumerable<object> GetdroneFields(Func<int, string, object> generate)
      {
          List<Drone> Listdrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);

          return from drone in Listdrones
                 select generate(drone.Id, Getdrone(drone.Id).Name);
      }


      public IEnumerable<object> GetdroneListWithSelectedFields(Func<Drone, object> generate)
      {
          List<Drone> Listdrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);

          return from drone in Listdrones
                 select generate(drone);
      }

      public void Updatedrone(int id, Action<Drone> update)
      {
          throw new NotImplementedException();
      }
      */
        #endregion drone

        #region base station
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BaseStation Find_baseStation(int id)
        {
            List<BaseStation> ListBaseStations = XMLTools.LoadListFromXMLSerializer<BaseStation>(baseStationsPath);

            BaseStation bs = ListBaseStations.FirstOrDefault(d => d.Id == id);
            if (bs.Id != default(int))
                return bs; //no need to Clone()
            else
                throw new BadBaseStationIdException("not found baseStation id: "+id);
                //throw new BadBaseStationIdException(id, $"bad baseStation id: {id}");
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Add_base_station(BaseStation baseStation)
        {
            List<BaseStation> ListBaseStations = XMLTools.LoadListFromXMLSerializer<BaseStation>(baseStationsPath);
            BaseStation bs = ListBaseStations.Find(b => b.Id == baseStation.Id);
            if (bs.Id != default(int))
                throw new BadBaseStationIdException(baseStation.Id, "Duplicate baseStation ID");

            ListBaseStations.Add(baseStation); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListBaseStations, baseStationsPath);

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStation> Get_all_baseStations()
        {
            List<BaseStation> ListBaseStations = XMLTools.LoadListFromXMLSerializer<BaseStation>(baseStationsPath);

            return from baseStation in ListBaseStations
                   select baseStation; //no need to Clone()
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStation> Get_all_base_stations(Predicate<BaseStation> match)
        {
            List<BaseStation> ListBaseStations = XMLTools.LoadListFromXMLSerializer<BaseStation>(baseStationsPath);

            return from baseStation in ListBaseStations
                   where match(baseStation)
                   select baseStation; //no need to Clone()
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateBaseStation(BaseStation baseStation)
        {
            List<BaseStation> ListBaseStation = XMLTools.LoadListFromXMLSerializer<BaseStation>(baseStationsPath);

            BaseStation? bs = ListBaseStation.Find(b => b.Id == baseStation.Id);
            if (bs != null)
            {
                ListBaseStation.Remove((BaseStation)bs);
                ListBaseStation.Add(baseStation); //no nee to Clone()
            }
            else
                throw new BadBaseStationIdException(baseStation.Id, $"bad baseStation id: {baseStation.Id}");

            XMLTools.SaveListToXMLSerializer(ListBaseStation, baseStationsPath);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteBaseStation(int id)
        {
            List<BaseStation> ListBaseStations = XMLTools.LoadListFromXMLSerializer<BaseStation>(baseStationsPath);

            BaseStation? bs = ListBaseStations.Find(b => b.Id == id);

            if (bs != null)
            {
                ListBaseStations.Remove((BaseStation)bs);
            }
            else
                throw new BadBaseStationIdException(id, $"bad baseStation id: {id}");

            XMLTools.SaveListToXMLSerializer(ListBaseStations, baseStationsPath);
        }
        #endregion

        #region parcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel Find_parcel(int id)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);

            Parcel par = ListParcels.FirstOrDefault(p => p.Id == id);
            if (par.Id != default(int))
                return par; //no need to Clone()
            else
                throw new BadParcelIdException(id, $"bad parcel id: {id}");
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Add_parcel(Parcel parcel)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            Parcel par = ListParcels.Find(p => p.Id == parcel.Id);
            if (par.Id != default(int))
                throw new BadParcelIdException(parcel.Id, "Duplicate parcel ID");

            ListParcels.Add(parcel); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListParcels, parcelsPath);

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> Get_all_parcels()
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);

            return from parcel in ListParcels
                   select parcel; //no need to Clone()
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> Get_all_parcels(Predicate<Parcel> match)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);

            return from parcel in ListParcels
                   where match(parcel)
                   select parcel; //no need to Clone()
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcel(Parcel parcel)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);

            Parcel? par = ListParcels.Find(p => p.Id == parcel.Id);
            if (par != null)
            {
                ListParcels.Remove((Parcel)par);
                ListParcels.Add(parcel); //no nee to Clone()
            }
            else
                throw new BadParcelIdException(parcel.Id, $"bad parcel id: {parcel.Id}");

            XMLTools.SaveListToXMLSerializer(ListParcels, parcelsPath);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int id)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);

            Parcel? par = ListParcels.Find(p => p.Id == id);

            if (par != null)
            {
                ListParcels.Remove((Parcel)par);
            }
            else
                throw new BadParcelIdException(id, $"bad parcel id: {id}");

            XMLTools.SaveListToXMLSerializer(ListParcels, parcelsPath);
        }
        #endregion


        /*
        #region customer
        public Customer Find_customer(int id)
        {
            List<Customer> ListCustomers = XMLTools.LoadListFromXMLSerializer<Customer>(customersPath);

            Customer cus = ListCustomers.FirstOrDefault(c => c.Id == id);
            if (cus.Id != default(int))
                return cus; //no need to Clone()
            else
                throw new BadCustomerIdException(id, $"bad customer id: {id}");
        }
        public void Add_customer(Customer customer)
        {
            List<Customer> ListCustomers = XMLTools.LoadListFromXMLSerializer<Customer>(customersPath);
            Customer? cus = ListCustomers.Find(c => c.Id == customer.Id);
            if (cus != null)
                throw new BadCustomerIdException(customer.Id, "Duplicate customer ID");

            ListCustomers.Add(customer); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListCustomers, customersPath);

        }
        public IEnumerable<Customer> Get_all_customers()
        {
            List<Customer> ListCustomers = XMLTools.LoadListFromXMLSerializer<Customer>(customersPath);

            return from customer in ListCustomers
                   select customer; //no need to Clone()
        }
        public void UpdateCustomer(Customer customer)
        {
            List<Customer> ListCustomers = XMLTools.LoadListFromXMLSerializer<Customer>(customersPath);

            Customer? cus = ListCustomers.Find(c => c.Id == customer.Id);
            if (cus != null)
            {
                ListCustomers.Remove((Customer)cus);
                ListCustomers.Add(customer); //no nee to Clone()
            }
            else
                throw new BadCustomerIdException(customer.Id, $"bad customer id: {customer.Id}");

            XMLTools.SaveListToXMLSerializer(ListCustomers, customersPath);
        }
        public void DeleteCustomer(int id)
        {
            List<Customer> ListCustomers = XMLTools.LoadListFromXMLSerializer<Customer>(customersPath);

            Customer? cus = ListCustomers.Find(c => c.Id == id);

            if (cus != null)
            {
                ListCustomers.Remove((Customer)cus);
            }
            else
                throw new BadCustomerIdException(id, $"bad customer id: {id}");

            XMLTools.SaveListToXMLSerializer(ListCustomers, customersPath);
        }
        #endregion
        */
        #region drone charge
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneCharge FindDroneCharge(int id)
        {
            List<DroneCharge> ListDroneCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargesPath);

            DroneCharge dch = ListDroneCharges.FirstOrDefault(dc => dc.DroneId == id);
            if (dch.DroneId != default(int))
                return dch; //no need to Clone()
            else
                throw new BadDroneChargeIdException(id, $"bad drone id: {id}");
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Add_DroneCharge(DroneCharge droneCharge)
        {
            List<DroneCharge> ListDroneCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargesPath);
            DroneCharge dch = ListDroneCharges.Find(d => d.DroneId == droneCharge.DroneId);
            if (dch.DroneId != default(int))
                throw new BadDroneChargeIdException(droneCharge.DroneId, "This drone is yet in charge");

            ListDroneCharges.Add(droneCharge); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListDroneCharges, droneChargesPath);

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> Get_all_DroneCharge()
        {
            List<DroneCharge> ListDroneCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargesPath);

            return from droneCharge in ListDroneCharges
                   select droneCharge; //no need to Clone()
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDroneCharge(DroneCharge droneCharge)
        {
            List<DroneCharge> ListDroneCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargesPath);

            DroneCharge? dch = ListDroneCharges.Find(d => d.DroneId == droneCharge.DroneId);
            if (dch != null)
            {
                ListDroneCharges.Remove((DroneCharge)dch);
                ListDroneCharges.Add(droneCharge); //no nee to Clone()
            }
            else
                throw new BadDroneChargeIdException(droneCharge.DroneId, $"bad drone id: {droneCharge.DroneId}");

            XMLTools.SaveListToXMLSerializer(ListDroneCharges, droneChargesPath);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDroneCharge(int id)
        {
            List<DroneCharge> ListDroneCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargesPath);

            DroneCharge? dch = ListDroneCharges.Find(c => c.DroneId == id);

            if (dch != null)
            {
                ListDroneCharges.Remove((DroneCharge)dch);
            }
            else
                throw new BadDroneChargeIdException(id, $"bad drone id: {id}");

            XMLTools.SaveListToXMLSerializer(ListDroneCharges, droneChargesPath);
        }
        #endregion

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void send_drone_to_charge(DroneCharge droneCharge)
        {
            BaseStation baseStation = Find_baseStation(droneCharge.StationId);
            Drone drone = Find_drone(droneCharge.DroneId);
            baseStation.ChargeSlots--;
            Add_DroneCharge(droneCharge);
            UpdateBaseStation(baseStation);
            UpdateDrone(drone);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void put_out_drone_from_charge(int my_drone_id)
        {
            DroneCharge droneCharge = FindDroneCharge(my_drone_id);
            int my_baseStation_id = droneCharge.StationId;
            BaseStation baseStation = Find_baseStation(my_baseStation_id);
            baseStation.ChargeSlots++;
            UpdateBaseStation(baseStation);
            DeleteDroneCharge(droneCharge.DroneId);
        }


        #region data
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int GetAndUpdateRunNumber()
        {
            XElement details = XMLTools.LoadListFromXMLElement(detailsPath);
            int x = int.Parse(details.Element("RunNumber").Element("NewParcelId").Value);
            details.Element("RunNumber").Element("NewParcelId").Value = (x + 1).ToString();
            XMLTools.SaveListToXMLElement(details, detailsPath);
            return x;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public double[] ElectricityUse()
        {
                XElement details = XMLTools.LoadListFromXMLElement(detailsPath);
                var x = from data in details.Element("BatteryUsages").Elements()
                        select double.Parse(data.Value);
                return x.ToArray();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public string[] GetMenager()
        {
            string[] menager = new string[2];
            XElement details = XMLTools.LoadListFromXMLElement(detailsPath);
            menager[0] = details.Element("Menager").Element("UserName").Value;
            menager[1] = details.Element("Menager").Element("Password").Value;
            return menager;
        }
        #endregion
    }
}