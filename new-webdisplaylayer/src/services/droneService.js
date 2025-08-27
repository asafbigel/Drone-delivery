import axios from 'axios';

const API_BASE_URL = 'http://localhost:5000'; // Replace with your backend URL

const fetchDrones = async () => {
  try {
    const response = await axios.get(`${API_BASE_URL}/drones`);
    return response.data;
  } catch (error) {
    console.error('Error fetching drones:', error);
    return [];
  }
};

export { fetchDrones };