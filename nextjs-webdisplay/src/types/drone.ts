export interface DroneData {
  id: number;
  latitude: number;
  longitude: number;
  status: string;
  model?: string;
  batteryStatus?: number;
}