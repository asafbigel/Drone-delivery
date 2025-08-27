import React from 'react';

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

interface DroneListProps {
  drones: DroneToList[];
}

const DroneList: React.FC<DroneListProps> = ({ drones }) => {
  return (
    <div>
      <h2>Drones</h2>
      <ul>
        {drones.map((drone) => (
          <li key={drone.id}>
            ID: {drone.id}, Model: {drone.model}, Battery: {drone.battery}%
          </li>
        ))}
      </ul>
    </div>
  );
};

export default DroneList;
