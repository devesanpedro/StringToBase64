import axios from "axios";

const API_ROOT = `https://localhost:7252/`;
// const API_ROOT = `${process.env.REACT_APP_API_ROOT}`;

const handleErrors = () => {};

const handleResponse = (res) => {
  return res && res.data;
};

const beforeRequest = async (config) => {
  return config;
};

const createApi = () => {
  let options = {
    baseURL: API_ROOT,
    timeout: 30000
  };

  const token = localStorage.getItem("token");
  if (token !== null && token.trim().length > 0)
    options.headers = { Authorization: `Bearer ${JSON.parse(token)}` };

  const api = axios.create(options);
  api.interceptors.request.use(beforeRequest);
  return api;
};

export const requests = {
  get: (url) =>
    createApi()
      .get(`${API_ROOT}${url}`)
      .then(handleResponse)
      .catch(handleErrors),
  post: (url, data) =>
    createApi().post(`${API_ROOT}${url}`, data).then(handleResponse),
  put: (url, data) =>
    createApi()
      .put(`${API_ROOT}${url}`, data)
      .then(handleResponse)
      .catch(handleErrors),
  delete: (url) =>
    createApi()
      .delete(`${API_ROOT}${url}`)
      .then(handleResponse)
      .catch(handleErrors),
  deleteWithData: (url, data) =>
    createApi()
      .delete(`${API_ROOT}${url}`, { data: data })
      .then(handleResponse)
      .catch(handleErrors),
  upload: (url, formData) => {
    let options = {
      baseURL: API_ROOT,
      timeout: 30000
    };

    const token = localStorage.getItem("token");
    if (token !== null && token.trim().length > 0)
      options.headers = { Authorization: `Bearer ${JSON.parse(token)}` };

    const api = axios.create(options);
    api.interceptors.request.use(beforeRequest);
    return api
      .post(url, formData, {
        headers: { "content-type": "multipart/form-data" }
      })
      .then(handleResponse)
      .catch(handleErrors);
  }
};
