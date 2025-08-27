import React from 'react';

interface Location {
  longitude: number;
  latitude: number;
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

interface CustomerListProps {
  customers: CustomerToList[];
}

const CustomerList: React.FC<CustomerListProps> = ({ customers }) => {
  return (
    <div>
      <h2>Customers</h2>
      <ul>
        {customers.map((customer) => (
          <li key={customer.id}>
            ID: {customer.id}, Name: {customer.name}, Phone: {customer.phone}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default CustomerList;
