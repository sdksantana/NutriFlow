import { useEffect, useState } from "react";
import { IMCBar } from "./IMCBar";
import api from "../../api/axios";
import { getUserIdFromToken } from "../../utils/token";

interface Props {
  patientId?: string;
}

export function IMCIndicator({ patientId }: Props) {
  const [imc, setImc] = useState<number | null>(null);

  useEffect(() => {
    const token = localStorage.getItem("token");
    const id = patientId ?? localStorage.getItem("userId");
    if (!id) return;

    async function fetchIMC() {
      try {
        const res = await api.get(`/patients/body-measurements/${id}`, {
          headers: { Authorization: `Bearer ${token}` },
        });

        const { weight, height } = res.data;
        if (weight > 0 && height > 0) {
          const calculated = weight / Math.pow(height / 100, 2);
          setImc(parseFloat(calculated.toFixed(1)));
        }
      } catch (err) {
        console.error("Erro ao calcular IMC:", err);
      }
    }

    fetchIMC();
  }, [patientId]);

  if (imc === null) return null;

  return <IMCBar imc={imc} />;
}
