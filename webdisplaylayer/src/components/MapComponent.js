import React, { useEffect, useState } from 'react';
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';
import L from 'leaflet';
import { fetchDrones } from '../services/droneService';

// Fix for default marker icon not displaying
import markerIcon from 'leaflet/dist/images/marker-icon.png';
import markerShadow from 'leaflet/dist/images/marker-shadow.png';

const DefaultIcon = L.icon({
  iconUrl: markerIcon,
  shadowUrl: markerShadow,
  iconAnchor: [12, 41],
});
L.Marker.prototype.options.icon = DefaultIcon;

const MapComponent = () => {
  const [drones, setDrones] = useState([]);

  useEffect(() => {
    const loadDrones = async () => {
      const droneData = await fetchDrones();
      setDrones(droneData);
    };

    loadDrones();
  }, []);

  return (
    <MapContainer center={[51.505, -0.09]} zoom={13} style={{ height: '100vh', width: '100%' }}>
      <TileLayer
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        attribution="&copy; <a href='https://www.openstreetmap.org/copyright'>OpenStreetMap</a> contributors"
      />
      {drones.map((drone) => (
        <Marker key={drone.id} position={[drone.latitude, drone.longitude]}>
          <Popup>
            Drone ID: {drone.id} <br />
            Status: {drone.status}
          </Popup>
        </Marker>
      ))}
    </MapContainer>
  );
};

export default MapComponent;