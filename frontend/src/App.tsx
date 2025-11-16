import "./App.css";
import { Outlet } from "react-router";
import Footer from "./components/Footer";
import Nav from "./components/Nav";

function App() {
  return (
    <>
        <Nav />
        <Outlet />
        {/* <Footer /> */}
    </>
  );
}

export default App;
