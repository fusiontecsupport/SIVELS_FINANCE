import React from 'react';
import styles from './PageContainer.module.css';
import Header from '../Header/Header';
import Footer from '../Footer/Footer';

const PageContainer = ({ children, withHeader = true, withFooter = true }) => {
  return (
    <div className={styles.container}>
      {withHeader && <Header />}
      <main className={styles.mainContent}>
        {children}
      </main>
      {withFooter && <Footer />}
    </div>
  );
};

export default PageContainer;
