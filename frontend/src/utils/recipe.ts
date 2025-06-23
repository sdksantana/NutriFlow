import { RawRecipe, Recipe, FilterOptions, Macro } from "../types/RecipeTypes";
export const placeholders = {
  name: "Nome da Receita",
  calories: "Calorias",
  protein: "Proteínas",
  fat: "Gorduras",
  carbs: "Carboidratos",
  meal: "Refeição",
  preparationTime: "Tempo de Preparo",
  description: "Modo de Preparo",
};

export const units: Record<string, string> = {
  calories: "kcal",
  protein: "P",
  fat: "G",
  carbs: "C",
};

export function getDisplay(key: string, label: string) {
  if (key === "calories") return `${label} ${units[key]}`;
  if (units[key]) return `${units[key]}: ${label}`;
  return label;
}

export function enrichRecipe(raw: RawRecipe, filters: FilterOptions): Recipe {
  const enrich = (macro: keyof FilterOptions, value: string): Macro => {
    const match = filters[macro]?.find((opt) => opt.value === value);
    return match || { label: value, value };
  };

  return {
    id: raw.id,
    name: raw.name,
    description: raw.description,
    preparationTime: enrich("preparationTime", raw.preparationTime),
    calories: enrich("calories", raw.calories),
    protein: enrich("protein", raw.protein),
    fat: enrich("fat", raw.fat),
    carbs: enrich("carbs", raw.carbs),
    meal: enrich("meal", raw.meal),
  };
}
