import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import useDevice from '../../hooks/useDevice';
import './Splash.css';
import logoImage from '../../assets/images/logo.png';

const Splash = () => {
  const navigate = useNavigate();
  const { isMobile } = useDevice();

  useEffect(() => {
    // Desktop: Direct to Login (no splash)
    if (!isMobile) {
      navigate('/login', { replace: true });
      return;
    }

    // Mobile: Splash (2.5s) -> Login
    const timer = setTimeout(() => {
      navigate('/login', { replace: true });
    }, 2500);

    // Clean timer on component unmount
    return () => clearTimeout(timer);
  }, [navigate, isMobile]);

  // Hide completely on non-mobile devices to prevent flicker
  if (!isMobile) return null;

  return (
    <div className="splash-container">
      <div className="splash-content">
        <div className="splash-logo-wrapper">
          <img src={logoImage} alt="LoanPro Logo" className="splash-logo" />
        </div>
        
        <h1 className="splash-app-name">LoanPro</h1>
        <p className="splash-tagline">Fast &bull; Secure &bull; Trusted</p>
        
        <div className="splash-loader-section">
          <div className="splash-dots">
            <span></span>
            <span></span>
            <span></span>
          </div>
          <p className="splash-loading-text">Loading...</p>
        </div>
      </div>
    </div>
  );
};

export default Splash;
