"use client";

import Layout from "../../../components/layouts/AppLayout";
import { useEffect, useState } from "react";
import api from "../../../api/axios";
import { Endpoints } from "../../../routes/endpoints";
import { getUserIdFromToken } from "../../../utils/token";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

type FormState = {
  [key: string]: string;
};

export default function ProfilePage() {
  const initialForm: FormState = {
    age: "",
    height: "",
    weight: "",
    neck: "",
    chest: "",
    waist: "",
    abdomen: "",
    hip: "",
    rightArm: "",
    leftArm: "",
    rightForearm: "",
    leftForearm: "",
    rightThigh: "",
    leftThigh: "",
    rightCalf: "",
    leftCalf: "",
  };

  const [form, setForm] = useState<FormState>(initialForm);
  const [original, setOriginal] = useState<FormState>(initialForm);
  const [isPost, setIsPost] = useState(true);
  const [loading, setLoading] = useState(true);
  const [hasChanged, setHasChanged] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    const token = localStorage.getItem("token") || "";
    const userId = getUserIdFromToken(token);
    if (!userId) return;

    api
      .get(Endpoints.getMeasurements(userId), {
        headers: { Authorization: `Bearer ${token}` },
        validateStatus: () => true,
      })
      .then((res) => {
        if (res.status === 200 && res.data) {
          const parsed = Object.fromEntries(
            Object.entries(res.data).map(([k, v]) => [
              k,
              v === null ? "" : String(v),
            ])
          ) as FormState;
          setForm(parsed);
          setOriginal(parsed);
          setIsPost(false);
        }
      })
      .catch(() => {
        toast.error("Erro ao buscar os dados.");
      })
      .finally(() => setLoading(false));
  }, []);

  function handleChange(e: React.ChangeEvent<HTMLInputElement>) {
    setForm({ ...form, [e.target.name]: e.target.value });
    setHasChanged(true);
  }

  function isDisabled(): boolean {
    if (!hasChanged) return true;

    if (isPost) {
      const required = ["age", "height", "weight"];
      return required.some((k) => {
        const val = form[k];
        const num = Number(val);
        return val === "" || isNaN(num) || num <= 0;
      });
    }

    return Object.values(form).some((v) => v === "0");
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    const token = localStorage.getItem("token") || "";
    const userId = getUserIdFromToken(token);
    if (!userId) return;

    const cleaned = Object.fromEntries(
      Object.entries(form).map(([k, v]) => [k, v === "" ? null : Number(v)])
    );

    const endpoint = isPost
      ? Endpoints.postMeasurements(userId)
      : Endpoints.putMeasurements(userId);

    try {
      await api[isPost ? "post" : "put"](endpoint, cleaned, {
        headers: { Authorization: `Bearer ${token}` },
      });
      toast.success("Dados salvos com sucesso!");
      if (isPost) navigate("/patient/dashboard");
      setOriginal(form);
      setHasChanged(false);
    } catch (err: any) {
      const backendErrors = err.response?.data?.errors;
      if (Array.isArray(backendErrors)) {
        backendErrors.forEach((e: string) => toast.error(e));
      } else {
        toast.error("Erro ao salvar os dados.");
      }
    }
  }

  const fields = [
    { name: "age", label: "Idade" },
    { name: "height", label: "Altura (cm)" },
    { name: "weight", label: "Peso (kg)" },
    { name: "neck", label: "Pescoço" },
    { name: "chest", label: "Tórax" },
    { name: "waist", label: "Cintura" },
    { name: "abdomen", label: "Abdômen" },
    { name: "hip", label: "Quadril" },
    { name: "rightArm", label: "Braço direito" },
    { name: "leftArm", label: "Braço esquerdo" },
    { name: "rightForearm", label: "Antebraço direito" },
    { name: "leftForearm", label: "Antebraço esquerdo" },
    { name: "rightThigh", label: "Coxa direita" },
    { name: "leftThigh", label: "Coxa esquerda" },
    { name: "rightCalf", label: "Panturrilha direita" },
    { name: "leftCalf", label: "Panturrilha esquerda" },
  ];

  return (
    <Layout hideMenu={isPost}>
      <div className="w-full max-w-5xl mx-auto">
        <h2 className="text-3xl font-bold mb-2">Cadastro Circunferências</h2>

        {isPost ? (
          <p className="text-orange-600 mb-6 text-sm">
            No primeiro acesso, idade, altura e peso são obrigatórios. Valores
            zerados não são permitidos.
          </p>
        ) : (
          <p className="text-orange-600 mb-4 text-sm">
            Os campos abaixo mostram o último dado salvo. Ao alterar algum
            valor, clique em <b>Salvar</b> para registrar a atualização.
          </p>
        )}

        {!loading && (
          <form
            onSubmit={handleSubmit}
            className="bg-white border rounded-xl p-6"
          >
            {isPost && isDisabled() && (
              <p className="text-red-600 mt-2 mb-4 font-medium text-sm">
                Idade, altura e peso são obrigatórios. Nenhum valor pode ser
                zero.
              </p>
            )}
            {!isPost && Object.values(form).some((v) => v === "0") && (
              <p className="text-red-600 mt-2 mb-4 font-medium text-sm">
                Nenhum valor pode ser zero.
              </p>
            )}

            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4">
              {fields.map(({ name, label }) => {
                const val = form[name];
                const isEdited = val !== "" && val !== original[name];
                return (
                  <div key={name} className="flex flex-col w-full">
                    <label className="mb-1 text-sm font-medium text-gray-700">
                      {label}
                    </label>
                    <input
                      name={name}
                      type="number"
                      value={val}
                      onChange={handleChange}
                      className={`bg-gray-50 border border-gray-200 rounded-lg px-4 py-3 w-full ${
                        isEdited ? "text-black" : "text-gray-400"
                      }`}
                    />
                  </div>
                );
              })}
            </div>

            <button
              type="submit"
              disabled={isDisabled()}
              className={`mt-6 font-bold py-2 px-6 rounded-lg transition ${
                isDisabled()
                  ? "bg-gray-300 text-gray-500 cursor-not-allowed"
                  : "bg-orange-500 hover:bg-orange-600 text-white"
              }`}
            >
              Salvar
            </button>
          </form>
        )}
      </div>
    </Layout>
  );
}
