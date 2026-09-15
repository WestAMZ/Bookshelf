import { NavLink } from "react-router";

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
          className="text-xl font-bold text-blue-600"
        >
          Bookshelf
        </NavLink>

        {/* Links */}
        <div className="flex items-center gap-2">
          <NavLink to="/" end className={linkClass}>
            Home
          </NavLink>

          <NavLink to="/books" className={linkClass}>
            Books
          </NavLink>

          <NavLink to="/authors" className={linkClass}>
            Authors
          </NavLink>
        </div>
      </div>
    </nav>
  );
};