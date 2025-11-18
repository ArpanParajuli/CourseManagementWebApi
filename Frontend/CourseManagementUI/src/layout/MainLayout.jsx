import { Outlet } from 'react-router-dom';
import Navbar from '../components/Navbar';

export default function MainLayout() {
  return (
    <div>
      <Navbar />
      <main className="p-5">
        <Outlet />
      </main>
    </div>
  );
}
