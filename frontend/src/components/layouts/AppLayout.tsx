import { ReactNode } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { Home, User, BookOpen, TrendingUp, LogOut } from "lucide-react";

interface LayoutProps {
  children: ReactNode;
  hideMenu?: boolean;
}

export default function Layout({ children, hideMenu = false }: LayoutProps) {
  const location = useLocation();
  const navigate = useNavigate();

  const userType = localStorage.getItem("userType");
  const isNutritionist = userType === "0";

  const menuItems = [
    {
      label: "Menu Inicial",
      path: isNutritionist ? "/nutritionist/dashboard" : "/patient/dashboard",
      icon: <Home className="w-5 h-5" />,
    },
    {
      label: "Perfil",
      path: isNutritionist ? "/nutritionist/profile" : "/patient/profile",
      icon: <User className="w-5 h-5" />,
    },
    {
      label: "Receitas",
      path: isNutritionist ? "/nutritionist/recipes" : "/patient/recipes",
      icon: <BookOpen className="w-5 h-5" />,
    },
    {
      label: "Evolução",
      path: isNutritionist ? "/nutritionist/evolution" : "/patient/evolution",
      icon: <TrendingUp className="w-5 h-5" />,
    },
  ];

  function handleLogout() {
    localStorage.removeItem("token");
    localStorage.removeItem("userId");
    localStorage.removeItem("userType");
    navigate("/login");
  }

  return (
    <div className="min-h-screen flex flex-col bg-orange-50">
      {!hideMenu && (
        <header className="w-full bg-[#FDEEDC] py-3 px-2 sm:px-6">
          <div className="flex flex-col sm:flex-row items-start sm:items-center justify-between gap-2 sm:gap-0">
            <div className="flex items-center gap-2">
              <div className="w-6 h-6 bg-orange-500 rounded-full"></div>
              <span className="text-lg sm:text-xl font-bold text-black font-poppins">
                NutriFlow
              </span>
            </div>

            <div className="flex items-center gap-2 sm:gap-4 mt-2 sm:mt-0 flex-wrap">
              {menuItems.map((item) => {
                const isActive = location.pathname === item.path;
                return (
                  <Link
                    key={item.path}
                    to={item.path}
                    className={`flex items-center justify-center gap-2 px-4 py-2 rounded transition text-sm sm:text-base ${
                      isActive
                        ? "text-orange-600 bg-orange-200"
                        : "text-gray-600 hover:text-orange-700 hover:bg-orange-200"
                    }`}
                  >
                    {item.icon}
                    <span>{item.label}</span>
                  </Link>
                );
              })}

              <button
                onClick={handleLogout}
                className="flex items-center gap-2 px-4 py-2 text-sm sm:text-base text-gray-600 hover:text-orange-700 hover:bg-orange-200 rounded transition"
              >
                <LogOut className="w-5 h-5" />
                <span>Sair</span>
              </button>
            </div>
          </div>
        </header>
      )}

      <main className="flex-1 px-4 py-6 md:px-10 md:py-8">
        <div className="max-w-7xl mx-auto">{children}</div>
      </main>
    </div>
  );
}
