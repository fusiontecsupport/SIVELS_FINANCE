import React from 'react';
import styles from './Card.module.css';

const Card = ({ children, className = '', padding = 'md' }) => {
  const cardClass = `${styles.card} ${styles[`padding-${padding}`]} ${className}`.trim();
  
  return (
    <div className={cardClass}>
      {children}
    </div>
  );
};

export default Card;
