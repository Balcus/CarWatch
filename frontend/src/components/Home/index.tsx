import React from "react";
import "./Home.css";
import { useNavigate } from "react-router-dom";
import { Box, Button } from "@mui/material";

const Home: React.FC = () => {
  const navigate = useNavigate();
  
  const handleUploadClick = () => {
    const isLoggedIn = localStorage.getItem("auth");
    if (!isLoggedIn) {
      navigate("/login");
    } else {
      navigate("/report");
    }
  };

  return (
    <>
      <Box className="home-page">
        <Box className="content-center">
          <Box className="left-image">
            <img 
              src="/police-man.png" 
              alt="Illegally parked vehicle" 
            />
          </Box>
          <Box className="right-content">
            <Box className="text-up">
              <h1>Help Keep Our Streets Clear</h1>
              <p>
                Join our community in making parking fair for everyone. 
                Report illegally parked vehicles quickly and easily. 
                Together, we can ensure accessible streets and reduce 
                traffic congestion caused by improper parking.
              </p>
            </Box>
            <Button onClick={handleUploadClick} disableRipple>
              Report Vehicle
            </Button>
          </Box>
        </Box>
      </Box>
    </>
  );
};

export default Home;