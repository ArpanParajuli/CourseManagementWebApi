import { NavLink } from "react-router-dom";

export default function Navbar() {
  return (
    <nav className="p-4 border-b border-gray-300 flex gap-6">
      <NavLink
        to="/courses"
        className={({ isActive }) =>
          isActive
            ? "font-bold text-black"
            : "text-gray-700 hover:text-black"
        }
      >
        Courses
      </NavLink>

      <NavLink
        to="/dashboard"
        className={({ isActive }) =>
          isActive
            ? "font-bold text-black"
            : "text-gray-700 hover:text-black"
        }
      >
        Dashboard
      </NavLink>

      <NavLink
        to="/students"
        className={({ isActive }) =>
          isActive
            ? "font-bold text-black"
            : "text-gray-700 hover:text-black"
        }
      >
        Students
      </NavLink>
    </nav>
  );
}
