import { FC } from "react";
import { Routes, Route } from "react-router-dom";
import App from "../App";
import Home from "../components/Home";
import Login from "../components/Login";
import { Register } from "../components/Register";
import { ReportMap } from "../components/ReportMap";
import ReportComponent from "../components/ReportComponent";

export const AppRoutes: FC = () => {
  return (
    <Routes>
      <Route path={"/"} element={<App />}>
        <Route path={"/"} element={<Home />} />
        <Route path={"/map"} element={<ReportMap />} />
        <Route path={"/Report"} element={<ReportComponent />} />
        <Route path={"/Login"} element={<Login />} />
        <Route path={"/Register"} element={<Register />} />
        <Route path={"*"} element={<div>Not found</div>} />
      </Route>
    </Routes>
  );
};
