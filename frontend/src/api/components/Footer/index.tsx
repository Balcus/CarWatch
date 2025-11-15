import { FC } from "react";
import "./Footer.css";

export const Footer: FC = () => {
  return (
    <footer className="footer">
      <div className="footer-container">
        <p className="footer-text">
          © {new Date().getFullYear()} CarWatch. All rights reserved.
        </p>
      </div>
    </footer>
  );
};

export default Footer;
