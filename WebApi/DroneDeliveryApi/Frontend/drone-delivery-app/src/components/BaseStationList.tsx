import React from 'react';

interface Location {
  longitude: number;
  latitude: number;
}

interface BaseStationToList {
  id: number;
  name: string;
  numOfFreeSlots: number;
  numOfBusySlots: number;
  baseStationLocation: Location;
}

interface BaseStationListProps {
  baseStations: BaseStationToList[];
}

const BaseStationList: React.FC<BaseStationListProps> = ({ baseStations }) => {
  return (
    <div>
      <h2>Base Stations</h2>
      <ul>
        {baseStations.map((bs) => (
          <li key={bs.id}>
            ID: {bs.id}, Name: {bs.name}, Free Slots: {bs.numOfFreeSlots}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default BaseStationList;
