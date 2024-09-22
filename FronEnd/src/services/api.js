import axios from "axios";

const apiUser = axios.create({
  baseURL: "http://localhost:5201",
  timeout: 7000,
});

export default apiUser;
