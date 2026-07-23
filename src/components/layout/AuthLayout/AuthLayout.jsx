import React from 'react';
import styles from './AuthLayout.module.css';
import Logo from '../../common/Logo/Logo';

const AuthLayout = ({ children, title, subtitle }) => {
  return (
    <div className={styles.container}>
      <div className={styles.leftPane}>
        <div className={styles.leftPaneContent}>
          <Logo size="xl" className={styles.logo} theme="light" />
          <h1 className={styles.heroTitle}>Empowering Your Financial Future</h1>
          <p className={styles.heroSubtitle}>
            Experience seamless, secure, and instant loan processing with enterprise-grade reliability.
          </p>
        </div>
      </div>
      <div className={styles.rightPane}>
        <div className={styles.formContainer}>
          <div className={styles.mobileHeader}>
            <Logo size="md" className={styles.mobileLogo} />
          </div>
          <div className={styles.formHeader}>
            <h2 className={styles.title}>{title}</h2>
            {subtitle && <p className={styles.subtitle}>{subtitle}</p>}
          </div>
          {children}
        </div>
      </div>
    </div>
  );
};

export default AuthLayout;
