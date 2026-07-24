import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Phone, Send } from 'lucide-react';
import AuthLayout from '../../components/layout/AuthLayout/AuthLayout';
import Card from '../../components/common/Card/Card';
import Input from '../../components/common/Input/Input';
import Button from '../../components/common/Button/Button';
import ProgressStepper from '../../components/common/ProgressStepper/ProgressStepper';
import { validateMobile } from '../../utils/validation';
import './SignUp.css';

const STEPS = [
  { label: 'Mobile Verification' },
  { label: 'OTP Verification' }
];

const SignUp = () => {
  const navigate = useNavigate();
  const [mobile, setMobile] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const handleSendOTP = (e) => {
    e.preventDefault();
    
    // Validate
    const validationError = validateMobile(mobile);
    if (validationError) {
      setError(validationError);
      return;
    }

    setError('');
    setLoading(true);

    // Simulate API call
    setTimeout(() => {
      setLoading(false);
      navigate('/otp', { state: { mobile } });
    }, 1000);
  };

  return (
    <AuthLayout>
      <Card className="signup-card">
        <div className="signup-header">
          <div className="user-icon-circle">
            <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path d="M20 21V19C20 17.9391 19.5786 16.9217 18.8284 16.1716C18.0783 15.4214 17.0609 15 16 15H8C6.93913 15 5.92172 15.4214 5.17157 16.1716C4.42143 16.9217 4 17.9391 4 19V21" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"/>
              <path d="M12 11C14.2091 11 16 9.20914 16 7C16 4.79086 14.2091 3 12 3C9.79086 3 8 4.79086 8 7C8 9.20914 9.79086 11 12 11Z" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"/>
            </svg>
          </div>
          <h2>Sign In</h2>
          <p>Sign in to your account to get started<br/>with Sivels Finance</p>
        </div>

        <ProgressStepper steps={STEPS} currentStep={1} />

        <form onSubmit={handleSendOTP} className="signup-form">
          <div className="form-group">
            <Input 
              label="Mobile Number *"
              placeholder="Enter your mobile number"
              icon={Phone}
              type="tel"
              value={mobile}
              onChange={(e) => {
                const val = e.target.value.replace(/\D/g, '').slice(0, 10);
                setMobile(val);
                if (error) setError('');
              }}
              error={error}
            />
          </div>

          <div className="info-text">
            <svg width="16" height="16" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path d="M12 22C17.5228 22 22 17.5228 22 12C22 6.47715 17.5228 2 12 2C6.47715 2 2 6.47715 2 12C2 17.5228 6.47715 22 12 22Z" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"/>
              <path d="M9 12L11 14L15 10" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"/>
            </svg>
            <span>We'll send a 6-digit OTP to this mobile number</span>
          </div>

          <Button type="submit" loading={loading} icon={Send}>
            Send OTP
          </Button>
          
          <Button type="button" variant="secondary" onClick={() => navigate('/customer-registration')}>
            Register
          </Button>
          
          <div className="divider">
            <span>OR</span>
          </div>
          
          <Button variant="outline" type="button" className="google-btn">
            <svg width="20" height="20" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path d="M22.56 12.25C22.56 11.47 22.49 10.72 22.36 10H12V14.26H17.92C17.67 15.63 16.89 16.8 15.72 17.58V20.34H19.28C21.36 18.42 22.56 15.6 22.56 12.25Z" fill="#4285F4"/>
              <path d="M12 23C14.97 23 17.46 22.02 19.28 20.34L15.72 17.58C14.73 18.24 13.48 18.64 12 18.64C9.14 18.64 6.72 16.71 5.84 14.12H2.16V16.97C3.97 20.57 7.7 23 12 23Z" fill="#34A853"/>
              <path d="M5.84 14.12C5.61 13.46 5.49 12.74 5.49 12C5.49 11.26 5.61 10.54 5.84 9.88V7.03H2.16C1.41 8.52 1 10.21 1 12C1 13.79 1.41 15.48 2.16 16.97L5.84 14.12Z" fill="#FBBC05"/>
              <path d="M12 5.36C13.62 5.36 15.07 5.92 16.21 7.01L19.36 3.86C17.45 2.07 14.96 1 12 1C7.7 1 3.97 3.43 2.16 7.03L5.84 9.88C6.72 7.29 9.14 5.36 12 5.36Z" fill="#EA4335"/>
            </svg>
            Continue with Google
          </Button>
        </form>
      </Card>
    </AuthLayout>
  );
};

export default SignUp;
