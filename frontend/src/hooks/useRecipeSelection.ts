// src/hooks/useRecipeSelection.ts

import { useState } from "react";
import { Recipe, RecipeWithLinks } from "../types/RecipeTypes";

type RecipeWithForce = Recipe & { _force?: number };

export function useRecipeSelection(onRefresh: () => void) {
  const [selectedRecipe, setSelectedRecipe] = useState<Recipe | null>(null);
  const [editingRecipe, setEditingRecipe] = useState<RecipeWithForce | null>(null);
  const [recipeForManagement, setRecipeForManagement] = useState<RecipeWithLinks | null>(null);
  const [showCreate, setShowCreate] = useState(false);
  const [openManagePatients, setOpenManagePatients] = useState(false);

  const openEdit = (recipe: Recipe) => {
    setEditingRecipe(null); // Garante re-render
    setTimeout(() => {
      setEditingRecipe({ ...recipe, _force: Date.now() });
    }, 0);
  };

  const openManage = (recipe: RecipeWithLinks) => {
    setRecipeForManagement(recipe);
    setTimeout(() => setOpenManagePatients(true), 0);
  };

  return {
    selectedRecipe,
    openDetails: (recipe: Recipe) => setSelectedRecipe(recipe),
    closeDetails: () => setSelectedRecipe(null),

    editingRecipe,
    openEdit,
    closeEdit: () => setEditingRecipe(null),

    showCreate,
    openCreate: () => setShowCreate(true),
    closeCreate: () => setShowCreate(false),

    recipeForManagement,
    openManagePatients,
    openManage,
    updateManagedRecipeWithLinks: (updated: RecipeWithLinks) =>
      setRecipeForManagement(updated),
    closeManage: () => {
      setRecipeForManagement(null);
      setOpenManagePatients(false);
    },

    onRefresh,
  };
}
