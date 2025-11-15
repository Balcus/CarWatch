import React from "react";
import { Link, useNavigate } from "react-router-dom";
import "./Header.css";
import Button from "@mui/material/Button";

const Header: React.FC = () => {
  const navigate = useNavigate();

  return (
    <header className="header">
      <h2 className="logo">🚘 Car Watch</h2>

      <div className="navRow">
        <nav className="navLeft">
          <Button variant="text">Te</Button>
          <Button variant="contained">Contained</Button>
          <Button variant="outlined">Outlined</Button>
        </nav>

        <div className="rightSection">
          <span className="navRight">🚗</span>
          <Button variant="contained" onClick={() => navigate("/register")}>
            Register
          </Button>

          <Button variant="contained" onClick={() => navigate("/login")}>
            Login
          </Button>
        </div>
      </div>
    </header>
  );
};

export default Header;
