import { Users, RefreshCcw, BookOpen, Star } from "lucide-react";
import Layout from "../../../components/layouts/AppLayout";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import api from "../../../api/axios";
import { Endpoints } from "../../../routes/endpoints";
import { PatientCardPreview } from "../../../components/dashboard/nutritionist/PatientCardPreview";
import { PatientDetailsModal } from "../../../components/dashboard/nutritionist/PatientDetailsModal";
import { mockedRecipes } from "../../../mocks/mockRecipes";

interface NutritionistDashboardResponse {
  firstName: string;
  topUpdatedPatient: string | null;
  totalMealsRegistered: number | null;
  totalLinkedPatients: number | null;
  patientEmail: string | null;
  patientGender: string | null;
  patientLinkStartDate: string | null;
}

export default function DashboardNutritionist() {
  const [firstName, setFirstName] = useState("");
  const [totalLinkedPatients, setTotalLinkedPatients] = useState(0);
  const [totalRecipesRegistered, setTotalRecipesRegistered] = useState(0); // mockado
  const [topUpdatedPatient, setTopUpdatedPatient] = useState<string | null>(
    null
  );
  const [patientEmail, setPatientEmail] = useState<string | null>(null);
  const [patientGender, setPatientGender] = useState<string | null>(null);
  const [patientLinkStartDate, setPatientLinkStartDate] = useState<
    string | null
  >(null);
  const [showModal, setShowModal] = useState(false);

  const navigate = useNavigate();

  useEffect(() => {
    async function fetchDashboard() {
      const userId = localStorage.getItem("userId");
      if (!userId) return;

      try {
        const { data } = await api.get<NutritionistDashboardResponse>(
          Endpoints.getNutritionistDashboard(userId)
        );

        setFirstName(data.firstName);
        setTotalLinkedPatients(data.totalLinkedPatients || 0);
        setTopUpdatedPatient(data.topUpdatedPatient);
        setPatientEmail(data.patientEmail);
        setPatientGender(data.patientGender);
        setPatientLinkStartDate(data.patientLinkStartDate);

        setTotalRecipesRegistered(data.totalMealsRegistered || 0);
      } catch (error) {
        console.error("Erro ao carregar dashboard do nutricionista", error);
      }
    }

    fetchDashboard();
  }, []);

  return (
    <div className="min-h-screen bg-orange-50">
      <Layout>
        <main className="max-w-4xl mx-auto px-4 mg:px-6 lg:px-8 py-12">
          <div className="text-center mb-12">
            <h1 className="text-3xl font-bold text-gray-900 mb-2">
              Olá, {firstName}!
            </h1>
            <p className="text-gray-600">
              Aqui está um resumo do seu painel como nutricionista.
            </p>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
            <div
              onClick={() => navigate("/nutritionist/profile")}
              className="bg-white rounded-2xl shadow-md h-44 flex flex-col justify-center items-center px-4 py-8 transition hover:shadow-lg hover:-translate-y-1 cursor-pointer"
            >
              <div className="w-12 h-12 bg-orange-100 rounded-lg flex items-center justify-center mb-2">
                <Users className="w-6 h-6 text-orange-600" />
              </div>
              <p className="text-gray-600 text-mg mb-2">Pacientes vinculados</p>
              <p className="text-2xl font-bold text-gray-900">
                {totalLinkedPatients.toString().padStart(2, "0")}
              </p>
            </div>

            <div className="bg-white rounded-2xl shadow-md h-44 flex flex-col justify-center items-center px-4 py-8 transition hover:shadow-lg">
              <div className="w-12 h-12 bg-orange-100 rounded-lg flex items-center justify-center mb-2">
                <RefreshCcw className="w-6 h-6 text-orange-600" />
              </div>
              <p className="text-gray-600 text-mg mb-2">
                Atualizações recente de pacientes
              </p>
              <span className="text-mg italic text-gray-500 flex items-center gap-1">
                Em construção
              </span>
            </div>

            <div
              onClick={() => navigate("/nutritionist/recipes")}
              className="bg-white rounded-2xl shadow-md h-44 flex flex-col justify-center items-center px-4 py-8 transition hover:shadow-lg hover:-translate-y-1 cursor-pointer"
            >
              <div className="w-12 h-12 bg-orange-100 rounded-lg flex items-center justify-center mb-2">
                <BookOpen className="w-6 h-6 text-orange-600" />
              </div>
              <p className="text-gray-600 text-mg mb-2">Receitas cadastradas</p>
              <p className="text-2xl font-bold text-gray-900">
                {totalRecipesRegistered.toString().padStart(2, "0")}
              </p>
            </div>

            {topUpdatedPatient ? (
              <>
                <PatientCardPreview
                  name={topUpdatedPatient}
                  email={patientEmail || ""}
                  gender={patientGender || ""}
                  startDate={patientLinkStartDate || ""}
                  onClick={() => setShowModal(true)}
                />
                {showModal && (
                  <PatientDetailsModal
                    onClose={() => setShowModal(false)}
                    patientInfo={{
                      name: topUpdatedPatient,
                      email: patientEmail || "",
                      gender: patientGender || "",
                      startDate: patientLinkStartDate || "",
                    }}
                  />
                )}
              </>
            ) : (
              <div className="bg-white rounded-2xl shadow-md h-44 flex flex-col justify-center items-center px-4 py-8">
                <div className="w-12 h-12 bg-orange-100 rounded-lg flex items-center justify-center mb-2">
                  <Star className="w-6 h-6 text-orange-600" />
                </div>
                <p className="text-gray-600 text-mg mb-2">
                  Paciente com mais atualizações
                </p>
                <p className="text-mg italic text-gray-500 text-center">
                  Ainda não há pacientes com registros suficientes
                </p>
              </div>
            )}
          </div>
        </main>
      </Layout>
    </div>
  );
}
