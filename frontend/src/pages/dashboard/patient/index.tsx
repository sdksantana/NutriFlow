import { Scale, Clock, BarChart3, User } from "lucide-react";
import Layout from "../../../components/layouts/AppLayout";
import { useEffect, useState } from "react";
import { Endpoints } from "../../../routes/endpoints";
import api from "../../../api/axios";
import { NutritionistCardPreview } from "../../../components/dashboard/patient/NutritionistCardPreview";
import { NutritionistDetailsModal } from "../../../components/dashboard/patient/NutritionistDetailsModal";
import { useNavigate } from "react-router-dom";

interface DashboardResponse {
  firstName: string;
  lastWeightRegistered: number;
  nutritionist: string | null;
  nutritionistEmail: string | null;
  nutritionistCrn: string | null;
  nutritionistLinkStartDate: string | null;
  totalMealsRegistered: number | null;
}

export default function DashboardPatient() {
  const [firstName, setFirstName] = useState("");
  const [lastWeightRegistered, setLastWeightRegistered] = useState(0);
  const [nutritionist, setNutritionist] = useState<string | null>(null);
  const [nutritionistEmail, setNutritionistEmail] = useState<string | null>(
    null
  );
  const [nutritionistCrn, setNutritionistCrn] = useState<string | null>(null);
  const [startDate, setNutritionistLinkStartDate] = useState<string | null>(
    null
  );
  const [totalMealsRegistered, setTotalMealsRegistered] = useState<
    number | null
  >(null);
  const [showModal, setShowModal] = useState(false);

  const patientId = localStorage.getItem("userId");
  const navigate = useNavigate();
  useEffect(() => {
    async function fetchDashboard() {
      try {
        if (!patientId) return;
        const { data } = await api.get<DashboardResponse>(
          Endpoints.getDashboard(patientId)
        );
        setFirstName(data.firstName);
        setLastWeightRegistered(data.lastWeightRegistered);
        setNutritionist(data.nutritionist);
        setNutritionistEmail(data.nutritionistEmail);
        setNutritionistCrn(data.nutritionistCrn);
        setNutritionistLinkStartDate(data.nutritionistLinkStartDate);
        setTotalMealsRegistered(data.totalMealsRegistered);
      } catch (error) {
        console.error("Erro ao carregar dashboard", error);
      }
    }

    fetchDashboard();
  }, [patientId]);

  return (
    <div className="min-h-screen bg-orange-50">
      <Layout>
        <main className="max-w-4xl mx-auto px-4 mg:px-6 lg:px-8 py-12">
          <div className="text-center mb-12">
            <h1 className="text-3xl font-bold text-gray-900 mb-2">
              Olá, {firstName}!
            </h1>
            <p className="text-gray-600">
              Aqui está um resumo do seu progresso.
            </p>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
            <div
              onClick={() => navigate("/patient/evolution")}
              className="bg-white rounded-2xl shadow-md h-44 w-70 flex flex-col justify-center items-center px-4 py-8 transition-all duration-300 hover:shadow-lg hover:-translate-y-1 cursor-pointer"
            >
              <div className="w-12 h-12 bg-orange-100 rounded-lg flex items-center justify-center mb-2">
                <Scale className="w-6 h-6 text-orange-600" />
              </div>
              <p className="text-gray-600 text-mg mb-2">
                Último peso registrado
              </p>
              <p className="text-3xl font-bold text-gray-900">
                {lastWeightRegistered}{" "}
                <span className="text-lg font-normal">kg</span>
              </p>
            </div>

            <div className="bg-white rounded-2xl shadow-md h-44 w-70 flex flex-col justify-center items-center px-4 py-8 transition-all duration-300 hover:shadow-lg hover:-translate-y-1 cursor-pointer">
              <div className="w-12 h-12 bg-orange-100 rounded-lg flex items-center justify-center mb-2">
                <Clock className="w-6 h-6 text-orange-600" />
              </div>
              <p className="text-gray-600 text-mg mb-2">
                Refeições registradas hoje
              </p>
              <span className="text-mg italic text-gray-500 flex items-center gap-1">
                <Clock className="h-4 w-4" />
                Em construção
              </span>
            </div>

            <div
              onClick={() => navigate("/patient/recipes")}
              className="bg-white rounded-2xl shadow-md h-44 w-70 flex flex-col justify-center items-center px-4 py-8 transition-all duration-300 hover:shadow-lg hover:-translate-y-1 cursor-pointer"
            >
              <div className="w-12 h-12 bg-orange-100 rounded-lg flex items-center justify-center mb-2">
                <BarChart3 className="w-6 h-6 text-orange-600" />
              </div>
              <p className="text-gray-600 text-mg mb-2">Receitas salvas</p>

              {totalMealsRegistered && totalMealsRegistered > 0 ? (
                <p className="text-2xl font-bold text-gray-900">
                  {totalMealsRegistered.toString().padStart(2, "0")}
                </p>
              ) : (
                <p className="text-mg italic text-gray-500 text-center">
                  Nenhuma receita cadastrada até o momento
                </p>
              )}
            </div>

            {nutritionist ? (
              <>
                <NutritionistCardPreview
                  name={nutritionist}
                  onClick={() => setShowModal(true)}
                />
                {showModal && (
                  <NutritionistDetailsModal
                    onClose={() => setShowModal(false)}
                    nutritionistInfo={{
                      name: nutritionist,
                      email: nutritionistEmail || "",
                      crn: nutritionistCrn || "",
                      startDate: startDate || "",
                    }}
                    onUnlink={() => {
                      console.log("Desvincular nutricionista");
                      setShowModal(false);
                    }}
                  />
                )}
              </>
            ) : (
              <div className="bg-white rounded-2xl shadow-md h-44 w-70 flex flex-col justify-center items-center px-4 py-8 transition hover:shadow-lg">
                <div className="w-12 h-12 bg-orange-100 rounded-lg flex items-center justify-center mb-2">
                  <User className="w-6 h-6 text-orange-600" />
                </div>
                <p className="text-gray-600 text-mg mb-2">
                  Nutricionista vinculado
                </p>
                <p className="text-mg italic text-gray-500 text-center">
                  Nenhum nutricionista vinculado no momento
                </p>
              </div>
            )}
          </div>
        </main>
      </Layout>
    </div>
  );
}
