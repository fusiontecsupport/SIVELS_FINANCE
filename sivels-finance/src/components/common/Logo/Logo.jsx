import React from 'react';
import styles from './Logo.module.css';
import logoImage from '../../../assets/images/logo.png';

const Logo = ({ size = 'md', className = '', showText = true, theme = 'dark' }) => {
  const containerClass = `${styles.container} ${styles[`size-${size}`]} ${styles[`theme-${theme}`]} ${className}`.trim();
  
  return (
    <div className={containerClass}>
      <img src={logoImage} alt="LoanPro Logo" className={styles.image} />
      {showText && (
        <span className={styles.text}>
          Loan<span className={styles.textHighlight}>Pro</span>
        </span>
      )}
    </div>
  );
};

export default Logo;
