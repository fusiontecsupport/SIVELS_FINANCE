import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import toast from 'react-hot-toast';
import AuthLayout from '../../components/layout/AuthLayout/AuthLayout';
import Input from '../../components/common/Input/Input';
import PasswordInput from '../../components/common/PasswordInput/PasswordInput';
import Button from '../../components/common/Button/Button';
import styles from './Login.module.css';

const Login = () => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    username: '',
    password: '',
    rememberMe: false
  });
  const [errors, setErrors] = useState({});
  const [isLoading, setIsLoading] = useState(false);
  const [apiError, setApiError] = useState('');

  const validateForm = () => {
    const newErrors = {};
    if (!formData.username) {
      newErrors.username = 'Username is required';
    }
    
    if (!formData.password) {
      newErrors.password = 'Password is required';
    } else if (formData.password.length < 6) {
      newErrors.password = 'Password must be at least 6 characters';
    }

    setErrors(newErrors);
    const isValid = Object.keys(newErrors).length === 0;
    if (!isValid) {
      toast.error('Please fix the errors before submitting', { duration: 3000 });
    }
    return isValid;
  };

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: type === 'checkbox' ? checked : value
    }));
    // Clear error when user starts typing
    if (errors[name]) {
      setErrors(prev => ({ ...prev, [name]: '' }));
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setApiError('');
    if (validateForm()) {
      setIsLoading(true);
      try {
        const response = await fetch('/api/Auth/login', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({
            username: formData.username,
            password: formData.password
          })
        });

        const data = await response.json();

        if (response.ok && data.status) {
          sessionStorage.setItem('user', JSON.stringify(data));
          
          toast.success(
            <div>
              <strong>Login Successful</strong>
              <br />
              Welcome back, {data.firstName || formData.username}!
            </div>,
            { duration: 2500 }
          );

          setTimeout(() => {
            navigate('/dashboard');
          }, 2500);
        } else {
          const errorMsg = data.message || 'Invalid Username or Password';
          setApiError(errorMsg);
          toast.error(errorMsg, { duration: 3000 });
        }
      } catch (error) {
        const errorMsg = 'Network error. Please ensure the backend is running.';
        setApiError(errorMsg);
        toast.error(errorMsg, { duration: 3000 });
      } finally {
        setIsLoading(false);
      }
    }
  };

  return (
    <AuthLayout 
      title="Welcome Back" 
      subtitle="Please enter your details to sign in."
    >
      <form onSubmit={handleSubmit} className={styles.form} noValidate>
        {apiError && <div className={styles.apiError}>{apiError}</div>}
        <Input
          label="Username"
          id="username"
          name="username"
          type="text"
          placeholder="Enter your username"
          value={formData.username}
          onChange={handleChange}
          error={errors.username}
        />
        
        <PasswordInput
          label="Password"
          id="password"
          name="password"
          placeholder="Enter your password"
          value={formData.password}
          onChange={handleChange}
          error={errors.password}
        />
        

        <Button 
          type="submit" 
          fullWidth 
          size="lg" 
          disabled={isLoading}
          className={styles.submitButton}
        >
          {isLoading ? 'Signing in...' : 'Sign In'}
        </Button>

        <Button 
          type="button" 
          variant="outline"
          fullWidth 
          size="lg" 
          onClick={() => navigate('/register')}
          className={styles.newUserButton}
        >
          New User
        </Button>
      </form>
    </AuthLayout>
  );
};

export default Login;
