import React from "react";
import { Box, Heading, Text, Button, VStack, Image } from "@chakra-ui/react";
import Logo from "../../assets/logo-atak.png";
import { Link } from "react-router-dom";

function HomePage() {
  return (
    <Box bg="gray.100" minH="100vh">
      <Box as="ul" bg="#050f14" color="white" h={14}>
        <Image src={Logo} transform={"scale(0.5)"} />
      </Box>
      <VStack spacing={8} p={6} marginTop={"70px"}>
        <Heading as="h1" size="2xl" mt={8}>
          Bem-vindo ao Gerador de dados
        </Heading>
        <Text fontSize="xl">
          Esse é um projete que gera dados ficticios de clientes em um arquivo
          xlsx anexa e envia para um email pré definido.
        </Text>
        <Box display={"flex"} gap={"30px"}>
          <Link to="/login">
            <Button colorScheme="teal" size="lg" width={"130px"}>
              Login
            </Button>
          </Link>
          <Link to="/register">
            <Button colorScheme="teal" size="lg" width={"130px"}>
              Cadastre-se
            </Button>
          </Link>
        </Box>
      </VStack>
    </Box>
  );
}

export default HomePage;
