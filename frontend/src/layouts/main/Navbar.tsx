import { NavLink } from "react-router";
import { FaBook, FaBookOpen, FaHouse, FaPenNib } from "react-icons/fa6";

export const Navbar = () => {
  const linkClass = ({ isActive }: { isActive: boolean }) =>
    `px-3 py-2 rounded-md transition-colors ${
      isActive
        ? "bg-blue-600 text-white"
        : "text-gray-700 hover:bg-gray-100 hover:text-blue-600"
    }`;

  return (
    <nav className="bg-white shadow-md">
      <div className="mx-auto flex h-16 max-w-7xl items-center justify-between px-6">
        {/* Logo */}
        <NavLink
          to="/"
          className="flex items-center gap-2 text-xl font-bold text-blue-600"
        >
          <FaBookOpen aria-hidden="true" />
          Bookshelf
        </NavLink>

        {/* Links */}
        <div className="flex items-center gap-2">
          <NavLink to="/" end className={(props) => `${linkClass(props)} flex items-center gap-2`}>
            <FaHouse aria-hidden="true" />
            Home
          </NavLink>

          <NavLink to="/books" className={(props) => `${linkClass(props)} flex items-center gap-2`}>
            <FaBook aria-hidden="true" />
            Books
          </NavLink>

          <NavLink to="/authors" className={(props) => `${linkClass(props)} flex items-center gap-2`}>
            <FaPenNib aria-hidden="true" />
            Authors
          </NavLink>
        </div>
      </div>
    </nav>
  );
};