import { Edit3, User, Mail, Calendar, BadgeCheck, Eye } from "lucide-react";

interface PatientCardPreviewProps {
  name: string;
  email: string;
  gender: string;
  startDate: string;
  onClick: () => void;
}

export function PatientCardPreview({
  name,
  email,
  gender,
  startDate,
  onClick,
}: PatientCardPreviewProps) {
  const genderText =
    gender === "MALE"
      ? "Masculino"
      : gender === "FEMALE"
      ? "Feminino"
      : "Outro";

  return (
    <div
      onClick={onClick}
      className="bg-white rounded-xl shadow border border-gray-200 transition-all duration-300 hover:shadow-lg hover:-translate-y-1 cursor-pointer group relative flex flex-col justify-center items-center h-44 w-70 p-4"
    >
      <div className="absolute top-4 right-4 bg-gray-100 p-2 rounded-full group-hover:bg-gray-200">
        <Eye className="w-5 h-5 text-gray-600 group-hover:text-gray-800" />
      </div>

      <div className="w-12 h-12 bg-orange-100 rounded-lg flex items-center justify-center mb-2">
        <User className="w-6 h-6 text-orange-600" />
      </div>
      <p className="text-gray-600 text-mg mb-2">Paciente mais engajado</p>
      <p className="text-xl font-semibold text-gray-900 text-center">{name}</p>
    </div>
  );
}
