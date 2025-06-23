import { useEffect, useState } from "react";
import { TrendingUp, TrendingDown } from "lucide-react";
import api from "../../api/axios";
import { getUserIdFromToken } from "../../utils/token";

interface MeasurementItem {
  label: string;
  initial: number;
  current: number;
  diff: number;
}

interface Props {
  patientId?: string;
}

export function MeasurementHistory({ patientId }: Props) {
  const [data, setData] = useState<MeasurementItem[]>([]);

  useEffect(() => {
    const token = localStorage.getItem("token");
    const id = patientId ?? localStorage.getItem("userId");
    if (!id) return;

    async function fetchData() {
      try {
        const res = await api.get(
          `/patients/body-measurements-historic/${id}`,
          {
            headers: { Authorization: `Bearer ${token}` },
          }
        );

        setData(Array.isArray(res.data) ? res.data : []);
      } catch (err) {
        console.error("Erro ao buscar histórico de medições", err);
      }
    }

    fetchData();
  }, [patientId]);

  return (
    <div className="bg-white rounded-xl shadow border border-gray-200 p-6 w-full">
      <h2 className="text-xl font-bold text-gray-600 mb-4">
        Histórico de Medições Corporais
      </h2>

      {data.length === 0 ? (
        <div className="text-center text-gray-600 text-sm bg-orange-50 border border-orange-200 rounded-lg p-6">
          <p className="mb-2">
            <strong className="text-gray-800">
              Nenhum histórico disponível ainda.
            </strong>
            <br />
            Para visualizar a diferença entre medições, é necessário registrar
            mais de uma medição corporal. Atualize seus dados na próxima semana
            para acompanhar seu progresso.
          </p>
          {!patientId && localStorage.getItem("userType") === "1" && (
            <p
              onClick={() => (window.location.href = "/patient/profile")}
              className="text-orange-600 underline cursor-pointer hover:text-orange-700 transition-colors"
            >
              Ir para o perfil e registrar uma nova medição
            </p>
          )}
        </div>
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          {data.map((item) => {
            const isPositive = item.diff > 0;
            return (
              <div
                key={item.label}
                className="bg-gray-50 rounded-lg p-4 border border-gray-200 hover:border-gray-300 transition-colors"
              >
                <div className="flex items-center justify-between mb-2">
                  <h4 className="font-semibold text-gray-600 text-mg">
                    {item.label}
                  </h4>
                  {isPositive ? (
                    <TrendingUp className="w-4 h-4 text-red-500" />
                  ) : (
                    <TrendingDown className="w-4 h-4 text-green-500" />
                  )}
                </div>

                <div className="text-sm space-y-1">
                  <div className="flex justify-between">
                    <span className="text-gray-700 font-semibold">
                      Inicial:
                    </span>
                    <span className="font-semibold text-base text-gray-800">
                      {item.initial}
                    </span>
                  </div>
                  <div className="flex justify-between">
                    <span className="text-gray-700 font-semibold">Atual:</span>
                    <span className="font-semibold text-base text-gray-800">
                      {item.current}
                    </span>
                  </div>
                  <div className="flex justify-between pt-1 border-t border-gray-200">
                    <span className="text-gray-800 font-semibold">
                      Diferença:
                    </span>
                    <span
                      className={`font-bold text-base ${
                        isPositive
                          ? "text-red-600 font-semibold"
                          : "text-green-600"
                      }`}
                    >
                      {item.diff > 0 ? `+${item.diff}` : item.diff}
                    </span>
                  </div>
                </div>
              </div>
            );
          })}
        </div>
      )}
    </div>
  );
}
