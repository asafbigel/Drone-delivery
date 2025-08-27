'use client';

import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';
import L from 'leaflet';
import { Drone } from '../types/drone';

// Fix for default icon issue with Webpack
delete (L.Icon.Default.prototype as any)._getIconUrl;
L.Icon.Default.mergeOptions({
  iconRetinaUrl: 'leaflet/dist/images/marker-icon-2x.png',
  iconUrl: 'leaflet/dist/images/marker-icon.png',
  shadowUrl: 'leaflet/dist/images/marker-shadow.png',
});

interface MapProps {
  drones: Drone[];
}

const Map = ({ drones }: MapProps) => {
  const defaultPosition: [number, number] = [31.771959, 35.217018]; // Jerusalem coordinates

  return (
    <MapContainer center={defaultPosition} zoom={13} style={{ height: '100vh', width: '100%' }}>
      <TileLayer
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
      />
      {drones.map((drone) => (
        <Marker key={drone.id} position={[drone.latitude, drone.longitude]}>
          <Popup>
            Drone ID: {drone.id} <br />
            Status: {drone.status} <br />
            Battery: {drone.battery}%
          </Popup>
        </Marker>
      ))}
    </MapContainer>
  );
};

export default Map;