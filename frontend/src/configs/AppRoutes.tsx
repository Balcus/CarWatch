import { FC } from "react";
import { Routes, Route } from "react-router-dom";
import App from "../App";
import { Home } from "../components/Home";

export const AppRoutes: FC = () => {
  return (
    <Routes>
      <Route path={"/"} element={<App />}>
        <Route path={"/"} element={<Home />} />
        {/* <Route path={"/page1"} element={<Page1 />} />
        <Route path={"/page2"} element={<Page2 />} /> */}
        <Route path={"*"} element={<div>Not found</div>} />
      </Route>
    </Routes>
  );
};