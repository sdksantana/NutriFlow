import { useEffect, useState } from "react";
import {
  LineChart,
  Line,
  XAxis,
  YAxis,
  Tooltip,
  ResponsiveContainer,
  CartesianGrid,
} from "recharts";
import { ChartBar } from "lucide-react";
import api from "../../api/axios";
import { Endpoints } from "../../routes/endpoints";

interface EvolutionData {
  date: string;
  weight: number | null;
  neck: number | null;
  chest: number | null;
  waist: number | null;
  abdomen: number | null;
  hip: number | null;
  rightArm: number | null;
  leftArm: number | null;
  rightForearm: number | null;
  leftForearm: number | null;
  rightThigh: number | null;
  leftThigh: number | null;
  rightCalf: number | null;
  leftCalf: number | null;
}

interface Props {
  patientId?: string;
}

const metricOptions = [
  { key: "weight", label: "Peso" },
  { key: "waist", label: "Cintura" },
  { key: "chest", label: "Tórax" },
  { key: "hip", label: "Quadril" },
  { key: "neck", label: "Pescoço" },
  { key: "abdomen", label: "Abdômen" },
  { key: "rightArm", label: "Braço direito" },
  { key: "leftArm", label: "Braço esquerdo" },
  { key: "rightForearm", label: "Antebraço direito" },
  { key: "leftForearm", label: "Antebraço esquerdo" },
  { key: "rightThigh", label: "Coxa direita" },
  { key: "leftThigh", label: "Coxa esquerda" },
  { key: "rightCalf", label: "Panturrilha direita" },
  { key: "leftCalf", label: "Panturrilha esquerda" },
];

function CustomTooltip({ active, payload, label }: any) {
  if (active && payload?.length) {
    const date = new Date(label).toLocaleDateString("pt-BR");
    const value = payload[0].value;
    const key = payload[0].name;

    const labelMap: Record<string, string> = Object.fromEntries(
      metricOptions.map((opt) => [opt.key, opt.label])
    );

    return (
      <div className="bg-white p-2 border rounded shadow text-sm">
        <p className="text-gray-800">{date}</p>
        <p className="text-orange-500 font-semibold">
          {labelMap[key] || key}: {value}
        </p>
      </div>
    );
  }

  return null;
}

export function EvolutionChartCard({ patientId }: Props) {
  const [data, setData] = useState<EvolutionData[]>([]);
  const [selectedMetric, setSelectedMetric] = useState("weight");

  useEffect(() => {
    const id = patientId ?? localStorage.getItem("userId");
    if (!id) return;

    api
      .get(Endpoints.getBodyMeasurementEvolution(id))
      .then((res) => setData(res.data))
      .catch((err) => console.error("Erro ao buscar evolução:", err));
  }, [patientId]);

  const hasRelevantChange = () => {
    if (data.length < 2) return false;
    const values = data.map(
      (entry) => entry[selectedMetric as keyof EvolutionData]
    );
    const first = values[0];
    return values.some((v) => v !== first);
  };

  const showEmptyState = data.length < 2 || !hasRelevantChange();

  return (
    <div className="rounded-xl border border-gray-200 bg-white p-6 shadow w-full">
      <div className="flex items-center justify-between mb-4">
        <div className="flex gap-2 items-center">
          <div className="p-3 bg-gray-100 rounded-full">
            <ChartBar className="h-6 w-6 text-gray-600" />
          </div>
          <h3 className="text-lg font-semibold text-gray-900">
            Gráfico de Evolução
          </h3>
        </div>

        <select
          value={selectedMetric}
          onChange={(e) => setSelectedMetric(e.target.value)}
          className="border border-gray-300 rounded px-3 py-1 text-sm"
        >
          {metricOptions.map((opt) => (
            <option key={opt.key} value={opt.key}>
              {opt.label}
            </option>
          ))}
        </select>
      </div>

      {showEmptyState ? (
        <div className="text-center text-gray-600 text-sm bg-orange-50 border border-orange-200 rounded-lg p-6">
          <p className="mb-2">
            <strong className="text-gray-800">
              Nenhuma evolução disponível ainda.
            </strong>
            <br />
            Para visualizar o gráfico de evolução, é necessário registrar mais
            de uma medição corporal. Atualize seus dados na próxima semana para
            acompanhar seu progresso.
          </p>
          {localStorage.getItem("userType") === "1" && (
            <p
              onClick={() => (window.location.href = "/patient/profile")}
              className="text-orange-600 underline cursor-pointer hover:text-orange-700 transition-colors"
            >
              Ir para o perfil e registrar uma nova medição
            </p>
          )}
        </div>
      ) : (
        <div className="h-72">
          <ResponsiveContainer width="100%" height="100%">
            <LineChart data={data}>
              <CartesianGrid strokeDasharray="3 3" />
              <XAxis
                dataKey="date"
                tickFormatter={(d) => new Date(d).toLocaleDateString("pt-BR")}
              />
              <YAxis />
              <Tooltip content={<CustomTooltip />} />
              <Line
                type="monotone"
                dataKey={selectedMetric}
                stroke="#f97316"
                strokeWidth={2}
                dot={{ r: 4 }}
                activeDot={{ r: 6 }}
              />
            </LineChart>
          </ResponsiveContainer>
        </div>
      )}
    </div>
  );
}
