import React from 'react';
import styles from './BackgroundWrapper.module.css';

const BackgroundWrapper = ({ children, className = '' }) => {
  const wrapperClass = `${styles.wrapper} ${className}`.trim();
  
  return (
    <div className={wrapperClass}>
      <div className={styles.blob1}></div>
      <div className={styles.blob2}></div>
      <div className={styles.content}>
        {children}
      </div>
    </div>
  );
};

export default BackgroundWrapper;
