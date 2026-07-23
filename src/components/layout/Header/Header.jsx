import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Logo from '../../common/Logo/Logo';
import styles from './Header.module.css';

const Header = () => {
  const [user, setUser] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const storedUser = sessionStorage.getItem('user') || localStorage.getItem('user');
    if (storedUser) {
      setUser(JSON.parse(storedUser));
    }
  }, []);

  const formatLastLogin = (lastLogin) => {
    if (!lastLogin) return 'First Login';
    const d = new Date(lastLogin);
    return d.toLocaleDateString() + ' ' + d.toLocaleTimeString();
  };

  const handleLogout = () => {
    sessionStorage.removeItem('user');
    localStorage.removeItem('user');
    navigate('/login', { replace: true });
  };

  return (
    <header className={styles.header}>
      <div className={styles.container}>
        <Logo size="sm" />
        <nav className={styles.nav}>
          {/* Navigation links will go here */}
        </nav>
        {user && (
          <div style={{ textAlign: 'right', fontSize: '14px', color: 'var(--color-text-secondary)', display: 'flex', alignItems: 'center', gap: '16px' }}>
            <div>
              <div style={{ fontWeight: 'bold', color: 'var(--color-text-primary)' }}>Welcome, {user.firstName}</div>
              <div>Last Login: {formatLastLogin(user.lastLogin)}</div>
            </div>
            <button 
              onClick={handleLogout}
              style={{
                padding: '6px 12px',
                background: 'transparent',
                border: '1px solid #e2e8f0',
                borderRadius: '6px',
                color: '#64748b',
                cursor: 'pointer',
                fontSize: '14px',
                fontWeight: '500',
                transition: 'all 0.2s'
              }}
              onMouseOver={(e) => { e.currentTarget.style.background = '#f8fafc'; e.currentTarget.style.color = '#ef4444'; e.currentTarget.style.borderColor = '#fca5a5'; }}
              onMouseOut={(e) => { e.currentTarget.style.background = 'transparent'; e.currentTarget.style.color = '#64748b'; e.currentTarget.style.borderColor = '#e2e8f0'; }}
            >
              Logout
            </button>
          </div>
        )}
      </div>
    </header>
  );
};

export default Header;
