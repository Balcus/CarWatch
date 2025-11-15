/*
import React from "react";
import Header from "../Header";
import Footer from "../Footer";
import "./Home.css";

const Home: React.FC = () => {
  return (
    <>
      <main className="homeContainer">
        <h1 className="homeTitle">Bine ai venit pe pagina noastră</h1>

        <p className="homeSubtitle">Încarcă să ajutăm comunitatea</p>

        <button className="uploadBtn">Încarcă o poză</button>
      </main>
    </>
  );
};

export default Home;
*/

import React from "react";
import "./Home.css";

const Home = () => {
  return (
    <div className="layout">
      <main className="content">
        <div className="overlay">
          <h1>Bine ai venit pe pagina noastră</h1>
          <p>Încarcă să ajutăm comunitatea</p>
          <button>Încarcă o poză</button>
        </div>
      </main>
    </div>
  );
};

export default Home;
