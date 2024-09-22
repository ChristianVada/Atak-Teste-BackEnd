import { createContext, useState } from "react";
import apiUser from "../services/api";
import { useNavigate } from "react-router-dom";

const UserContext = createContext();

const UserProvider = ({ children }) => {
  const [token, setToken] = useState("");
  const navigate = useNavigate();

  const fazerLogin = async (dataLogin) => {
    try {
      const response = await apiUser.post("/api/autenticacao/login", dataLogin);
      setToken(response.data.token);
      navigate("/encaminharEmail");
    } catch (error) {
      console.warn(error);
    }
  };

  const EnviarEmailComExcel = async (quantidade) => {
    try {
      await apiUser.post(
        `/api/excel/enviar-email?quantidade=${quantidade}`,
        null,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
    } catch (error) {
      console.warn(error);
    }
  };

  const CadastrarUsuario = async (dataRegister) => {
    try {
      await apiUser.post(`/api/autenticacao/registro`, dataRegister);
      navigate("/login");
    } catch (error) {
      console.warn(error);
    }
  };

  return (
    <UserContext.Provider
      value={{
        token,
        setToken,
        fazerLogin,
        EnviarEmailComExcel,
        CadastrarUsuario,
      }}
    >
      {children}
    </UserContext.Provider>
  );
};

export { UserContext, UserProvider };
