import { useState, useEffect } from 'react';
import { useLocation, useNavigate, Link } from 'react-router-dom';
import { Mail, Lock, CheckCircle2, ArrowLeft } from 'lucide-react';
import AuthLayout from '../../components/layout/AuthLayout/AuthLayout';
import Card from '../../components/common/Card/Card';
import Button from '../../components/common/Button/Button';
import ProgressStepper from '../../components/common/ProgressStepper/ProgressStepper';
import OTPInput from '../../components/common/OTPInput/OTPInput';
import { CONSTANTS } from '../../utils/constants';
import './OTPVerification.css';

const STEPS = [
  { label: 'Mobile Verification' },
  { label: 'OTP Verification' }
];

const OTPVerification = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const mobile = location.state?.mobile || '9876543210';
  
  const [otp, setOtp] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const [timeLeft, setTimeLeft] = useState(CONSTANTS.RESEND_TIMER_SECONDS);

  useEffect(() => {
    if (!location.state?.mobile) {
      // In a real app we might redirect back if no mobile is found, 
      // but for this demo it's fine.
    }
  }, [location]);

  useEffect(() => {
    if (timeLeft <= 0) return;
    
    const timerId = setInterval(() => {
      setTimeLeft((prev) => prev - 1);
    }, 1000);
    
    return () => clearInterval(timerId);
  }, [timeLeft]);

  const handleVerify = (e) => {
    e.preventDefault();
    
    if (otp.length !== CONSTANTS.OTP_LENGTH) {
      setError(`Please enter a valid ${CONSTANTS.OTP_LENGTH}-digit OTP`);
      return;
    }
    
    setLoading(true);
    
    // Simulate API verification
    setTimeout(() => {
      setLoading(false);
      if (otp === CONSTANTS.OTP) {
        navigate('/dashboard');
      } else {
        setError('Invalid OTP. Please try again.');
      }
    }, 800);
  };

  const handleResend = () => {
    if (timeLeft > 0) return;
    
    // Reset timer
    setTimeLeft(CONSTANTS.RESEND_TIMER_SECONDS);
    setOtp('');
    setError('');
    // Simulate resend
  };

  const formatTime = (seconds) => {
    const mins = Math.floor(seconds / 60);
    const secs = seconds % 60;
    return `${mins.toString().padStart(2, '0')}:${secs.toString().padStart(2, '0')}`;
  };

  return (
    <AuthLayout showTopRightSign={true}>
      <Card className="otp-card">
        <div className="otp-header">
          <div className="mail-icon-circle">
            <Mail size={24} />
          </div>
          <h2>OTP Verification</h2>
          <p>Enter the 6-digit code sent to<br/>your mobile number</p>
        </div>

        <ProgressStepper steps={STEPS} currentStep={2} />

        <div className="success-banner">
          <div className="success-banner-content">
            <CheckCircle2 size={20} className="success-icon" />
            <div className="success-text">
              <span className="success-title">OTP has been sent to</span>
              <span className="success-email">+91 {mobile}</span>
            </div>
          </div>
          <Link to="/" className="edit-email-link">Edit Number</Link>
        </div>

        <form onSubmit={handleVerify} className="otp-form">
          <div className="form-group">
            <OTPInput 
              label={`Enter ${CONSTANTS.OTP_LENGTH}-digit OTP`}
              required
              length={CONSTANTS.OTP_LENGTH} 
              value={otp} 
              onChange={(val) => {
                setOtp(val);
                if (error) setError('');
              }} 
              error={error} 
            />
          </div>

          <div className="resend-section">
            <span className="resend-text">Didn't receive the code?</span>
            <button 
              type="button" 
              className={`resend-btn ${timeLeft > 0 ? 'disabled' : ''}`}
              onClick={handleResend}
              disabled={timeLeft > 0}
            >
              Resend OTP {timeLeft > 0 && `in ${formatTime(timeLeft)}`}
            </button>
          </div>

          <Button type="submit" loading={loading} icon={Lock}>
            Verify OTP
          </Button>
          
          <div className="change-email-container">
            <Link to="/" className="change-email-link">
              <ArrowLeft size={16} />
              Change Mobile Number
            </Link>
          </div>
        </form>
      </Card>
    </AuthLayout>
  );
};

export default OTPVerification;
