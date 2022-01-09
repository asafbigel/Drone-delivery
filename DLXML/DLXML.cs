using System;
using System.Collections.Generic;
using System.Linq;
using DalApi;
using DL;
using DO;

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
  //      string customersPath = @"xml/CustomersXml.xml"; //XElement
        string customersPath = @"CustomersXml.xml"; //XMLSerializer

        string dronesPath = @"DronesXml.xml"; //XMLSerializer
        string baseStationsPath = @"BaseStationsXml.xml"; //XMLSerializer
        string parcelsPath = @"ParcelsXml.xml"; //XMLSerializer
        string droneChargesPath = @"DroneChargesXml.xml"; //XMLSerializer
        #endregion


        #region drone
        public Drone Find_drone(int id)
        {
           return XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath).Find(d => d.Id == id);
            /*
            List<Drone> Listdrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);

            Drone dro = Listdrones.FirstOrDefault(d => d.Id == id);
            if (dro.Id == default(int))
                return dro; //no need to Clone()
            else
                throw new BadDroneIdException(id, $"bad drone id: {id}");
            */
        }
        public void Add_drone(Drone drone)
        {
            List<Drone> Listdrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);
            Drone drone1 = Listdrones.Find(d => d.Id == drone.Id);
            if (drone1.Id != default(int))
                throw new BadDroneIdException(drone.Id, "Duplicate drone ID");

            Listdrones.Add(drone); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(Listdrones, dronesPath);

        }
        public IEnumerable<Drone> Get_all_drones()
        {
            List<Drone> Listdrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);

            return from drone in Listdrones
                   select drone; //no need to Clone()
        }
        public void UpdateDrone(Drone drone)
        {
            List<Drone> Listdrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);

            Drone? dro = Listdrones.Find(d => d.Id == drone.Id);
            if (dro != null)
            {
                Listdrones.Remove((Drone)dro);
                Listdrones.Add(drone); //no nee to Clone()
            }
            else
                throw new BadDroneIdException(drone.Id, $"bad drone id: {drone.Id}");

            XMLTools.SaveListToXMLSerializer(Listdrones, dronesPath);
        }
        public void DeleteDrone(int id)
        {
            List<Drone> Listdrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);

            Drone? dro = Listdrones.Find(p => p.Id == id);

            if (dro != null)
            {
                Listdrones.Remove((Drone)dro);
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
        public BaseStation Find_baseStation(int id)
        {
            List<BaseStation> ListBaseStations = XMLTools.LoadListFromXMLSerializer<BaseStation>(baseStationsPath);

            BaseStation bs = ListBaseStations.FirstOrDefault(d => d.Id == id);
            if (bs.Id != default(int))
                return bs; //no need to Clone()
            else
                throw new BadBaseStationIdException(id, $"bad baseStation id: {id}");
        }
        public void Add_base_station(BaseStation baseStation)
        {
            List<BaseStation> ListBaseStations = XMLTools.LoadListFromXMLSerializer<BaseStation>(baseStationsPath);
            BaseStation? bs = ListBaseStations.Find(b => b.Id == baseStation.Id);
            if (bs != null)
                throw new BadBaseStationIdException(baseStation.Id, "Duplicate baseStation ID");

            ListBaseStations.Add(baseStation); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListBaseStations, baseStationsPath);

        }
        public IEnumerable<BaseStation> Get_all_baseStations()
        {
            List<BaseStation> ListBaseStations = XMLTools.LoadListFromXMLSerializer<BaseStation>(baseStationsPath);

            return from baseStation in ListBaseStations
                   select baseStation; //no need to Clone()
        }
        public IEnumerable<BaseStation> Get_all_base_stations(Predicate<BaseStation> match)
        {
            List<BaseStation> ListBaseStations = XMLTools.LoadListFromXMLSerializer<BaseStation>(baseStationsPath);

            return from baseStation in ListBaseStations
                   where match(baseStation)
                   select baseStation; //no need to Clone()
        }
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
        public Parcel Find_parcel(int id)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);

            Parcel par = ListParcels.FirstOrDefault(p => p.Id == id);
            if (par.Id == default(int))
                return par; //no need to Clone()
            else
                throw new BadParcelIdException(id, $"bad parcel id: {id}");
        }
        public void Add_parcel(Parcel parcel)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            Parcel? par = ListParcels.Find(p => p.Id == parcel.Id);
            if (par != null)
                throw new BadParcelIdException(parcel.Id, "Duplicate parcel ID");

            ListParcels.Add(parcel); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListParcels, parcelsPath);

        }
        public IEnumerable<Parcel> Get_all_parcels()
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);

            return from parcel in ListParcels
                   select parcel; //no need to Clone()
        }
        public IEnumerable<Parcel> Get_all_parcels(Predicate<Parcel> match)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);

            return from parcel in ListParcels
                   where match(parcel)
                   select parcel; //no need to Clone()
        }
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

        #region drone charge

        public DroneCharge FindDroneCharge(int id)
        {
            List<DroneCharge> ListDroneCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargesPath);

            DroneCharge dch = ListDroneCharges.FirstOrDefault(dc => dc.DroneId == id);
            if (dch.DroneId != default(int))
                return dch; //no need to Clone()
            else
                throw new BadDroneChargeIdException(id, $"bad drone id: {id}");
        }
        public void Add_DroneCharge(DroneCharge droneCharge)
        {
            List<DroneCharge> ListDroneCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargesPath);
            DroneCharge dch = ListDroneCharges.Find(d => d.DroneId == droneCharge.DroneId);
            if (dch.DroneId != default(int))
                throw new BadDroneChargeIdException(droneCharge.DroneId, "This drone is yet in charge");

            ListDroneCharges.Add(droneCharge); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListDroneCharges, droneChargesPath);

        }
        public IEnumerable<DroneCharge> Get_all_DroneCharge()
        {
            List<DroneCharge> ListDroneCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(droneChargesPath);

            return from droneCharge in ListDroneCharges
                   select droneCharge; //no need to Clone()
        }
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

        public void send_drone_to_charge(DroneCharge droneCharge)
        {
            BaseStation baseStation = Find_baseStation(droneCharge.StationId);
            Drone drone = Find_drone(droneCharge.DroneId);
            baseStation.ChargeSlots--;
            Add_DroneCharge(droneCharge);
            UpdateBaseStation(baseStation);
            UpdateDrone(drone);
        }

        public void put_out_drone_from_charge(int my_drone_id)
        {
            DroneCharge droneCharge = FindDroneCharge(my_drone_id);
            int my_baseStation_id = droneCharge.StationId;
            BaseStation baseStation = Find_baseStation(my_baseStation_id);
            baseStation.ChargeSlots++;
            UpdateBaseStation(baseStation);
            DeleteDroneCharge(droneCharge.DroneId);
        }


        #region need to do

        public int GetAndUpdateRunNumber()
        {
            Random rand = new Random();
            return rand.Next(10000, 99999);
        }

        public double[] ElectricityUse()
        {
            double[] a = { 5, 10, 15, 20, 30 };
            return a;
        }

        #endregion
    }
}