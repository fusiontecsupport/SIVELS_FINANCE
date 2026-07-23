import React, { useState, useEffect } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import toast from 'react-hot-toast';
import AuthLayout from '../../components/layout/AuthLayout/AuthLayout';
import Input from '../../components/common/Input/Input';
import PasswordInput from '../../components/common/PasswordInput/PasswordInput';
import Button from '../../components/common/Button/Button';
import styles from './Register.module.css';

const Register = () => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    username: '',
    firstName: '',
    lastName: '',
    email: '',
    mobileNumber: '',
    gender: '',
    dateOfBirth: '',
    password: '',
    confirmPassword: ''
  });
  const [errors, setErrors] = useState({});
  const [isLoading, setIsLoading] = useState(false);
  const [apiError, setApiError] = useState('');

  const [otp, setOtp] = useState('');
  const [currentStep, setCurrentStep] = useState(1);
  const [isSuccessMsgVisible, setIsSuccessMsgVisible] = useState(false);
  const [timer, setTimer] = useState(0);
  const [otpError, setOtpError] = useState('');
  const [isOtpLoading, setIsOtpLoading] = useState(false);

  useEffect(() => {
    let interval = null;
    if (timer > 0) {
      interval = setInterval(() => {
        setTimer(t => t - 1);
      }, 1000);
    }
    return () => clearInterval(interval);
  }, [timer]);

  const handleSendOtp = async () => {
    if (!formData.email) {
      setErrors(prev => ({ ...prev, email: 'Email is required to send OTP.' }));
      return;
    }
    if (!/\S+@\S+\.\S+/.test(formData.email)) {
      setErrors(prev => ({ ...prev, email: 'Invalid email format.' }));
      return;
    }

    setIsOtpLoading(true);
    setOtpError('');
    try {
      const response = await fetch('/api/Auth/send-otp', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email: formData.email })
      });
      const data = await response.json();
      if (response.ok && data.status) {
        // Success toast is handled in handleSubmit
        setCurrentStep(2);
        setTimer(60);
      } else {
        const errorMsg = data.message || 'Failed to send OTP.';
        if (errorMsg.toLowerCase().includes('email')) {
          setErrors(prev => ({ ...prev, email: errorMsg }));
        } else {
          setOtpError(errorMsg);
        }
        toast.error(errorMsg, { duration: 3500 });
      }
    } catch (error) {
      const errorMsg = 'Network error while sending OTP.';
      setOtpError(errorMsg);
      toast.error(errorMsg, { duration: 3500 });
    } finally {
      setIsOtpLoading(false);
    }
  };

  const handleVerifyOtp = async () => {
    if (!otp) {
      setOtpError('OTP is required.');
      return;
    }

    setIsOtpLoading(true);
    setOtpError('');
    try {
      const response = await fetch('/api/Auth/verify-otp', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email: formData.email, otp: otp })
      });
      const data = await response.json();
      if (response.ok && data.status) {
        setOtpError('');
        setIsSuccessMsgVisible(true);
        toast.success('Email Verified Successfully', { duration: 2500 });
        await performRegistration();
      } else {
        const errorMsg = data.message || 'Invalid OTP.';
        setOtpError(errorMsg);
        toast.error(errorMsg, { duration: 3500 });
      }
    } catch (error) {
      const errorMsg = 'Network error while verifying OTP.';
      setOtpError(errorMsg);
      toast.error(errorMsg, { duration: 3500 });
    } finally {
      setIsOtpLoading(false);
    }
  };

  const validateForm = () => {
    const newErrors = {};
    if (!formData.username) newErrors.username = 'Username is required.';
    if (!formData.firstName) newErrors.firstName = 'First Name is required.';
    if (!formData.lastName) newErrors.lastName = 'Last Name is required.';
    
    if (!formData.email) {
      newErrors.email = 'Email is required.';
    } else if (!/\S+@\S+\.\S+/.test(formData.email)) {
      newErrors.email = 'Invalid email format.';
    }

    if (!formData.mobileNumber) {
      newErrors.mobileNumber = 'Mobile Number is required.';
    } else if (!/^\d{10}$/.test(formData.mobileNumber)) {
      newErrors.mobileNumber = 'Mobile Number must contain exactly 10 digits.';
    }
    
    if (!formData.gender) newErrors.gender = 'Gender is required';
    if (!formData.dateOfBirth) newErrors.dateOfBirth = 'Date of Birth is required';

    if (!formData.password) {
      newErrors.password = 'Password is required.';
    } else if (formData.password.length < 8) {
      newErrors.password = 'Password must be at least 8 characters.';
    }

    if (!formData.confirmPassword) {
      newErrors.confirmPassword = 'Confirm Password is required';
    } else if (formData.password !== formData.confirmPassword) {
      newErrors.confirmPassword = 'Passwords do not match';
    }

    setErrors(newErrors);
    const isValid = Object.keys(newErrors).length === 0;
    if (!isValid) {
      toast('Please fix the errors before submitting', { icon: '⚠️', duration: 3000 });
    }
    return isValid;
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
    if (errors[name]) {
      setErrors(prev => ({ ...prev, [name]: '' }));
    }
  };

  const performRegistration = async () => {
    setIsLoading(true);
    try {
      const response = await fetch('/api/Auth/register', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          username: formData.username,
          firstName: formData.firstName,
          lastName: formData.lastName,
          email: formData.email,
          mobileNumber: formData.mobileNumber,
          gender: formData.gender,
          dateOfBirth: formData.dateOfBirth,
          passwordHash: formData.password
        })
      });

      const data = await response.json();

      if (response.ok && data.status) {
        toast.success('Registration Successful', { duration: 2500 });
        setTimeout(() => {
          navigate('/login');
        }, 1000);
      } else {
        setIsSuccessMsgVisible(false);
        setCurrentStep(1);
        const errorMsg = data.message || 'Registration failed. Please try again.';
        
        const lowerError = errorMsg.toLowerCase();
        if (lowerError.includes('username')) {
          setErrors(prev => ({ ...prev, username: errorMsg }));
        } else if (lowerError.includes('email')) {
          setErrors(prev => ({ ...prev, email: errorMsg }));
        } else if (lowerError.includes('mobile')) {
          setErrors(prev => ({ ...prev, mobileNumber: errorMsg }));
        } else {
          setApiError(errorMsg);
        }
        
        toast.error(errorMsg, { duration: 3500 });
      }
    } catch (error) {
      setIsSuccessMsgVisible(false);
      setCurrentStep(1);
      const errorMsg = 'Network error. Please ensure the backend is running.';
      setApiError(errorMsg);
      toast.error(errorMsg, { duration: 3500 });
    } finally {
      setIsLoading(false);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setApiError('');
    if (validateForm()) {
      setIsLoading(true);
      try {
        const response = await fetch('/api/Auth/check-user', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            username: formData.username,
            email: formData.email,
            mobile: formData.mobileNumber
          })
        });
        const data = await response.json();
        
        if (response.ok && data.success) {
          toast.success('User details verified. OTP sent successfully.', { duration: 2500 });
          await handleSendOtp();
        } else {
          toast.error(data.message || 'Validation failed', { duration: 3500 });
        }
      } catch (error) {
        toast.error('Network error while checking user details.', { duration: 3500 });
      } finally {
        setIsLoading(false);
      }
    }
  };

  const handleOtpKeyDown = (e) => {
    if (e.key === 'Enter') {
      e.preventDefault();
      handleVerifyOtp();
    }
  };

  return (
    <AuthLayout 
      title={currentStep === 1 ? "Create Account" : "Verify Email"} 
      subtitle={currentStep === 1 ? "Please fill in your details to register." : ""}
    >
      {currentStep === 1 ? (
        <form onSubmit={handleSubmit} className={styles.form} noValidate>
          {apiError && <div className={styles.apiError}>{apiError}</div>}
          
          <div className={styles.row}>
            <Input
              label="First Name"
              id="firstName"
              name="firstName"
              placeholder="First Name"
              value={formData.firstName}
              onChange={handleChange}
              error={errors.firstName}
            />
            <Input
              label="Last Name"
              id="lastName"
              name="lastName"
              placeholder="Last Name"
              value={formData.lastName}
              onChange={handleChange}
              error={errors.lastName}
            />
          </div>

          <Input
            label="Username"
            id="username"
            name="username"
            placeholder="Choose a username"
            value={formData.username}
            onChange={handleChange}
            error={errors.username}
          />

          <Input
            label="Email"
            id="email"
            name="email"
            type="email"
            placeholder="Enter your email"
            value={formData.email}
            onChange={handleChange}
            error={errors.email}
          />

          <div className={styles.row}>
            <Input
              label="Mobile Number"
              id="mobileNumber"
              name="mobileNumber"
              placeholder="e.g. 1234567890"
              value={formData.mobileNumber}
              onChange={handleChange}
              error={errors.mobileNumber}
            />
            <Input
              label="Date of Birth"
              id="dateOfBirth"
              name="dateOfBirth"
              type="date"
              placeholder="YYYY-MM-DD"
              value={formData.dateOfBirth}
              onChange={handleChange}
              error={errors.dateOfBirth}
            />
          </div>
          
          <div className={styles.inputGroup}>
            <label className={styles.label} htmlFor="gender">Gender</label>
            <select 
              id="gender"
              name="gender" 
              value={formData.gender} 
              onChange={handleChange}
              className={`${styles.select} ${errors.gender ? styles.selectError : ''}`}
            >
              <option value="" disabled>Select Gender</option>
              <option value="Male">Male</option>
              <option value="Female">Female</option>
              <option value="Other">Other</option>
            </select>
            {errors.gender && <span className={styles.errorText}>{errors.gender}</span>}
          </div>
          
          <PasswordInput
            label="Password"
            id="password"
            name="password"
            placeholder="Create password"
            value={formData.password}
            onChange={handleChange}
            error={errors.password}
          />
          
          <PasswordInput
            label="Confirm Password"
            id="confirmPassword"
            name="confirmPassword"
            placeholder="Confirm password"
            value={formData.confirmPassword}
            onChange={handleChange}
            error={errors.confirmPassword}
          />
          
          <div className={`${styles.row} ${styles.submitButton}`}>
            <Button 
              type="button" 
              variant="outline"
              fullWidth 
              size="lg" 
              onClick={() => navigate('/login')}
            >
              Cancel
            </Button>
            <Button 
              type="submit" 
              fullWidth 
              size="lg" 
              disabled={isLoading || isOtpLoading}
            >
              {isLoading ? 'Registering...' : 'Register'}
            </Button>
          </div>

          <div className={styles.loginLink}>
            Already have an account? <Link to="/login">Sign In</Link>
          </div>
        </form>
      ) : (
        <div style={{ display: 'flex', flexDirection: 'column', gap: '20px' }}>
          {isSuccessMsgVisible ? (
            <div style={{ textAlign: 'center', color: '#10b981', padding: '20px' }}>
              <div style={{ fontSize: '1.2rem', fontWeight: 'bold', marginBottom: '10px' }}>
                Email Verified Successfully ✓
              </div>
              <div style={{ color: '#64748b' }}>
                Creating your account...
              </div>
            </div>
          ) : (
            <>
              <p style={{ color: '#475569', fontSize: '1rem', margin: 0, textAlign: 'center' }}>
                A verification code has been sent to<br />
                <strong>{formData.email}</strong>
              </p>
              
              <Input
                id="otp"
                name="otp"
                type="text"
                placeholder="Enter 6-digit OTP"
                value={otp}
                onChange={(e) => setOtp(e.target.value)}
                onKeyDown={handleOtpKeyDown}
                error={otpError}
                autoFocus
              />

              <div style={{ display: 'flex', flexDirection: 'column', gap: '12px' }}>
                <Button
                  type="button"
                  fullWidth
                  size="lg"
                  onClick={handleVerifyOtp}
                  disabled={isOtpLoading || isLoading || !otp}
                >
                  {isLoading || isOtpLoading ? 'Verifying...' : 'Verify OTP'}
                </Button>

                <Button
                  type="button"
                  variant="outline"
                  fullWidth
                  size="lg"
                  onClick={handleSendOtp}
                  disabled={timer > 0 || isOtpLoading}
                >
                  {timer > 0 ? `Resend in ${timer}s` : 'Resend OTP'}
                </Button>
              </div>

              <div style={{ marginTop: '10px', textAlign: 'center' }}>
                <button 
                  type="button" 
                  onClick={() => setCurrentStep(1)}
                  style={{ background: 'none', border: 'none', color: '#3b82f6', cursor: 'pointer', fontSize: '0.95rem', fontWeight: '500' }}
                >
                  &larr; Back to Registration
                </button>
              </div>
            </>
          )}
        </div>
      )}
    </AuthLayout>
  );
};

export default Register;
