import './RegistrationStepper.css';
import { User, Phone, MapPin, Briefcase, Building, Lock } from 'lucide-react';

const STEPS = [
  { id: 1, label: 'Personal\nInformation', icon: User },
  { id: 2, label: 'Contact\nInformation', icon: Phone },
  { id: 3, label: 'Address\nInformation', icon: MapPin },
  { id: 4, label: 'Employment\nInformation', icon: Briefcase },
  { id: 5, label: 'Business\nInformation', icon: Building },
];

const RegistrationStepper = ({ activeStep = 1 }) => {
  return (
    <div className="registration-stepper-container">
      <div className="registration-stepper">
        {STEPS.map((step, index) => {
          const Icon = step.icon;
          
          return (
            <div key={step.id} className="registration-step-wrapper">
              <div className="registration-step">
                <div className={`reg-step-circle ${step.id === activeStep ? 'active' : ''} ${step.id < activeStep ? 'completed' : ''}`}>
                  <Icon size={16} />
                </div>
              </div>
            </div>
          );
        })}
      </div>
    </div>
  );
};

export default RegistrationStepper;
