import './CustomerRegistration.css';
import DashboardLayout from '../../components/layout/DashboardLayout/DashboardLayout';
import RegistrationStepper from './RegistrationStepper';
import FormCard from './FormCard';
import Input from '../../components/common/Input/Input';
import Select from '../../components/common/Select/Select';
import Textarea from '../../components/common/Textarea/Textarea';
import Checkbox from '../../components/common/Checkbox/Checkbox';
import Button from '../../components/common/Button/Button';
import { 
  User, 
  Phone, 
  MapPin, 
  Briefcase, 
  Building,
  UserCircle2,
  Mail,
  Calendar,
  Building2,
  Receipt
} from 'lucide-react';

import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const CustomerRegistration = () => {
  const navigate = useNavigate();
  const [activeStep, setActiveStep] = useState(1);
  const [currentAddress, setCurrentAddress] = useState('');
  const [permanentAddress, setPermanentAddress] = useState('');
  const [sameAsCurrent, setSameAsCurrent] = useState(false);

  const handleCurrentAddressChange = (e) => {
    const val = e.target.value;
    setCurrentAddress(val);
    if (sameAsCurrent) {
      setPermanentAddress(val);
    }
  };

  const handleSameAsCurrentChange = (e) => {
    const isChecked = e.target.checked;
    setSameAsCurrent(isChecked);
    if (isChecked) {
      setPermanentAddress(currentAddress);
    }
  };

  const handleSidebarClick = (stepId) => {
    setActiveStep(stepId);
    const element = document.getElementById(`step-${stepId}`);
    if (element) {
      element.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
  };

  return (
    <DashboardLayout 
      title="Customer Registration"
      sidebarActiveStep={activeStep}
      onSidebarStepClick={handleSidebarClick}
    >
      <form 
        className="customer-registration-content"
        onSubmit={(e) => {
          e.preventDefault();
          navigate('/');
        }}
      >
        <div className="registration-forms-grid">
          
          {/* Row 1 */}
          <div className="forms-row forms-row-1">
            <FormCard id="step-1" title="Personal Information" icon={User} className="personal-card" onFocusCapture={() => setActiveStep(1)}>
              <div className="form-grid-2">
                <Input label="First Name" required maxLength={50} pattern="[A-Za-z\s\-]+" title="Only letters are allowed" placeholder="Enter first name" icon={UserCircle2} />
                <Input label="Last Name" required maxLength={50} pattern="[A-Za-z\s\-]+" title="Only letters are allowed" placeholder="Enter last name" icon={UserCircle2} />
              </div>
              <div className="form-grid-3">
                <Input label="Date of Birth" required type="date" icon={Calendar} />
                <Select label="Gender" required options={[
                  { value: 'male', label: 'Male' },
                  { value: 'female', label: 'Female' },
                  { value: 'other', label: 'Other' }
                ]} placeholder="Select gender" />
                <Select label="Marital Status" required options={[
                  { value: 'single', label: 'Single' },
                  { value: 'married', label: 'Married' }
                ]} placeholder="Select status" />
              </div>
            </FormCard>
            
            <FormCard id="step-2" title="Contact Information" icon={Phone} className="contact-card" onFocusCapture={() => setActiveStep(2)}>
              <div className="form-col">
                <Input label="Mobile Number" required type="tel" maxLength={10} minLength={10} pattern="[0-9]{10}" title="Mobile number must be exactly 10 digits" placeholder="Enter mobile number" icon={Phone} onInput={(e) => e.target.value = e.target.value.replace(/[^0-9]/g, '')} />
                <Input label="Email Address" required type="email" maxLength={100} pattern="^[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,}$" title="Please enter a valid email address (e.g., name@example.com)" placeholder="Enter email address" icon={Mail} />
              </div>
            </FormCard>
          </div>
          
          {/* Row 2 */}
          <FormCard id="step-3" title="Address Information" icon={MapPin} className="address-card" onFocusCapture={() => setActiveStep(3)}>
            <div className="form-grid-2 align-start">
              <Textarea 
                label="Current Address" 
                required 
                maxLength={250}
                minLength={10}
                placeholder="Enter current address" 
                value={currentAddress}
                onChange={handleCurrentAddressChange}
              />
              <div className="permanent-address-col">
                <Textarea 
                  label="Permanent Address" 
                  required={!sameAsCurrent}
                  maxLength={250}
                  minLength={10}
                  placeholder="Enter permanent address" 
                  value={permanentAddress}
                  onChange={(e) => setPermanentAddress(e.target.value)}
                  disabled={sameAsCurrent}
                />
                <Checkbox 
                  label="Same as Current Address" 
                  className="same-address-cb" 
                  checked={sameAsCurrent}
                  onChange={handleSameAsCurrentChange}
                />
              </div>
            </div>
          </FormCard>
          
          {/* Row 3 */}
          <div className="forms-row forms-row-2">
            <FormCard id="step-4" title="Employment Information" icon={Briefcase} className="employment-card" onFocusCapture={() => setActiveStep(4)}>
              <div className="form-grid-2">
                <Input label="Employer Name" required maxLength={100} placeholder="Enter employer name" icon={UserCircle2} />
                <Input label="Designation" required maxLength={50} placeholder="Enter designation" icon={Briefcase} />
              </div>
              <div className="form-grid-2">
                <Input label="Salary" required type="number" min="0" max="999999999" placeholder="Enter salary" icon={() => <span style={{marginLeft: '4px'}}>₹</span>} />
              </div>
            </FormCard>
            
            <FormCard id="step-5" title="Business Information" icon={Building} className="business-card" onFocusCapture={() => setActiveStep(5)}>
              <div className="form-grid-2">
                <Input label="GST Number" required maxLength={15} minLength={15} pattern="[A-Za-z0-9]{15}" title="GST number must be exactly 15 alphanumeric characters" placeholder="Enter GST number" icon={Receipt} />
                <Select label="Annual Turnover" required options={[
                  { value: '0-10L', label: '0 - 10 Lakhs' },
                  { value: '10L-50L', label: '10 - 50 Lakhs' },
                  { value: '50L+', label: 'Above 50 Lakhs' }
                ]} placeholder="Select annual turnover" icon={Receipt} />
              </div>
              <div className="form-grid-2">
                <Input label="Nature of Business" required maxLength={100} placeholder="Enter nature of business" icon={Building2} />
              </div>
            </FormCard>
          </div>
          
        </div>
        
        <div className="registration-actions">
          <Button variant="outline" className="btn-cancel" onClick={() => navigate('/')}>
            Cancel
          </Button>
          <Button variant="primary" type="submit" className="btn-register">
            Register
          </Button>
        </div>
        
      </form>
    </DashboardLayout>
  );
};

export default CustomerRegistration;
