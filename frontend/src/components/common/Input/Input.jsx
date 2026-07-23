import React, { forwardRef } from 'react';
import styles from './Input.module.css';

const Input = forwardRef(({ 
  label, 
  id, 
  type = 'text', 
  error, 
  fullWidth = true,
  className = '',
  wrapperClassName = '',
  ...props 
}, ref) => {
  const inputClass = `${styles.input} ${error ? styles.inputError : ''} ${fullWidth ? styles.fullWidth : ''} ${className}`.trim();
  const generatedId = id || `input-${Math.random().toString(36).substr(2, 9)}`;

  return (
    <div className={`${styles.wrapper} ${fullWidth ? styles.fullWidth : ''} ${wrapperClassName}`.trim()}>
      {label && (
        <label htmlFor={generatedId} className={styles.label}>
          {label}
        </label>
      )}
      <input
        ref={ref}
        id={generatedId}
        type={type}
        className={inputClass}
        {...props}
      />
      {error && <span className={styles.errorText}>{error}</span>}
    </div>
  );
});

Input.displayName = 'Input';
export default Input;
