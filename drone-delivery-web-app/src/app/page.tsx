import dynamic from 'next/dynamic';
import { useState, useEffect } from 'react';
import { fetchDrones } from '../services/droneService';
import { Drone } from '../types/drone';

const Map = dynamic(() => import('../components/Map'), { ssr: false });

export default function Home() {
  const [drones, setDrones] = useState<Drone[]>([]);

  useEffect(() => {
    const getDrones = async () => {
      const data = await fetchDrones();
      setDrones(data);
    };

    getDrones();
    const interval = setInterval(getDrones, 5000); // Poll every 5 seconds

    return () => clearInterval(interval);
  }, []);

  return (
    <main style={{ height: '100vh', width: '100vw' }}>
      <Map drones={drones} />
    </main>
  );
}