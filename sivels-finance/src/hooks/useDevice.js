import { useState, useEffect } from 'react';

const useDevice = () => {
  // Initialize synchronously to prevent layout shifts and routing flickers
  const [isMobile, setIsMobile] = useState(() => 
    typeof window !== 'undefined' ? window.innerWidth < 768 : false
  );

  useEffect(() => {
    const handleResize = () => {
      // Treat screens under 768px as mobile
      setIsMobile(window.innerWidth < 768);
    };

    window.addEventListener('resize', handleResize);
    return () => window.removeEventListener('resize', handleResize);
  }, []);

  return { isMobile };
};

export default useDevice;
