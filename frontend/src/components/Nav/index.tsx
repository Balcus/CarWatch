import { useState } from 'react';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import "../../index.css"
import './Nav.css';
import { useNavigate } from 'react-router';
import { Button } from '@mui/material';

const Nav: React.FC = () => {
  const [activeIndex, setActiveIndex] = useState(0);
  const [hoverIndex, setHoverIndex] = useState(0);
  const navigate = useNavigate();

  const navItems = [
    { label: 'Home', isDot: true, path: '/' },
    { label: 'Map', isDot: false, path: '/map' },
    { label: 'Report', isDot: false, path: '/report' },
  ];

  return (
    <div className="nav-container">
      <img src="/logo.svg" alt="CarWatch Logo" className="nav-logo" />

      <nav className="nav-center">
        <div className="nav-railway">
          <div
            className="nav-rail"
            style={{ '--index': hoverIndex + 1 } as React.CSSProperties}
          >
            <span className="nav-tracker"></span>
          </div>

          <div className="nav-items">
            {navItems.map((item, index) => (
              <a
                key={index}
                href="#"
                className={`nav-item ${index === activeIndex ? 'dot' : ''}`}
                onMouseOver={() => setHoverIndex(index)}
                onClick={(e) => {
                    e.preventDefault();
                    setActiveIndex(index);
                    navigate(item.path);
                }}
              >
                <span>{item.label}</span>
              </a>
            ))}
          </div>
        </div>
      </nav>

      <div className="nav-user">
        <Button onClick={() => {navigate("/login")}} disableRipple>
            <AccountCircleIcon style={{fontSize: '48px', color: '#00000098' }} />
        </Button>
      </div>
    </div>
  );
};

export default Nav;
