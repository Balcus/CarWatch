import { FC } from "react";
import { Routes, Route } from "react-router-dom";
import App from "../App";
import Home from "../components/Home";
import Login from "../components/Login";
import Report from "../components/Report";
import Map from "../components/Map";
import { Register } from "../components/Register";

export const AppRoutes: FC = () => {
  return (
    <Routes>
      <Route path={"/"} element={<App />}>
        <Route path={"/"} element={<Home />} />
        <Route path={"/Map"} element={<Map />} />
        <Route path={"/Report"} element={<Report />} />
        <Route path={"/Login"} element={<Login />} />
        <Route path={"/Register"} element={<Register />} />
        <Route path={"*"} element={<div>Not found</div>} />
      </Route>
    </Routes>
  );
};
