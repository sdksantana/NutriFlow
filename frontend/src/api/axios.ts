import axios from 'axios';
import { toast } from 'react-toastify';

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token && config.headers) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

api.interceptors.response.use(
  (response) => response,
  (error) => {
    const status = error.response?.status;
    const requestUrl = error.config?.url;
    const isLoginRoute = requestUrl?.includes('/users/login');

    if (status === 401 && !isLoginRoute) {
      localStorage.removeItem('token');
      toast.error('Sessão expirada. Faça login novamente.');
      window.location.href = '/login';
      return;
    }

    const errors = error.response?.data?.errors;
    if (Array.isArray(errors)) {
      errors.forEach((e: any) => {
        if (e?.message) {
          toast.error(e.message);
        }
      });
    }

    return Promise.reject(error);
  }
);

export default api;
