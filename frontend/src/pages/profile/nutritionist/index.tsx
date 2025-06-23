import Layout from "../../../components/layouts/AppLayout";
import { useEffect, useState } from "react";
import { User, UserMinus, UserPlus, ChevronRight } from "lucide-react";
import { toast } from "react-toastify";
import api from "../../../api/axios";
import { Endpoints } from "../../../routes/endpoints";

interface NutritionistInfo {
  firstName: string;
  lastName: string;
  crn: string;
}

interface Patient {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  gender: string;
  height: number;
  weight: number;
  startDate: string;
  expanded?: boolean;
}

export default function NutritionistProfile() {
  const [nutritionist, setNutritionist] = useState<NutritionistInfo>({
    firstName: "",
    lastName: "",
    crn: "",
  });

  const [patients, setPatients] = useState<Patient[]>([]);
  const [showPopup, setShowPopup] = useState(false);
  const [emailInput, setEmailInput] = useState("");

  async function fetchProfile() {
    const userId = localStorage.getItem("userId");
    if (!userId) return;

    try {
      const response = await api.get(Endpoints.getProfile(userId));
      const { firstName, lastName, crn, linkedPatients } = response.data;
      setNutritionist({ firstName, lastName, crn });

      const mappedPatients: Patient[] = linkedPatients.map((p: any) => ({
        id: p.id,
        email: p.email,
        firstName: p.firstName,
        lastName: p.lastName,
        gender: p.gender,
        height: p.height,
        weight: p.weight,
        startDate: p.startDate,
      }));

      setPatients(mappedPatients);
    } catch (error: any) {
      const errors = error?.response?.data?.errors;
      if (Array.isArray(errors)) {
        errors.forEach((e: string) => toast.error(e));
      } else {
        toast.error("Erro ao carregar dados do nutricionista");
      }
      console.error(error);
    }
  }

  useEffect(() => {
    fetchProfile();
  }, []);

  async function unlinkPatient(email: string) {
    const nutritionistId = localStorage.getItem("userId");
    try {
      await api.post(Endpoints.addLink(nutritionistId!), {
        email: email,
        isActive: false,
        userType: 0,
      });
      toast.success("Paciente desvinculado com sucesso!");
      fetchProfile();
    } catch (error: any) {
      const backendErrors = error?.response?.data?.errors;

      if (error?.response?.status === 400 && Array.isArray(backendErrors)) {
        backendErrors.forEach((e: string) => toast.error(e));
      } else {
        toast.error("Erro ao desvincular paciente.");
      }
    }
  }

  async function handleLinkPatient() {
    const nutritionistId = localStorage.getItem("userId");
    try {
      await api.post(Endpoints.addLink(nutritionistId!), {
        email: emailInput,
        isActive: true,
        userType: 0,
      });
      toast.success("Paciente vinculado com sucesso!");
      setShowPopup(false);
      setEmailInput("");
      fetchProfile();
    } catch (error: any) {
      const backendErrors = error?.response?.data?.errors;

      if (error?.response?.status === 400 && Array.isArray(backendErrors)) {
        backendErrors.forEach((e: string) => toast.error(e));
      } else {
        toast.error("Erro ao desvincular paciente.");
      }
    }
  }

  function toggleExpand(id: string) {
    setPatients((prev) =>
      prev.map((p) => (p.id === id ? { ...p, expanded: !p.expanded } : p))
    );
  }

  function getGenderText(gender: string) {
    switch (gender.toUpperCase()) {
      case "MALE":
        return "Masculino";
      case "FEMALE":
        return "Feminino";
      default:
        return "Outro";
    }
  }

  return (
    <Layout>
      <div className="w-full max-w-5xl mx-auto">
        <h2 className="text-3xl font-bold mb-2">Perfil do Nutricionista</h2>
        <p className="text-gray-600 mb-6">
          Gerencie suas informações profissionais e pacientes vinculados.
        </p>

        <div className="bg-white border rounded-xl p-6 mb-8">
          <h3 className="text-xl font-semibold mb-4 text-gray-900">
            Informações Profissionais
          </h3>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div>
              <label className="text-sm font-medium text-gray-700">
                Nome *
              </label>
              <div className="px-3 py-2 bg-gray-100 border border-gray-300 rounded-md text-gray-600">
                {nutritionist.firstName}
              </div>
            </div>
            <div>
              <label className="text-sm font-medium text-gray-700">
                Sobrenome *
              </label>
              <div className="px-3 py-2 bg-gray-100 border border-gray-300 rounded-md text-gray-600">
                {nutritionist.lastName}
              </div>
            </div>
            <div className="md:col-span-2">
              <label className="text-sm font-medium text-gray-700">CRN *</label>
              <div className="px-3 py-2 bg-gray-100 border border-gray-300 rounded-md text-gray-600 max-w-xs">
                {nutritionist.crn}
              </div>
            </div>
          </div>
          <p className="text-sm text-gray-600 mt-4">
            * Campos somente leitura. Para alterações, entre em contato com o
            suporte.
          </p>
        </div>

        <div className="bg-white border rounded-xl p-6">
          <div className="flex justify-between items-center mb-4">
            <h3 className="text-xl font-semibold text-gray-900">
              Pacientes vinculados ({patients.length})
            </h3>
            <button
              onClick={() => setShowPopup(true)}
              className="flex items-center gap-1 px-3 py-2 border border-gray-300 bg-white text-gray-700 hover:bg-gray-100 rounded-md text-sm"
            >
              <UserPlus className="w-4 h-4" /> Vincular paciente
            </button>
          </div>

          {patients.length === 0 ? (
            <div className="text-center py-8 text-gray-500">
              <User className="w-12 h-12 mx-auto mb-4 text-gray-300" />
              <p>Nenhum paciente vinculado ainda.</p>
            </div>
          ) : (
            <div className="space-y-4">
              {patients.map((patient) => (
                <div
                  key={patient.id}
                  className="border border-gray-200 rounded-lg bg-gray-50"
                >
                  <div
                    className="px-4 py-3 flex justify-between items-center hover:bg-gray-100 cursor-pointer"
                    onClick={() => toggleExpand(patient.id)}
                  >
                    <div className="flex items-center gap-2">
                      <ChevronRight
                        className={`w-4 h-4 text-gray-500 transform transition-transform ${
                          patient.expanded ? "rotate-90" : "rotate-0"
                        }`}
                      />
                      <span className="font-medium text-gray-900">
                        {patient.firstName} {patient.lastName}
                      </span>
                    </div>
                    <button
                      onClick={(e) => {
                        e.stopPropagation();
                        unlinkPatient(patient.email);
                      }}
                      className="flex items-center gap-1 px-3 py-1 border border-red-200 text-red-600 hover:text-red-700 hover:bg-red-50 rounded-md text-sm"
                    >
                      <UserMinus className="w-4 h-4" /> Desvincular
                    </button>
                  </div>
                  {patient.expanded && (
                    <div className="px-4 pb-4">
                      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 gap-y-2 gap-x-6 text-sm text-gray-700">
                        <div>
                          <span className="font-semibold block text-gray-600">
                            Nome:
                          </span>
                          {patient.firstName}
                        </div>
                        <div>
                          <span className="font-semibold block text-gray-600">
                            Sobrenome:
                          </span>
                          {patient.lastName}
                        </div>
                        <div>
                          <span className="font-semibold block text-gray-600">
                            Sexo:
                          </span>
                          {getGenderText(patient.gender)}
                        </div>
                        <div>
                          <span className="font-semibold block text-gray-600">
                            Altura:
                          </span>
                          {patient.height}cm
                        </div>
                        <div>
                          <span className="font-semibold block text-gray-600">
                            Peso:
                          </span>
                          {patient.weight}kg
                        </div>
                        <div className="md:col-span-2">
                          <span className="font-semibold block text-gray-600">
                            Email:
                          </span>
                          {patient.email}
                        </div>
                        <div>
                          <span className="font-semibold block text-gray-600">
                            Desde:
                          </span>
                          {patient.startDate}
                        </div>
                      </div>
                    </div>
                  )}
                </div>
              ))}
            </div>
          )}
        </div>

        {showPopup && (
          <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
            <div className="bg-white p-6 rounded-xl shadow-lg w-full max-w-md">
              <h4 className="text-lg font-semibold mb-4">Vincular paciente</h4>
              <input
                type="email"
                value={emailInput}
                onChange={(e) => setEmailInput(e.target.value)}
                className="w-full border border-gray-300 rounded-md px-4 py-2 mb-4"
                placeholder="Digite o email do paciente"
              />
              <div className="flex justify-end gap-2">
                <button
                  onClick={() => setShowPopup(false)}
                  className="px-4 py-2 text-sm bg-gray-200 hover:bg-gray-300 rounded-md"
                >
                  Cancelar
                </button>
                <button
                  onClick={handleLinkPatient}
                  className="px-4 py-2 text-sm bg-orange-500 hover:bg-orange-600 text-white rounded-md"
                >
                  Vincular
                </button>
              </div>
            </div>
          </div>
        )}
      </div>
    </Layout>
  );
}
