export interface Drone {
  id: string;
  latitude: number;
  longitude: number;
  status: 'available' | 'flying' | 'charging';
  battery: number;
  lastUpdate: string;
}