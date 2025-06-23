import { useState, useEffect } from "react";
import api from "../api/axios";
import { Endpoints } from "../routes/endpoints";
import {
  Recipe,
  RecipeWithLinks,
  RawRecipe,
  FilterOptions,
  Macro,
} from "../types/RecipeTypes";

function enrichRecipe(raw: RawRecipe, filters: FilterOptions): Recipe {
  const enrich = (macro: keyof FilterOptions, value: string): Macro => {
    const match = filters[macro]?.find((opt) => opt.value === value);
    return match || { label: value, value };
  };

  const enriched: Recipe = {
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

  if ((raw as any)._force) {
    (enriched as any)._force = (raw as any)._force;
  }

  return enriched;
}

export function useRecipesPage(
  mode: "patient" | "nutritionist",
  selectedFilters: Record<string, string>,
  activeTab: "receitas" | "plano"
) {
  const userId = localStorage.getItem("userId");

  const [recipes, setRecipes] = useState<(Recipe | RecipeWithLinks)[]>([]);
  const [filterOptions, setFilterOptions] = useState<FilterOptions>({
    meal: [],
    calories: [],
    protein: [],
    fat: [],
    carbs: [],
    preparationTime: [],
  });
  const [filteredRecipes, setFilteredRecipes] = useState<
    (Recipe | RecipeWithLinks)[]
  >([]);
  const [searchTerm, setSearchTerm] = useState("");

  async function fetchFilters(): Promise<FilterOptions> {
    try {
      const res = await api.get(Endpoints.getRecipeFilters);
      const mapping: Record<string, keyof FilterOptions> = {
        Meal: "meal",
        Calories: "calories",
        Protein: "protein",
        Fat: "fat",
        Carbs: "carbs",
        PreparationTime: "preparationTime",
      };

      const formatted: FilterOptions = {
        meal: [],
        calories: [],
        protein: [],
        fat: [],
        carbs: [],
        preparationTime: [],
      };

      res.data.forEach((category: any) => {
        const key = mapping[category.category];
        if (key) formatted[key] = category.filters;
      });

      setFilterOptions(formatted);
      return formatted;
    } catch (err) {
      console.error("Erro ao buscar filtros:", err);
      return filterOptions;
    }
  }

  async function fetchData() {
    if (!userId) return;
    try {
      const filters = await fetchFilters();
      let recipesRes;

      if (mode === "patient") {
        if (activeTab === "receitas") {
          recipesRes = await api.get(Endpoints.getRecipes(userId));
        } else {
          recipesRes = await api.get(Endpoints.getPlanRecipes(userId));
        }

        const enriched = recipesRes.data.map((r: RawRecipe) =>
          enrichRecipe(r, filters)
        );

        setRecipes(enriched);
      } else {
        recipesRes = await api.get(Endpoints.getNutritionistRecipe(userId));

        const enriched = recipesRes.data.map(
          (r: RawRecipe & { links?: any }) => ({
            ...enrichRecipe(r, filters),
            links: r.links || [],
          })
        );
        setRecipes(enriched);
      }
    } catch (err) {
      console.error("Erro ao buscar receitas:", err);
    }
  }

  useEffect(() => {
    fetchData();
  }, [activeTab]);

  useEffect(() => {
    const filtered = recipes.filter((recipe) => {
      const matchesSearch = recipe.name
        .toLowerCase()
        .includes(searchTerm.toLowerCase());

      const matchesFilters = Object.entries(selectedFilters).every(
        ([key, value]) => {
          if (!value) return true;
          const field = recipe[key as keyof Recipe];
          if (typeof field === "object" && field !== null && "value" in field) {
            return field.value === value;
          }
          return true;
        }
      );

      return matchesSearch && matchesFilters;
    });

    setFilteredRecipes(filtered);
  }, [recipes, searchTerm, selectedFilters]);

  return {
    recipes,
    setRecipes,
    filterOptions,
    filteredRecipes,
    searchTerm,
    setSearchTerm,
    fetchData,
  };
}