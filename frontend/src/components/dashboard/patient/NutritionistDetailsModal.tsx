import { X, Mail, BadgeCheck, Calendar } from "lucide-react";
import { toast } from "react-toastify";
import api from "../../../api/axios";
import { Endpoints } from "../../../routes/endpoints";
import { getUserTypeFromToken, getUserIdFromToken } from "../../../utils/token";
import { UserType } from "../../../types/UserTypes";

interface NutritionistDetailsModalProps {
  onClose: () => void;
  onUnlink: () => void;
  nutritionistInfo: {
    name: string;
    email: string;
    crn: string;
    startDate: string;
  };
}

export function NutritionistDetailsModal({
  onClose,
  onUnlink,
  nutritionistInfo,
}: NutritionistDetailsModalProps) {
  async function handleUnlink() {
    try {
      const token = localStorage.getItem("token");

      if (!token) {
        toast.error("Token não encontrado.");
        return;
      }

      const userId = getUserIdFromToken(token);
      const userType = getUserTypeFromToken(token);

      if (!userId || userType === null) {
        toast.error("Não foi possível identificar o usuário.");
        return;
      }

      const email = nutritionistInfo.email;

      await api.post(Endpoints.addLink(userId), {
        email,
        isActive: false,
        userType,
      });

      toast.success("Desvinculado com sucesso!");
      onUnlink();
      onClose();
    } catch (error) {
      toast.error("Erro ao desvincular.");
      console.error(error);
    }
  }

  return (
    <div className="fixed inset-0 bg-black bg-opacity-40 flex items-center justify-center z-50">
      <div className="bg-white rounded-xl p-6 w-full max-w-md shadow-lg relative">
        <button
          onClick={onClose}
          className="absolute top-4 right-4 text-gray-400 hover:text-gray-600"
        >
          <X className="w-5 h-5" />
        </button>

        <h2 className="text-2xl font-bold text-center mb-4">
          Detalhes do Nutricionista
        </h2>

        <p className="text-lg font-semibold text-gray-900 text-center mb-2">
          {nutritionistInfo.name}
        </p>

        <div className="flex items-center gap-2 justify-center text-gray-600 mb-2">
          <Mail className="w-4 h-4" />
          <span>{nutritionistInfo.email}</span>
        </div>

        <div className="flex items-center gap-2 justify-center text-gray-600 mb-2">
          <Calendar className="w-4 h-4" />
          <span>Vinculado desde: {nutritionistInfo.startDate}</span>
        </div>

        <div className="flex items-center gap-2 justify-center text-gray-600 mb-6">
          <BadgeCheck className="w-4 h-4" />
          <span>CRN: {nutritionistInfo.crn}</span>
        </div>

        <button
          onClick={handleUnlink}
          className="w-full py-2 rounded-full border border-red-500 text-red-500 hover:bg-red-50 transition"
        >
          Desvincular nutricionista
        </button>
      </div>
    </div>
  );
}
