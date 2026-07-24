import './Sidebar.css';
import { 
  User, 
  Phone, 
  MapPin, 
  Briefcase, 
  Building, 
  Lock, 
  Headset,
  ArrowRight,
  ShieldCheck
} from 'lucide-react';
import Button from '../../common/Button/Button';

const STEPS = [
  { id: 1, label: 'Personal Information', icon: User, active: true },
  { id: 2, label: 'Contact Information', icon: Phone },
  { id: 3, label: 'Address Information', icon: MapPin },
  { id: 4, label: 'Employment Information', icon: Briefcase },
  { id: 5, label: 'Business Information', icon: Building },
];

const Sidebar = ({ activeStep = 1, onStepClick }) => {
  return (
    <aside className="sidebar">
      <div className="sidebar-header">
        <div className="sidebar-active-card">
          <div className="sidebar-active-icon">
            <User size={20} />
          </div>
          <div className="sidebar-active-text">
            <h3>Customer Onboarding</h3>
            <p>Create a new customer profile</p>
          </div>
        </div>
      </div>
      
      <nav className="sidebar-nav">
        {STEPS.map((step) => {
          const Icon = step.icon;
          return (
            <div 
              key={step.id} 
              className={`sidebar-nav-item ${step.id === activeStep ? 'active' : ''}`}
              onClick={() => onStepClick && onStepClick(step.id)}
              style={{ cursor: 'pointer' }}
            >
              <div className="sidebar-nav-icon-wrapper">
                <Icon size={18} className="sidebar-nav-icon" />
              </div>
              <div className="sidebar-nav-badge">{step.id}</div>
              <span className="sidebar-nav-label">{step.label}</span>
            </div>
          );
        })}
      </nav>
    </aside>
  );
};

export default Sidebar;
