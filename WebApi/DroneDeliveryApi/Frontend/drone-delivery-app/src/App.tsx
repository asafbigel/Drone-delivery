import React, { useEffect, useState } from 'react';
import './App.css';
import MapComponent from './components/MapComponent';
import DroneList from './components/DroneList';
import BaseStationList from './components/BaseStationList';
import CustomerList from './components/CustomerList';
import ParcelList from './components/ParcelList';

interface Location {
  longitude: number;
  latitude: number;
}

interface DroneToList {
  id: number;
  model: string;
  maxWeight: number;
  battery: number;
  status: number;
  droneLocation: Location;
  numOfParcel: number;
}

interface BaseStationToList {
  id: number;
  name: string;
  numOfFreeSlots: number;
  numOfBusySlots: number;
  baseStationLocation: Location;
}

interface CustomerToList {
  id: number;
  name: string;
  phone: string;
  numOfParcelsSentAndArrived: number;
  numOfParcelsSentAndNotArrived: number;
  numOfParcelsGot: number;
  numOfParcelsToGet: number;
  customerLocation: Location;
}

interface ParcelToList {
  id: number;
  senderName: string;
  getterName: string;
  weight: number;
  priority: number;
  status: number;
  senderLocation: Location;
  getterLocation: Location;
}

function App() {
  const [drones, setDrones] = useState<DroneToList[]>([]);
  const [baseStations, setBaseStations] = useState<BaseStationToList[]>([]);
  const [customers, setCustomers] = useState<CustomerToList[]>([]);
  const [parcels, setParcels] = useState<ParcelToList[]>([]);
  const [error, setError] = useState<string | null>(null);

  // State for visibility filters
  const [showDrones, setShowDrones] = useState(true);
  const [showParcels, setShowParcels] = useState(true);
  const [showCustomers, setShowCustomers] = useState(true);
  const [showBaseStations, setShowBaseStations] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [dronesRes, baseStationsRes, customersRes, parcelsRes] = await Promise.all([
          fetch('/Drone'),
          fetch('/BaseStation'),
          fetch('/Customer'),
          fetch('/Parcel'),
        ]);

        if (!dronesRes.ok) throw new Error(`HTTP error! status: ${dronesRes.status} for Drones`);
        if (!baseStationsRes.ok) throw new Error(`HTTP error! status: ${baseStationsRes.status} for BaseStations`);
        if (!customersRes.ok) throw new Error(`HTTP error! status: ${customersRes.status} for Customers`);
        if (!parcelsRes.ok) throw new Error(`HTTP error! status: ${parcelsRes.status} for Parcels`);

        const dronesData: DroneToList[] = await dronesRes.json();
        const baseStationsData: BaseStationToList[] = await baseStationsRes.json();
        const customersData: CustomerToList[] = await customersRes.json();
        const parcelsData: ParcelToList[] = await parcelsRes.json();

        setDrones(dronesData);
        setBaseStations(baseStationsData);
        setCustomers(customersData);
        setParcels(parcelsData);
      } catch (error: any) {
        setError(error.message);
        console.error("Failed to fetch data:", error);
      }
    };

    fetchData();
  }, []);

  if (error) {
    return <div className="App">Error: {error}</div>;
  }

  return (
    <div className="App">
      <h1>Drone Delivery Dashboard</h1>

      <div className="filters">
        <label>
          <input type="checkbox" checked={showDrones} onChange={() => setShowDrones(!showDrones)} />
          Show Drones
        </label>
        <label>
          <input type="checkbox" checked={showParcels} onChange={() => setShowParcels(!showParcels)} />
          Show Parcels
        </label>
        <label>
          <input type="checkbox" checked={showCustomers} onChange={() => setShowCustomers(!showCustomers)} />
          Show Customers
        </label>
        <label>
          <input type="checkbox" checked={showBaseStations} onChange={() => setShowBaseStations(!showBaseStations)} />
          Show Base Stations
        </label>
      </div>

      <MapComponent
        drones={showDrones ? drones : []}
        baseStations={showBaseStations ? baseStations : []}
        customers={showCustomers ? customers : []}
        parcels={showParcels ? parcels : []}
        showDrones={showDrones}
        showBaseStations={showBaseStations}
        showCustomers={showCustomers}
        showParcels={showParcels}
      />
      <DroneList drones={drones} />
      <BaseStationList baseStations={baseStations} />
      <CustomerList customers={customers} />
      <ParcelList parcels={parcels} />
    </div>
  );
}

export default App;
