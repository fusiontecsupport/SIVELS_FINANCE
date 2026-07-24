import './DashboardLayout.css';
import Sidebar from '../Sidebar/Sidebar';
import TopNav from '../TopNav/TopNav';

const DashboardLayout = ({ children, title, subtitle, headerContent, hideSidebar = false, sidebarActiveStep = 1, onSidebarStepClick }) => {
  return (
    <div className="dashboard-layout">
      <TopNav title={title} subtitle={subtitle} headerContent={headerContent} />
      <div className="dashboard-container">
        {!hideSidebar && <Sidebar activeStep={sidebarActiveStep} onStepClick={onSidebarStepClick} />}
        <main className="dashboard-main">
          {children}
        </main>
      </div>
    </div>
  );
};

export default DashboardLayout;
