// utils/mockedFilters.ts
import { FilterOptions } from "../types/RecipeTypes";

export const mockedFilterOptions: FilterOptions = {
  meal: [
    { label: "Café da manhã", value: "breakfast" },
    { label: "Almoço", value: "lunch" },
    { label: "Jantar", value: "dinner" },
    { label: "Pré-treino", value: "preworkout" },
    { label: "Pós-treino", value: "postworkout" },
  ],
  calories: [
    { label: "0-99 kcal", value: "0-99" },
    { label: "100-199 kcal", value: "100-199" },
    { label: "200-299 kcal", value: "200-299" },
    { label: "300-399 kcal", value: "300-399" },
    { label: "400-499 kcal", value: "400-499" },
    { label: "500+ kcal", value: "500+" },
  ],
  protein: [
    { label: "0-10g", value: "0-10" },
    { label: "11-20g", value: "11-20" },
    { label: "21-30g", value: "21-30" },
    { label: "30g+", value: "30+" },
  ],
  fat: [
    { label: "0-5g", value: "0-5" },
    { label: "6-10g", value: "6-10" },
    { label: "11-15g", value: "11-15" },
    { label: "16-20g", value: "16-20" },
    { label: "20g+", value: "20+" },
  ],
  carbs: [
    { label: "0-10g", value: "0-10" },
    { label: "11-20g", value: "11-20" },
    { label: "21-30g", value: "21-30" },
    { label: "31-40g", value: "31-40" },
    { label: "41-50g", value: "41-50" },
    { label: "50g+", value: "50+" },
  ],
  preparationTime: [
    { label: "5min", value: "5min" },
    { label: "10min", value: "10min" },
    { label: "15min", value: "15min" },
    { label: "20min", value: "20min" },
    { label: "30min", value: "30min" },
  ],
};
