import { Recipe } from "../../types/RecipeTypes";
import { X } from "lucide-react";
import { placeholders, getDisplay } from "../../utils/recipe";

interface Props {
  recipe: Recipe;
  onClose: () => void;
  onSuccess: () => void;
}

export function RecipeDetailsPopup({ recipe, onClose }: Props) {
  const infoFields: (keyof typeof placeholders)[] = [
    "calories",
    "protein",
    "fat",
    "carbs",
    "meal",
    "preparationTime",
  ];

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/40 px-4 overflow-y-auto">
      <div className="bg-white rounded-2xl border border-gray-200 p-6 sm:p-8 w-full max-w-md shadow-xl relative mt-10 mb-10">
        <button
          className="absolute top-4 right-4 text-gray-400 hover:text-gray-600"
          onClick={onClose}
          aria-label="Fechar"
        >
          <X className="w-5 h-5" />
        </button>

        <h2 className="text-xl font-semibold text-center text-gray-800 mb-6">
          {recipe.name}
        </h2>

        <div className="grid grid-cols-2 gap-4 text-sm text-gray-700 mb-6">
          {infoFields.map((key) => {
            const field = recipe[key];
            const value =
              typeof field === "object" && field !== null && "label" in field
                ? field.label
                : "-";
            return (
              <div key={key}>
                <span className="block text-gray-600 font-medium mb-1">
                  {placeholders[key]}
                </span>
                <span className="text-gray-600 font-medium">
                  {getDisplay(key, value)}
                </span>
              </div>
            );
          })}
        </div>

        <div className="bg-gray-50 border border-gray-200 rounded-xl p-4">
          <h3 className="text-sm font-medium text-gray-500 mb-2">
            {placeholders.description}
          </h3>
          <p className="text-sm text-gray-700 whitespace-pre-line">
            {recipe.description}
          </p>
        </div>
      </div>
    </div>
  );
}
