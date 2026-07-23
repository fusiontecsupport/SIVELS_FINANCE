import React, { useState, forwardRef } from 'react';
import { Eye, EyeOff } from 'lucide-react';
import Input from '../Input/Input';
import styles from './PasswordInput.module.css';

const PasswordInput = forwardRef((props, ref) => {
  const [showPassword, setShowPassword] = useState(false);

  const togglePasswordVisibility = () => {
    setShowPassword((prev) => !prev);
  };

  return (
    <div className={styles.container}>
      <Input
        ref={ref}
        type={showPassword ? 'text' : 'password'}
        wrapperClassName={styles.inputWrapper}
        {...props}
      />
      <button
        type="button"
        className={styles.toggleButton}
        onClick={togglePasswordVisibility}
        aria-label={showPassword ? 'Hide password' : 'Show password'}
        tabIndex="-1"
      >
        {showPassword ? <EyeOff size={18} /> : <Eye size={18} />}
      </button>
    </div>
  );
});

PasswordInput.displayName = 'PasswordInput';

export default PasswordInput;
