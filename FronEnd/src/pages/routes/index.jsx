import { Route, Routes } from "react-router-dom";
import HomePage from "../HomePage";
import LoginPage from "../LoginPage";
import RegisterPage from "../RegisterPage";
import EncaminharEmail from "../EncaminharEmail";

function RoutePages() {
  return (
    <Routes>
      <Route path="/" element={<HomePage />} />
      <Route path="/login" element={<LoginPage />} />
      <Route path="/register" element={<RegisterPage />} />
      <Route path="/encaminharEmail" element={<EncaminharEmail />} />
    </Routes>
  );
}

export default RoutePages;
