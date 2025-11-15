import React from "react";
import "./Home.css";
import Button from "@mui/material/Button";
import { useNavigate } from "react-router-dom";

const Home: React.FC = () => {
  const navigate = useNavigate();

  const handleUploadClick = () => {
    const isLoggedIn = localStorage.getItem("auth"); // verifică login

    if (!isLoggedIn) {
      navigate("/login"); // redirect dacă nu e logat
    } else {
      navigate("/report"); // merge la report dacă e logat
    }
  };

  return (
    <div className="layout">
      <main className="content">
        <div className="overlay">
          <h1>Bine ai venit pe pagina noastră</h1>
          <p>Încarcă să ajutăm comunitatea</p>

          <Button
            variant="contained"
            onClick={handleUploadClick}
            className="uploadBtn"
          >
            Încarcă o poză 🚗
          </Button>
        </div>
      </main>
    </div>
  );
};

export default Home;
