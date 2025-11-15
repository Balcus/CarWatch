import React from "react";
import { useNavigate } from "react-router-dom";
import Button from "@mui/material/Button";
import "./Header.css";

const Header: React.FC = () => {
  const navigate = useNavigate();

  return (
    <header className="header">
      {/* Logo */}
      <Button className="logo-btn" onClick={() => navigate("/")}>
        🚘 CAR WATCH
      </Button>

      <div className="navRow">
        {/* Left Navigation */}
        <nav className="navLeft">
          <Button className="nav-btn" onClick={() => navigate("/")}>
            Home
          </Button>
          <Button className="nav-btn" onClick={() => navigate("/map")}>
            Map
          </Button>
          <Button className="nav-btn" onClick={() => navigate("/report")}>
            Report
          </Button>
        </nav>

        {/* Right Navigation */}
        <div className="rightSection">
          <Button
            className="register-btn"
            onClick={() => navigate("/register")}
          >
            Register
          </Button>

          <Button className="login-btn" onClick={() => navigate("/login")}>
            Login
          </Button>
        </div>
      </div>
    </header>
  );
};

export default Header;
