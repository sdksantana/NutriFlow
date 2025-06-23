import { useState } from "react";
import { Routes, Route } from "react-router-dom";

import LoginPage from "../pages/login";
import RegisterPage from "../pages/register";

import PatientDashboard from "../pages/dashboard/patient";
import PatientProfile from "../pages/profile/patient";
import RecipesPage from "../pages/recipes";
import PatientEvolutionPage from "../pages/evolution/patient";

import NutritionistDashboard from "../pages/dashboard/nutritionist";
import NutritionistProfile from "../pages/profile/nutritionist";
import NutritionistEvolutionPage from "../pages/evolution/nutritionist";

export function AppRoutes() {
  const [activeTab, setActiveTab] = useState<"receitas" | "plano">("receitas");

  return (
    <Routes>
      <Route path="/" element={<LoginPage />} />
      <Route path="/login" element={<LoginPage />} />
      <Route path="/register" element={<RegisterPage />} />
      <Route path="/patient/dashboard" element={<PatientDashboard />} />
      <Route path="/patient/profile" element={<PatientProfile />} />
      <Route
        path="/patient/recipes"
        element={
          <RecipesPage
            activeTab={activeTab}
            setActiveTab={setActiveTab}
            mode="patient"
          />
        }
      />
      <Route path="/patient/evolution" element={<PatientEvolutionPage />} />
      <Route
        path="/nutritionist/dashboard"
        element={<NutritionistDashboard />}
      />
      <Route path="/nutritionist/profile" element={<NutritionistProfile />} />
      <Route
        path="/nutritionist/recipes"
        element={
          <RecipesPage
            activeTab={activeTab}
            setActiveTab={setActiveTab}
            mode="nutritionist"
          />
        }
      />
      <Route
        path="/nutritionist/evolution"
        element={<NutritionistEvolutionPage />}
      />
    </Routes>
  );
}
