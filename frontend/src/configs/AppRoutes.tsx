import { FC } from "react";
import { Routes, Route } from "react-router-dom";
import App from "../App";
import Home from "../components/Home";
import Register from "../components/Register";

export const AppRoutes: FC = () => {
  return (
    <Routes>
      <Route path={"/"} element={<App />}>
        <Route path={"/"} element={<Home />} />
        <Route path={"/register"} element={<Register />} />
        <Route path={"*"} element={<div>Not found</div>} />
      </Route>
    </Routes>
  );
};
