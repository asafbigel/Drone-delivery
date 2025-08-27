import React from 'react';
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';
import L from 'leaflet';

// Fix for default icon issue with Webpack
L.Icon.Default.mergeOptions({
  iconRetinaUrl: require('leaflet/dist/images/marker-icon-2x.png'),
  iconUrl: require('leaflet/dist/images/marker-icon.png'),
  shadowUrl: require('leaflet/dist/images/marker-shadow.png'),
});

// Custom icons
const droneIcon = new L.Icon({
  iconUrl: require('./../assets/drone-icon.png'), // Corrected path
  iconSize: [32, 32],
  iconAnchor: [16, 32],
  popupAnchor: [0, -32],
});

const parcelIcon = new L.Icon({
  iconUrl: require('./../assets/parcel-icon.png'), // Corrected path
  iconSize: [32, 32],
  iconAnchor: [16, 32],
  popupAnchor: [0, -32],
});

const baseStationIcon = new L.Icon({
  iconUrl: require('./../assets/base-station-icon.png'), // Placeholder: Replace with your base station icon path
  iconSize: [32, 32],
  iconAnchor: [16, 32],
  popupAnchor: [0, -32],
});

const customerIcon = new L.Icon({
  iconUrl: require('./../assets/customer-icon.png'), // Placeholder: Replace with your customer icon path
  iconSize: [32, 32],
  iconAnchor: [16, 32],
  popupAnchor: [0, -32],
});

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

interface MapComponentProps {
  drones: DroneToList[];
  baseStations: BaseStationToList[];
  customers: CustomerToList[];
  parcels: ParcelToList[];
  showDrones: boolean;
  showBaseStations: boolean;
  showCustomers: boolean;
  showParcels: boolean;
}

const MapComponent: React.FC<MapComponentProps> = ({ drones, baseStations, customers, parcels, showDrones, showBaseStations, showCustomers, showParcels }) => {
  const defaultCenter: [number, number] = [31.771959, 35.217018]; // Center of Jerusalem, for example

  return (
    <MapContainer center={defaultCenter} zoom={10} style={{ height: '600px', width: '100%' }}>
      <TileLayer
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
      />

      {showDrones && drones.map((drone) => (
        drone.droneLocation && (
          <Marker key={drone.id} position={[drone.droneLocation.latitude, drone.droneLocation.longitude]} icon={droneIcon}>
            <Popup>
              Drone ID: {drone.id}<br />
              Model: {drone.model}<br />
              Battery: {drone.battery}%
            </Popup>
          </Marker>
        )
      ))}

      {showBaseStations && baseStations.map((bs) => (
        bs.baseStationLocation && (
          <Marker key={bs.id} position={[bs.baseStationLocation.latitude, bs.baseStationLocation.longitude]} icon={baseStationIcon}>
            <Popup>
              Base Station ID: {bs.id}<br />
              Name: {bs.name}<br />
              Free Slots: {bs.numOfFreeSlots}
            </Popup>
          </Marker>
        )
      ))}

      {showCustomers && customers.map((customer) => (
        customer.customerLocation && (
          <Marker key={customer.id} position={[customer.customerLocation.latitude, customer.customerLocation.longitude]} icon={customerIcon}>
            <Popup>
              Customer ID: {customer.id}<br />
              Name: {customer.name}<br />
              Phone: {customer.phone}
            </Popup>
          </Marker>
        )
      ))}

      {showParcels && parcels.map((parcel) => (
        <React.Fragment key={parcel.id}>
          {parcel.senderLocation && (
            <Marker position={[parcel.senderLocation.latitude, parcel.senderLocation.longitude]} icon={parcelIcon}>
              <Popup>
                Parcel ID: {parcel.id}<br />
                Sender: {parcel.senderName}<br />
                Status: {parcel.status} (Sender)
              </Popup>
            </Marker>
          )}
          {parcel.getterLocation && (
            <Marker position={[parcel.getterLocation.latitude, parcel.getterLocation.longitude]} icon={parcelIcon}>
              <Popup>
                Parcel ID: {parcel.id}<br />
                Getter: {parcel.getterName}<br />
                Status: {parcel.status} (Getter)
              </Popup>
            </Marker>
          )}
        </React.Fragment>
      ))}
    </MapContainer>
  );
};

export default MapComponent;
