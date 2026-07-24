import { ShieldCheck, Zap, Clock } from 'lucide-react';
import './LeftPanel.css';

const features = [
  {
    icon: ShieldCheck,
    title: 'Secure & Reliable',
    description: 'Bank-level security to protect your data'
  },
  {
    icon: Zap,
    title: 'Fast Onboarding',
    description: 'Quick & easy signup in just a few steps'
  },
  {
    icon: Clock,
    title: '24/7 Support',
    description: 'Our team is always here to help you'
  }
];

const LeftPanel = () => {
  return (
    <div className="left-panel">
      <div className="left-panel-content">
        <div className="welcome-section">
          <span className="welcome-badge">Secure • Simple • Trusted</span>
          <h1 className="welcome-title">Welcome to<br/>Sivels Finance</h1>
          <p className="welcome-subtitle">
            Your trusted partner for smart loan management and financial growth.
          </p>
        </div>

        <div className="features-list">
          {features.map((feature, index) => {
            const Icon = feature.icon;
            return (
              <div key={index} className="feature-item">
                <div className="feature-icon-wrapper">
                  <Icon className="feature-icon" size={20} />
                </div>
                <div className="feature-text">
                  <h3 className="feature-title">{feature.title}</h3>
                  <p className="feature-description">{feature.description}</p>
                </div>
              </div>
            );
          })}
        </div>
      </div>
      
      <div className="security-illustration">
        <div className="shield-icon-large">
          <ShieldCheck size={48} />
        </div>
        <div className="check-badge">
          <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
            <circle cx="12" cy="12" r="12" fill="#10B981"/>
            <path d="M7 12L10.5 15.5L17 9" stroke="white" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"/>
          </svg>
        </div>
      </div>
    </div>
  );
};

export default LeftPanel;
