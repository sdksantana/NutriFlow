import { useEffect, useState } from "react";
import { Recipe, Macro, FilterOptions } from "../../types/RecipeTypes";
import { SelectMenu } from "./SelectMenu";
import { X } from "lucide-react";
import { useRecipeForm } from "../../hooks/useRecipeForm";
import { placeholders, getDisplay } from "../../utils/recipe";
import { toast } from "react-toastify";
import api from "../../api/axios";
import { Endpoints } from "../../routes/endpoints";

interface Props {
  recipe: Recipe;
  onClose: () => void;
  onSuccess: (updated?: Recipe) => void;
  filterOptions: FilterOptions;
}

export function EditRecipePopup({
  recipe,
  onClose,
  onSuccess,
  filterOptions,
}: Props) {
  const { form, setForm, updateField, editedFields } = useRecipeForm(recipe);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    setForm(recipe);
  }, [recipe]);

  function handleMacroSelect(key: keyof FilterOptions, value: string) {
    const selected = filterOptions[key]?.find((opt) => opt.value === value);
    if (!selected) return;
    updateField(key, { label: selected.label, value: selected.value });
  }

  async function handleSave() {
    const userId = localStorage.getItem("userId");
    const userType = localStorage.getItem("userType");
    if (!userId) {
      toast.error("Usuário não encontrado.");
      return;
    }

    const requestBody: Record<string, string> = { id: recipe.id };

    for (const key in editedFields) {
      if (!editedFields[key]) continue;
      const value = form[key as keyof typeof form];
      requestBody[key] =
        typeof value === "object" && value !== null
          ? (value as any).value
          : value;
    }

    const updatedRecipe: Recipe = {
      ...recipe,
      name: form.name,
      description: form.description,
      preparationTime: form.preparationTime,
      calories: form.calories,
      protein: form.protein,
      fat: form.fat,
      carbs: form.carbs,
      meal: form.meal,
    };

    try {
      setLoading(true);

      const url =
        userType === "0"
          ? Endpoints.updateNutritionistRecipe(recipe.id, userId)
          : Endpoints.updateRecipe(recipe.id, userId);
      await api.put(url, requestBody);

      toast.success("Receita atualizada com sucesso!");
      onSuccess(updatedRecipe);
      onClose();
    } catch (error) {
      toast.error("Erro ao atualizar receita.");
    } finally {
      setLoading(false);
    }
  }

  async function handleDelete() {
    const userId = localStorage.getItem("userId");
    const userType = localStorage.getItem("userType");
    if (!userId) {
      toast.error("Usuário não encontrado.");
      return;
    }

    try {
      setLoading(true);

      const url =
        userType === "0"
          ? Endpoints.deleteNutritionistRecipe(recipe.id, userId)
          : Endpoints.deleteRecipe(recipe.id, userId);

      await api.delete(url);
      toast.success("Receita excluída com sucesso!");
      onSuccess();
      onClose();
    } catch (error) {
      toast.error("Erro ao excluir receita.");
    } finally {
      setLoading(false);
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
          Editar Receita
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
              value={(form[key] as Macro)?.value}
              options={filterOptions[key] ?? []}
              onChange={(v: string) => handleMacroSelect(key, v)}
              fieldKey={key}
              placeholder={getDisplay(key, (form[key] as Macro)?.label || "")}
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

        <div className="flex justify-between">
          <button
            onClick={handleDelete}
            disabled={loading}
            className="bg-red-500 hover:bg-red-600 text-white text-sm px-4 py-2 rounded-lg shadow disabled:opacity-50"
          >
            Excluir
          </button>
          <button
            onClick={handleSave}
            disabled={loading}
            className="bg-green-600 hover:bg-green-700 text-white text-sm px-5 py-2 rounded-lg shadow disabled:opacity-50"
          >
            {loading ? "Salvando..." : "Salvar alterações"}
          </button>
        </div>
      </div>
    </div>
  );
}
