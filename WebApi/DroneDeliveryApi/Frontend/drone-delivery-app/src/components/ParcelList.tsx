import React from 'react';

interface Location {
  longitude: number;
  latitude: number;
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

interface ParcelListProps {
  parcels: ParcelToList[];
}

const ParcelList: React.FC<ParcelListProps> = ({ parcels }) => {
  return (
    <div>
      <h2>Parcels</h2>
      <ul>
        {parcels.map((parcel) => (
          <li key={parcel.id}>
            ID: {parcel.id}, Sender: {parcel.senderName}, Getter: {parcel.getterName}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default ParcelList;
