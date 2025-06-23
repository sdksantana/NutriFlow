import { Recipe } from "../../types/RecipeTypes";
import { getBadgeColor } from "../../utils/colors";
import { Edit3, Clock } from "lucide-react";

interface Props {
  recipe: Recipe;
  onEdit: (recipe: Recipe) => void;
  onDetails: (recipe: Recipe) => void;
  hideEdit?: boolean;
}

export function RecipeCard({
  recipe,
  onEdit,
  onDetails,
  hideEdit = false,
}: Props) {
  return (
    <div className="bg-white rounded-xl border border-gray-200 shadow-sm p-4">
      <div className="flex items-start justify-between mb-3">
        <h3 className="text-sm font-semibold text-gray-800 leading-tight">
          {recipe.name}
        </h3>

        {!hideEdit && (
          <button
            onClick={() => onEdit(recipe)}
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

      <div className="flex justify-end">
        <button
          onClick={() => onDetails(recipe)}
          className="bg-blue-500 hover:bg-blue-600 text-white text-xs font-medium px-3 py-1 rounded-full"
        >
          Mais detalhes ▶
        </button>
      </div>
    </div>
  );
}
