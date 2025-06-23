import { Macro, FilterOptions, Recipe } from "../../types/RecipeTypes";
import { SelectMenu } from "./SelectMenu";
import { X } from "lucide-react";
import api from "../../api/axios";
import { toast } from "react-toastify";
import { Endpoints } from "../../routes/endpoints";
import { useRecipeForm } from "../../hooks/useRecipeForm";
import { placeholders } from "../../utils/recipe";
import { useState } from "react";

interface Props {
  onClose: () => void;
  onSuccess: () => void;
  filterOptions: FilterOptions;
}

const emptyRecipe: Omit<Recipe, "id"> = {
  name: "",
  preparationTime: { label: "", value: "" },
  calories: { label: "", value: "" },
  protein: { label: "", value: "" },
  fat: { label: "", value: "" },
  carbs: { label: "", value: "" },
  meal: { label: "", value: "" },
  description: "",
};

export function CreateRecipePopup({
  onClose,
  onSuccess,
  filterOptions,
}: Props) {
  const { form, updateField, editedFields } = useRecipeForm(emptyRecipe);
  const [saving, setSaving] = useState(false);

  function handleMacroSelect(key: keyof FilterOptions, value: string) {
    const selected = filterOptions[key]?.find((opt) => opt.value === value);
    if (!selected) return;
    updateField(key, { label: selected.label, value: selected.value });
  }

  async function handleSave() {
    const requiredFields = Object.keys(emptyRecipe);
    const missing = requiredFields.filter((key) => {
      const value = form[key as keyof typeof form];
      return typeof value === "string"
        ? !value.trim()
        : !(value as Macro).value;
    });

    if (missing.length > 0) {
      toast.error("Preencha todos os campos obrigatórios.");
      return;
    }

    const recipeToSend = {
      name: form.name,
      description: form.description,
      preparationTime: form.preparationTime.value,
      calories: form.calories.value,
      protein: form.protein.value,
      fat: form.fat.value,
      carbs: form.carbs.value,
      meal: form.meal.value,
    };

    try {
      setSaving(true);
      const userId = localStorage.getItem("userId");
      const userType = localStorage.getItem("userType");

      if (!userId || !userType) {
        toast.error("Usuário não identificado.");
        return;
      }

      const endpoint =
        Number(userType) === 0
          ? Endpoints.createNutritionistRecipe(userId)
          : Endpoints.createRecipe(userId);

      await api.post(endpoint, recipeToSend);
      toast.success("Receita salva com sucesso!");
      onSuccess();
      onClose();
    } catch (err: any) {
      const backendErrors = err.response?.data?.errors;
      if (Array.isArray(backendErrors)) {
        backendErrors.forEach((e: any) => {
          if (typeof e.message === "string") toast.error(e.message);
        });
      } else {
        toast.error("Erro ao salvar a receita.");
      }
    } finally {
      setSaving(false);
    }
  }

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/40 px-2">
      <div className="bg-white rounded-2xl border border-gray-200 p-6 sm:p-8 w-full max-w-md shadow-xl relative">
        <button
          onClick={onClose}
          className="absolute top-4 right-4 text-gray-400 hover:text-gray-600"
        >
          <X className="w-5 h-5" />
        </button>

        <h2 className="text-xl font-semibold text-gray-800 mb-4">
          Cadastrar Receita
        </h2>

        <input
          className="w-full bg-white border border-gray-300 rounded-full px-4 py-2 mb-6 text-sm text-gray-800 placeholder-gray-400 outline-none focus:ring-2 focus:ring-orange-500"
          value={form.name}
          onChange={(e) => updateField("name", e.target.value)}
          placeholder={placeholders.name}
        />

        <div className="grid grid-cols-2 gap-4 mb-6">
          {(
            [
              "calories",
              "protein",
              "fat",
              "carbs",
              "preparationTime",
              "meal",
            ] as (keyof FilterOptions)[]
          ).map((key) => (
            <SelectMenu
              key={key}
              value={(form[key] as Macro).value}
              options={filterOptions[key] ?? []}
              onChange={(v: string) => handleMacroSelect(key, v)}
              fieldKey={key}
              placeholder={placeholders[key]}
              editedFields={editedFields}
            />
          ))}
        </div>

        <div className="bg-gray-50 border border-gray-200 rounded-xl p-4 mb-6">
          <label className="block text-sm font-medium text-gray-500 mb-1">
            {placeholders.description}
          </label>
          <textarea
            className={`w-full bg-transparent outline-none text-sm resize-none placeholder-gray-400 ${
              editedFields["description"] ? "text-gray-800" : "text-gray-400"
            }`}
            rows={3}
            value={form.description}
            onChange={(e) => updateField("description", e.target.value)}
            placeholder="Ex: Adicione 5 queijos..."
          />
        </div>

        <div className="mt-4 flex justify-center">
          <button
            onClick={handleSave}
            disabled={saving}
            className="bg-orange-500 hover:bg-orange-600 text-white text-sm px-6 py-2 rounded-xl font-medium shadow disabled:opacity-50"
          >
            {saving ? "Salvando..." : "Salvar Receita"}
          </button>
        </div>
      </div>
    </div>
  );
}
