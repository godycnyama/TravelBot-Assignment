import axios from 'axios';

const axiosInstance = axios.create({ baseURL: ' https://localhost:7242/' });
export default axiosInstance;