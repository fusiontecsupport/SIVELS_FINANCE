import { Shield } from 'lucide-react';
import './Footer.css';

const Footer = () => {
  return (
    <footer className="auth-footer">
      <div className="footer-content">
        <div className="footer-left">
          <Shield className="footer-icon" size={20} />
          <p>
            Your data is 100% secure<br />
            and will never be shared.
          </p>
        </div>
        <div className="footer-right">
          <p>© 2025 Sivels Finance. All rights reserved.</p>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
