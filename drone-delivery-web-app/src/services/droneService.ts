import axios from 'axios';
import { Drone } from '../types/drone';

const API_URL = '/api/drones'; // Assuming your backend serves drone data from this endpoint

export const fetchDrones = async (): Promise<Drone[]> => {
  try {
    const response = await axios.get<Drone[]>(API_URL);
    return response.data;
  } catch (error) {
    console.error('Error fetching drones:', error);
    return [];
  }
};