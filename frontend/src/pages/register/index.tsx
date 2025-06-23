import { useForm } from "react-hook-form";
import { useState } from "react";
import api from "../../api/axios";
import { Link } from "react-router-dom";
import { Gender, UserType } from "../../types/UserTypes";
import { GenderLabels, UserTypeLabels } from "../../types/EnumLabels";
import { translateBackendErrors } from "../../utils/translateErrors";
import { toast } from "react-toastify";
import { Eye, EyeOff } from "lucide-react";

export default function RegisterPage() {
  const { register, handleSubmit, watch } = useForm();
  const userTypeValue = watch("userType");
  const isNutri =
    userTypeValue !== "" && Number(userTypeValue) === UserType.NUTRITIONIST;
  const [sending, setSending] = useState(false);
  const [showPassword, setShowPassword] = useState(false);

  async function onSubmit(data: any) {
    setSending(true);
    const body = {
      email: data.email,
      password: data.password,
      userType: parseInt(data.userType),
      ...(parseInt(data.userType) === UserType.NUTRITIONIST
        ? {
            nutritionist: {
              firstName: data.firstName,
              lastName: data.lastName,
              gender: parseInt(data.gender),
              crn: data.crn,
            },
          }
        : {
            patient: {
              firstName: data.firstName,
              lastName: data.lastName,
              gender: parseInt(data.gender),
            },
          }),
    };
    try {
      await api.post("/users/register", body);
      toast.success("Cadastro realizado com sucesso! Faça login.");
      window.location.href = "/login";
    } catch (err: any) {
      const backendErrors = err.response?.data?.errors;
      const msg = translateBackendErrors(backendErrors);
      toast.error(msg);
    } finally {
      setSending(false);
    }
  }

  return (
    <div className="min-h-screen flex items-center justify-center bg-orange-50">
      <div className="bg-white px-8 py-12 rounded-xl shadow-md border border-gray-200 w-full max-w-md min-h-[580px] sm:min-h-[620px] flex flex-col justify-center">
        <h1 className="text-center text-2xl font-bold text-gray-900 flex items-center gap-2 justify-center mb-12">
          <span className="w-6 h-6 rounded-full bg-orange-500 inline-block"></span>
          NutriFlow
        </h1>

        <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
          <div>
            <label className="text-sm font-medium text-gray-800 block mb-1">
              Email
            </label>
            <input
              {...register("email")}
              type="email"
              placeholder="Digite seu email"
              className="w-full border border-gray-300 rounded px-3 py-2 text-sm placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-orange-400"
            />
          </div>

          <div className="relative">
            <label className="text-sm font-medium text-gray-800 block mb-1">
              Senha
            </label>
            <input
              {...register("password")}
              type={showPassword ? "text" : "password"}
              placeholder="Digite sua senha"
              className="w-full border border-gray-300 rounded px-3 py-2 pr-10 text-sm placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-orange-400"
            />
            <button
              type="button"
              onClick={() => setShowPassword(!showPassword)}
              className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-400 hover:text-gray-600"
            >
              {showPassword ? (
                <EyeOff className="h-4 w-4" />
              ) : (
                <Eye className="h-4 w-4" />
              )}
            </button>
          </div>

          <div>
            <label className="text-sm font-medium text-gray-800 block mb-1">
              Nome
            </label>
            <input
              {...register("firstName")}
              placeholder="Digite seu nome"
              className="w-full border border-gray-300 rounded px-3 py-2 text-sm placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-orange-400"
            />
          </div>

          <div>
            <label className="text-sm font-medium text-gray-800 block mb-1">
              Sobrenome
            </label>
            <input
              {...register("lastName")}
              placeholder="Digite seu sobrenome"
              className="w-full border border-gray-300 rounded px-3 py-2 text-sm placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-orange-400"
            />
          </div>

          <div>
            <label className="text-sm font-medium text-gray-800 block mb-1">
              Sexo
            </label>
            <select
              {...register("gender")}
              className="w-full border border-gray-300 rounded px-3 py-2 text-sm text-gray-500 focus:outline-none focus:ring-2 focus:ring-orange-400"
            >
              <option value="">Selecione o sexo</option>
              {Object.entries(GenderLabels).map(([value, label]) => (
                <option key={value} value={value}>
                  {label}
                </option>
              ))}
            </select>
          </div>

          <div>
            <label className="text-sm font-medium text-gray-800 block mb-1">
              Tipo de usuário
            </label>
            <select
              {...register("userType")}
              className="w-full border border-gray-300 rounded px-3 py-2 text-sm text-gray-500 focus:outline-none focus:ring-2 focus:ring-orange-400"
            >
              <option value="">Selecione o tipo</option>
              {Object.entries(UserTypeLabels).map(([value, label]) => (
                <option key={value} value={value}>
                  {label}
                </option>
              ))}
            </select>
          </div>

          {isNutri && (
            <div>
              <label className="text-sm font-medium text-gray-800 block mb-1">
                CRN
              </label>
              <input
                {...register("crn")}
                placeholder="Digite seu CRN"
                className="w-full border border-gray-300 rounded px-3 py-2 text-sm placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-orange-400"
              />
            </div>
          )}

          <button
            type="submit"
            disabled={sending}
            className="w-full bg-orange-500 hover:bg-orange-600 text-white text-sm font-semibold py-2 rounded transition"
          >
            {sending ? "Enviando..." : "Cadastrar"}
          </button>

          <p className="text-center text-sm text-gray-600">
            Já possui uma conta?{" "}
            <Link
              to="/login"
              className="text-orange-500 hover:underline font-medium"
            >
              Login
            </Link>
          </p>
        </form>
      </div>
    </div>
  );
}
