import { ReactNode } from "react";
import Layout from "./AppLayout";
interface RecipesLayoutProps {
  children: ReactNode;
}

export default function RecipesLayout({ children }: RecipesLayoutProps) {
  return (
    <Layout>
      <div className="w-full min-h-screen bg-orange-50">
        <div className="w-full max-w-7xl mx-auto px-6 py-8">
          <h1 className="text-3xl font-bold text-gray-800 mb-2">
            Menu de Receitas
          </h1>
          <p className="text-gray-600 mb-6">
            Acompanhe e gerencie as receitas cadastradas.
          </p>

          {children}
        </div>
      </div>
    </Layout>
  );
}
