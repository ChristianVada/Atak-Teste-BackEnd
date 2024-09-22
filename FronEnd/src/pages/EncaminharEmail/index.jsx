import {
  Box,
  Button,
  FormControl,
  FormLabel,
  Input,
  Text,
  Textarea,
} from "@chakra-ui/react";
import { useContext, useState } from "react";
import { UserContext } from "../../context/userContext";

function EncaminharEmail() {
  const { EnviarEmailComExcel } = useContext(UserContext);
  const [inputValue, setInputValue] = useState("");

  const HandleSubmit = () => {
    EnviarEmailComExcel(inputValue);
  };

  const HandleChange = (evento) => {
    setInputValue(evento.target.value);
  };

  return (
    <Box maxW="md" mx="auto" mt={10} p={5} borderWidth={1} borderRadius="lg">
      <Text mb={4}>Os dados gerados serão encaminhados para o email xxx.</Text>
      <FormLabel>Quantidade de estar entre 10 e 1000</FormLabel>
      <Input
        value={inputValue}
        placeholder="Defina a quantidade de cliente fictícios a ser gerado"
        onChange={HandleChange}
      ></Input>
      <Button colorScheme="blue" mt={4} onClick={HandleSubmit}>
        Confirmar
      </Button>
    </Box>
  );
}

export default EncaminharEmail;
