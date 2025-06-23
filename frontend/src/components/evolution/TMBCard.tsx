import { useEffect, useState } from "react";
import { Flame } from "lucide-react";
import api from "../../api/axios";
import { getUserIdFromToken } from "../../utils/token";

interface Props {
  patientId?: string;
}

export function TMBCard({ patientId }: Props) {
  const [tmb, setTmb] = useState<number | null>(null);

  useEffect(() => {
    async function fetchTMB() {
      const token = localStorage.getItem("token");
      const fallbackId = getUserIdFromToken(token || "");
      const id = patientId ?? fallbackId;

      if (!id) return;

      try {
        const res = await api.get(`/patients/tmb/${id}`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });
        setTmb(res.data);
      } catch (err) {
        console.error("Erro ao buscar TMB:", err);
      }
    }

    fetchTMB();
  }, [patientId]);

  const isValidTMB = typeof tmb === "number" && tmb > 0;

  return (
    <div className="bg-white rounded-xl shadow border border-gray-200 transition-all duration-300 hover:shadow-lg hover:-translate-y-1">
      <div className="p-8 text-center">
        <div className="flex justify-center mb-4">
          <div className="p-3 bg-orange-100 rounded-full">
            <Flame className="h-8 w-8 text-orange-600" />
          </div>
        </div>

        <h3 className="text-lg font-semibold text-gray-700 mb-2">
          Taxa Metabólica Basal
        </h3>

        <div className="text-4xl font-bold text-gray-900 mb-1">
          {isValidTMB ? `${tmb.toLocaleString()} kcal` : "—"}
        </div>

        <div className="text-lg text-gray-600">kcal/dia</div>

        <div className="text-mg text-gray-600">
          Calorias necessárias em repouso
        </div>
      </div>
    </div>
  );
}
