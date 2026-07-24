import DashboardLayout from '../../components/layout/DashboardLayout/DashboardLayout';

const DashboardWelcome = () => {
  return (
    <DashboardLayout title="Dashboard" hideSidebar={true}>
      <div style={{ padding: '4rem', display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: '50vh' }}>
        <h1 style={{ fontSize: '2rem', fontWeight: '700', color: 'var(--text-heading)' }}>
          Welcome to Sivels Finance Dashboard
        </h1>
      </div>
    </DashboardLayout>
  );
};

export default DashboardWelcome;
