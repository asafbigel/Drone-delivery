'use client';

import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet';
import L from 'leaflet';

// Fix for default marker icon not displaying
const icon = L.icon({
  iconUrl: '/marker-icon.png',
  shadowUrl: '/marker-shadow.png',
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34],
  shadowSize: [41, 41]
});

interface DroneData {
  id: number;
  latitude: number;
  longitude: number;
  status: string;
}

interface MapProps {
  drones: DroneData[];
}

const Map = ({ drones }: MapProps) => {
  return (
    <MapContainer 
      center={[31.0461, 34.8516]} // Center on Israel
      zoom={8}
      style={{ height: '100vh', width: '100%' }}
    >
      <TileLayer
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
      />
      {drones.map((drone) => (
        <Marker 
          key={drone.id} 
          position={[drone.latitude, drone.longitude]}
          icon={icon}
        >
          <Popup>
            <div>
              <h3>Drone ID: {drone.id}</h3>
              <p>Status: {drone.status}</p>
            </div>
          </Popup>
        </Marker>
      ))}
    </MapContainer>
  );
};

export default Map;