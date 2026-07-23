import React from 'react';
import styles from './Loader.module.css';

const Loader = ({ size = 'md', color = 'primary', className = '' }) => {
  const loaderClass = `${styles.loader} ${styles[`size-${size}`]} ${styles[`color-${color}`]} ${className}`.trim();
  
  return (
    <div className={loaderClass} role="status" aria-label="Loading">
      <span className={styles.srOnly}>Loading...</span>
    </div>
  );
};

export default Loader;
