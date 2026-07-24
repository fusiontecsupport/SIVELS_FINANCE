import './TopNav.css';
import Logo from '../../common/Logo/Logo';
import Button from '../../common/Button/Button';
import { Bell, ChevronDown } from 'lucide-react';

const TopNav = ({ title, subtitle, headerContent }) => {
  return (
    <header className="topnav">
      <div className="topnav-brand">
        <Logo />
      </div>
      
      <div className="topnav-divider"></div>
      
      <div className="topnav-content">
        <div className="topnav-titles">
          <h1 className="topnav-title">{title}</h1>
          {subtitle && (
            <>
              <span className="topnav-subtitle-separator">|</span>
              <span className="topnav-subtitle">{subtitle}</span>
            </>
          )}
        </div>
        
        {headerContent && (
          <div className="topnav-header-content">
            {headerContent}
          </div>
        )}
      </div>
      
      <div className="topnav-right">
        {/* Removed Save Draft, Notifications, and User Profile as per request */}
      </div>
    </header>
  );
};

export default TopNav;
