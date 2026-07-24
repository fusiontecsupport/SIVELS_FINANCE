import { Routes, Route } from 'react-router-dom';
import SignUp from '../pages/SignUp/SignUp';
import OTPVerification from '../pages/OTPVerification/OTPVerification';
import CustomerRegistration from '../pages/CustomerRegistration/CustomerRegistration';
import DashboardWelcome from '../pages/DashboardWelcome/DashboardWelcome';

const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/" element={<SignUp />} />
      <Route path="/otp" element={<OTPVerification />} />
      <Route path="/dashboard" element={<DashboardWelcome />} />
      <Route path="/customer-registration" element={<CustomerRegistration />} />
    </Routes>
  );
};

export default AppRoutes;
