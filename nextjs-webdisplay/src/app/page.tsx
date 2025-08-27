'use client';

import { useEffect, useState } from 'react';
import dynamic from 'next/dynamic';
import 'leaflet/dist/leaflet.css';
import { fetchDrones } from '@/services/droneService';

// Dynamically import the map component to avoid SSR issues with Leaflet
const MapWithNoSSR = dynamic(() => import('@/components/Map'), {
  ssr: false
});

export default function Home() {
  const [drones, setDrones] = useState([]);

  useEffect(() => {
    const loadDrones = async () => {
      const droneData = await fetchDrones();
      setDrones(droneData);
    };

    loadDrones();
  }, []);

  return (
    <main className="min-h-screen">
      <MapWithNoSSR drones={drones} />
    </main>
  );
}