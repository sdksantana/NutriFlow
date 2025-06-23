import { Edit3, User } from "lucide-react";

interface NutritionistCardPreviewProps {
  name: string;
  onClick: () => void;
}

export function NutritionistCardPreview({
  name,
  onClick,
}: NutritionistCardPreviewProps) {
  return (
    <div
      onClick={onClick}
      className="bg-white rounded-xl shadow border border-gray-200 transition-all duration-300 hover:shadow-lg hover:-translate-y-1 cursor-pointer group relative flex flex-col justify-center items-center h-44 w-70 p-4"
    >
      <div className="absolute top-4 right-4 bg-gray-100 p-2 rounded-full group-hover:bg-gray-200">
        <Edit3 className="w-5 h-5 text-gray-600 group-hover:text-gray-800" />
      </div>

      <div className="w-12 h-12 bg-orange-100 rounded-lg flex items-center justify-center mb-2">
        <User className="w-6 h-6 text-orange-600" />
      </div>
      <p className="text-gray-600 text-mg mb-2">Nutricionista vinculado</p>
      <p className="text-xl font-semibold text-gray-900">{name}</p>
    </div>
  );
}
