import { Routes, Route, Navigate } from 'react-router-dom';

import MainLayout from './layout/MainLayout';
import Courses from './pages/Course';
import Students from './pages/Student';
import Home from './pages/Home';


export default function App() {
  return (
    <Routes>
      <Route path="/" element={<MainLayout />}>
        <Route index element={<Navigate to="/dashboard" replace />} />
        <Route path="dashboard" element={<Home />} />
        <Route path="courses" element={<Courses />} />
        <Route path="students" element={<Students />} />
      </Route>
      <Route path="*" element={<h2>404 - Page Not Found</h2>} />
    </Routes>
  );

  
}
