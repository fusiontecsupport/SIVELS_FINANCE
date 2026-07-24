import LeftPanel from '../LeftPanel/LeftPanel';
import Footer from '../Footer/Footer';
import Logo from '../../common/Logo/Logo';
import { Link } from 'react-router-dom';
import './AuthLayout.css';

const AuthLayout = ({ children, showTopRightSign = true }) => {
  return (
    <div className="auth-layout">
      <header className="auth-header">
        <div className="auth-header-content">
          <Logo />
        </div>
      </header>

      <div className="auth-container">
        <div className="auth-content">
          <div className="auth-left">
            <LeftPanel />
          </div>
          
          <div className="auth-right">
            <div className="auth-form-container">
              {children}
            </div>
          </div>
        </div>
      </div>
      
      <Footer />
    </div>
  );
};

export default AuthLayout;
