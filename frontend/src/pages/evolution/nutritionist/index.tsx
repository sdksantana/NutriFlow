import { useEffect, useState } from "react";
import Layout from "../../../components/layouts/AppLayout";
import { TrendingUp, ChevronDown } from "lucide-react";
import { IMCIndicator } from "../../../components/evolution/IMCIndicator";
import { TMBCard } from "../../../components/evolution/TMBCard";
import { GalleryCard } from "../../../components/evolution/GalleryCard";
import { MeasurementHistory } from "../../../components/evolution/MeasurementHistory";
import { EvolutionChartCard } from "../../../components/evolution/EvolutionChartCard";
import api from "../../../api/axios";
import { Endpoints } from "../../../routes/endpoints";

interface PatientOption {
  id: string;
  name: string;
}

export default function NutritionistEvolution() {
  const [patients, setPatients] = useState<PatientOption[]>([]);
  const [selectedPatient, setSelectedPatient] = useState("");
  const [isFocused, setIsFocused] = useState(false);

  useEffect(() => {
    async function fetchPatients() {
      const nutritionistId = localStorage.getItem("userId");
      if (!nutritionistId) return;

      try {
        const res = await api.get(Endpoints.getProfile(nutritionistId));
        const response = res.data;

        const mapped = response.linkedPatients.map((p: any) => ({
          id: p.id,
          name: `${p.firstName} ${p.lastName}`,
        }));

        setPatients(mapped);
      } catch (err) {
        console.error("Erro ao buscar pacientes vinculados", err);
      }
    }

    fetchPatients();
  }, []);

  return (
    <Layout>
      <div className="min-h-screen bg-orange-50 py-8 px-4 space-y-10">
        <div className="max-w-5xl mx-auto bg-white rounded-2xl shadow-md border border-gray-200 px-8 py-10">
          <div className="text-center">
            <h1 className="text-3xl font-bold text-gray-900 mb-2">
              Dashboard de Evolução
            </h1>
            <p className="text-gray-600 text-base">
              Acompanhe a evolução dos seus pacientes
            </p>
          </div>

          <div className="flex justify-center mt-8 mb-6">
            <div className="relative w-full max-w-sm">
              <select
                value={selectedPatient}
                onChange={(e) => setSelectedPatient(e.target.value)}
                onFocus={() => setIsFocused(true)}
                className="w-full px-4 py-2 pr-10 border border-gray-300 rounded-md text-gray-700 bg-white shadow-sm appearance-none focus:outline-none focus:ring-2 focus:ring-orange-500"
              >
                <option value="" disabled={!selectedPatient}>
                  Selecione um paciente para acompanhar
                </option>

                {patients.map((p) => (
                  <option key={p.id} value={p.id}>
                    {p.name}
                  </option>
                ))}
              </select>

              <div className="pointer-events-none absolute right-3 top-1/2 -translate-y-1/2 text-gray-500">
                <ChevronDown className="w-4 h-4" />
              </div>
            </div>
          </div>

          {!selectedPatient && (
            <div className="flex flex-col items-center space-y-6 pt-10">
              <TrendingUp className="w-16 h-16 text-gray-300" />
              <p className="text-gray-500 text-center">
                Selecione um paciente para visualizar sua evolução
              </p>
            </div>
          )}
        </div>

        {selectedPatient && (
          <div className="space-y-10">
            <div className="max-w-4xl mx-auto">
              <IMCIndicator patientId={selectedPatient} />
            </div>

            <div className="grid grid-cols-1 md:grid-cols-2 gap-6 max-w-4xl mx-auto">
              <TMBCard patientId={selectedPatient} />
              <GalleryCard />
            </div>

            <EvolutionChartCard patientId={selectedPatient} />
            <MeasurementHistory patientId={selectedPatient} />
          </div>
        )}
      </div>
    </Layout>
  );
}
