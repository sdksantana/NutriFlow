import { X, Mail, Calendar, BadgeCheck } from "lucide-react";

interface PatientDetailsModalProps {
  onClose: () => void;
  patientInfo: {
    name: string;
    email: string;
    gender: string;
    startDate: string;
  };
}

export function PatientDetailsModal({
  onClose,
  patientInfo,
}: PatientDetailsModalProps) {
  const genderText =
    patientInfo.gender === "MALE"
      ? "Masculino"
      : patientInfo.gender === "FEMALE"
      ? "Feminino"
      : "Outro";

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
          Detalhes do Paciente
        </h2>

        <p className="text-lg font-semibold text-gray-900 text-center mb-2">
          {patientInfo.name}
        </p>

        <div className="flex items-center gap-2 justify-center text-gray-600 mb-2">
          <Mail className="w-4 h-4" />
          <span>{patientInfo.email}</span>
        </div>

        <div className="flex items-center gap-2 justify-center text-gray-600 mb-2">
          <BadgeCheck className="w-4 h-4" />
          <span>Gênero: {genderText}</span>
        </div>

        <div className="flex items-center gap-2 justify-center text-gray-600 mb-2">
          <Calendar className="w-4 h-4" />
          <span>Vinculado desde: {patientInfo.startDate}</span>
        </div>
      </div>
    </div>
  );
}
