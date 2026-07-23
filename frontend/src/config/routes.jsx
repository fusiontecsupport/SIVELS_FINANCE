import React, { Suspense, lazy } from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import Loader from '../components/common/Loader/Loader';
import useDevice from '../hooks/useDevice';
import PageContainer from '../components/layout/PageContainer/PageContainer';
import ProtectedRoute from '../components/common/ProtectedRoute/ProtectedRoute';

import Splash from '../pages/Splash/Splash';
const Login = lazy(() => import('../pages/Login/Login'));
const Register = lazy(() => import('../pages/Register/Register'));

const AppRoutes = () => {
  const { isMobile } = useDevice();

  return (
    <Suspense 
      fallback={
        <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh', width: '100vw', backgroundColor: 'var(--color-background)' }}>
          <Loader size="lg" />
        </div>
      }
    >
      <Routes>
        <Route 
          path="/" 
          element={isMobile ? <Splash /> : <Navigate to="/login" replace />} 
        />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route 
          path="/dashboard" 
          element={
            <ProtectedRoute>
              <PageContainer>
                <div style={{ padding: '2rem', textAlign: 'center' }}>
                  <h2>Dashboard</h2>
                  <p>Welcome to the dashboard!</p>
                </div>
              </PageContainer>
            </ProtectedRoute>
          } 
        />
        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </Suspense>
  );
};

export default AppRoutes;
