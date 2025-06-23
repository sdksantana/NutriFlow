import { RecipeWithLinks } from "../../types/RecipeTypes";
import { getBadgeColor } from "../../utils/colors";
import { Edit3, Clock, Users } from "lucide-react";

interface Props {
  recipe: RecipeWithLinks;
  onEdit: () => void;
  onDetails: () => void;
  onManagePatients: () => void;
  patientCount?: number;
  hideEdit?: boolean;
}

export function RecipeCardNutritionist({
  recipe,
  onEdit,
  onDetails,
  onManagePatients,
  patientCount,
  hideEdit = false,
}: Props) {
  const count = patientCount ?? recipe.links?.length ?? 0;

  return (
    <div className="bg-white rounded-xl border border-gray-200 shadow-sm p-4 hover:shadow-md transition-shadow">
      <div className="flex items-start justify-between mb-3">
        <h3 className="text-sm font-semibold text-gray-800 leading-tight">
          {recipe.name}
        </h3>

        {!hideEdit && (
          <button
            onClick={onEdit}
            className="text-gray-400 hover:text-gray-600"
          >
            <Edit3 className="w-4 h-4" />
          </button>
        )}
      </div>

      <div className="flex items-center gap-1 text-orange-600 text-sm mb-4">
        <Clock className="w-4 h-4" />
        <span className="font-medium">
          {recipe.preparationTime.label || recipe.preparationTime.value}
        </span>
      </div>

      <div className="flex flex-wrap gap-1 mb-3">
        <span
          className={`px-2 py-1 rounded text-xs font-medium ${getBadgeColor(
            recipe.carbs.value,
            "carbs"
          )}`}
        >
          C: {recipe.carbs.label || recipe.carbs.value}
        </span>
        <span
          className={`px-2 py-1 rounded text-xs font-medium ${getBadgeColor(
            recipe.protein.value,
            "protein"
          )}`}
        >
          P: {recipe.protein.label || recipe.protein.value}
        </span>
        <span
          className={`px-2 py-1 rounded text-xs font-medium ${getBadgeColor(
            recipe.fat.value,
            "fat"
          )}`}
        >
          G: {recipe.fat.label || recipe.fat.value}
        </span>
        <span
          className={`px-2 py-1 rounded text-xs font-medium ${getBadgeColor(
            recipe.calories.value,
            "calories"
          )}`}
        >
          {recipe.calories.label || recipe.calories.value} kcal
        </span>
      </div>

      <div className="flex items-center justify-between mt-3 border-t pt-3">
        <button
          onClick={onManagePatients}
          className="w-full flex items-center justify-center gap-2 text-sm px-4 py-1 border border-gray-300 rounded-full text-gray-700 hover:bg-gray-50"
        >
          <Users className="w-4 h-4" />
          Gerenciar Pacientes ({count})
        </button>
      </div>

      <div className="flex justify-end mt-3">
        <button
          onClick={onDetails}
          className="bg-blue-500 hover:bg-blue-600 text-white text-xs font-medium px-3 py-1 rounded-full"
        >
          Mais detalhes ▶
        </button>
      </div>
    </div>
  );
}
