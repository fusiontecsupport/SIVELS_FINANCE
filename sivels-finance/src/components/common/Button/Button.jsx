import React from 'react';
import styles from './Button.module.css';

const Button = ({ 
  children, 
  onClick, 
  type = 'button', 
  variant = 'primary', 
  size = 'md', 
  fullWidth = false, 
  disabled = false, 
  className = '',
  ...props 
}) => {
  const baseClass = styles.button;
  const variantClass = styles[`variant-${variant}`];
  const sizeClass = styles[`size-${size}`];
  const fullWidthClass = fullWidth ? styles.fullWidth : '';

  const combinedClasses = `${baseClass} ${variantClass} ${sizeClass} ${fullWidthClass} ${className}`.trim();

  return (
    <button
      type={type}
      className={combinedClasses}
      onClick={onClick}
      disabled={disabled}
      {...props}
    >
      {children}
    </button>
  );
};

export default Button;
