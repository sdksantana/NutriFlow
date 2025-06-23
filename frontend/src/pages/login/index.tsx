import { useForm } from "react-hook-form";
import { useNavigate, Link } from "react-router-dom";
import api from "../../api/axios";
import { toast } from "react-toastify";
import { translateError } from "../../utils/translateErrors";
import React, { useState } from "react";
import { Eye, EyeOff } from "lucide-react";
import { getUserIdFromToken, getUserTypeFromToken } from "../../utils/token";
import { Endpoints } from "../../routes/endpoints";
import { UserType } from "../../types/UserTypes";

export default function LoginPage() {
  const { register, handleSubmit } = useForm();
  const navigate = useNavigate();
  const [showPassword, setShowPassword] = useState(false);

  async function onSubmit(data: any) {
    try {
      const response = await api.post(Endpoints.login, data);
      const token = response.data.token;

      localStorage.setItem("token", token);

      const userId = getUserIdFromToken(token);
      const userType = getUserTypeFromToken(token);

      if (!userId || userType === null) {
        toast.error("Erro ao processar as informações do usuário.");
        return;
      }

      localStorage.setItem("userId", userId);
      localStorage.setItem("userType", userType.toString());
      toast.success("Login realizado com sucesso!");

      if (userType === UserType.PATIENT) {
        const res = await api.get(Endpoints.getMeasurements(userId), {
          headers: {
            Authorization: `Bearer ${token}`,
          },
          validateStatus: () => true,
        });

        if (res.status === 204) {
          navigate("/patient/profile");
        } else {
          navigate("/patient/dashboard");
        }
      } else {
        navigate("/nutritionist/dashboard");
      }
    } catch (err: any) {
      const backendErrors = err.response?.data?.errors;

      if (Array.isArray(backendErrors)) {
        backendErrors.forEach((e: string) => toast.error(translateError(e)));
      } else {
        toast.error("Email ou senha inválidos.");
      }
    }
  }

  return (
    <div className="min-h-screen flex items-center justify-center bg-orange-50">
      <div className="bg-white px-8 py-14 rounded-xl shadow-md border border-gray-200 w-full max-w-md h-screen sm:h-auto sm:min-h-[580px] flex flex-col justify-center">
        <h1 className="text-center text-2xl font-bold text-gray-900 flex items-center gap-2 justify-center mb-12">
          <span className="w-6 h-6 rounded-full bg-orange-500 inline-block"></span>
          NutriFlow
        </h1>

        <form onSubmit={handleSubmit(onSubmit)} className="space-y-8">
          <div>
            <label className="text-sm font-medium text-gray-800 block mb-1">
              Email
            </label>
            <input
              {...register("email", { required: true })}
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
              {...register("password", { required: true })}
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

          <button
            type="submit"
            className="w-full bg-orange-500 hover:bg-orange-600 text-white text-sm font-semibold py-2 rounded transition"
          >
            Login
          </button>

          <p className="text-center text-sm text-gray-600">
            Ainda não possui um cadastro?{" "}
            <Link
              to="/register"
              className="text-orange-500 hover:underline font-medium"
            >
              Cadastre-se
            </Link>
          </p>
        </form>
      </div>
    </div>
  );
}
