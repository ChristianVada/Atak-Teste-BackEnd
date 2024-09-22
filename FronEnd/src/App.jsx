import { useState } from "react";
import RoutePages from "./pages/routes";
import { CSSReset } from "@chakra-ui/react";

function App() {
  return (
    <>
      <CSSReset />
      <RoutePages />
    </>
  );
}

export default App;
