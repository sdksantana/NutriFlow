import Layout from "../../components/layouts/AppLayout";
import { Search } from "lucide-react";
import { RecipeCard } from "../../components/recipes/RecipeCard";
import { RecipeCardNutritionist } from "../../components/recipes/RecipeCardNutritionist";
import { CreateRecipePopup } from "../../components/recipes/CreateRecipePopup";
import { EditRecipePopup } from "../../components/recipes/EditRecipePopup";
import { RecipeDetailsPopup } from "../../components/recipes/RecipeDetailsPopup";
import { FiltersBar } from "../../components/recipes/FiltersBar";
import { RecipeTab } from "../../components/recipes/RecipeTab";
import { ManageRecipePatientsPopup } from "../../components/recipes/ManageRecipePatientsPopup";
import { useFilterControls } from "../../hooks/useFilterControls";
import { useRecipeSelection } from "../../hooks/useRecipeSelection";
import { useRecipesPage } from "../../hooks/useRecipesPage";
import { useState } from "react";
import { Recipe, RecipeWithLinks } from "../../types/RecipeTypes";
import api from "../../api/axios";
import { Endpoints } from "../../routes/endpoints";
import { toast } from "react-toastify";

interface Props {
  activeTab: "receitas" | "plano";
  setActiveTab: (value: "receitas" | "plano") => void;
  mode: "patient" | "nutritionist";
}

export default function RecipesPage({ activeTab, setActiveTab, mode }: Props) {
  const {
    filterRef,
    openFilter,
    setOpenFilter,
    selectedFilters,
    handleFilterChange,
  } = useFilterControls();

  const {
    recipes,
    setRecipes,
    filterOptions,
    filteredRecipes,
    searchTerm,
    setSearchTerm,
    fetchData,
  } = useRecipesPage(
    mode === "nutritionist" && activeTab === "receitas"
      ? "nutritionist"
      : "patient",
    selectedFilters,
    activeTab
  );

  const {
    selectedRecipe,
    openDetails,
    closeDetails,
    editingRecipe,
    openEdit,
    closeEdit,
    showCreate,
    openCreate,
    closeCreate,
    recipeForManagement,
    openManagePatients,
    openManage,
    closeManage,
  } = useRecipeSelection(fetchData);

  async function handleManagePatients(recipe: Recipe) {
    const nutritionistId = localStorage.getItem("userId");
    if (!nutritionistId) {
      toast.error("Nutricionista não identificado.");
      return;
    }

    try {
      const response = await api.get(
        Endpoints.getNutritionistRecipeById(recipe.id, nutritionistId)
      );
      const recipeWithLinks: RecipeWithLinks = response.data;
      openManage(recipeWithLinks);
    } catch {
      toast.error("Erro ao carregar dados da receita.");
    }
  }

  const noRecipesMessage =
    activeTab === "plano"
      ? "Seu nutricionista ainda não adicionou nenhuma receita ao seu plano alimentar."
      : "Nenhuma receita encontrada.";

  return (
    <Layout>
      <div className="w-full max-w-7xl mx-auto px-6 py-8 min-h-screen bg-orange-50">
        <h1 className="text-3xl font-bold text-gray-800 mb-2">
          Menu de Receitas
        </h1>
        <p className="text-gray-600 mb-6">
          Aqui você pode cadastrar novas receitas e gerenciar o plano alimentar
          dos seus pacientes.
        </p>

        {mode === "patient" && (
          <RecipeTab
            activeTab={activeTab}
            setActiveTab={setActiveTab}
            mode="patient"
          />
        )}

        <hr className="w-3/5 border-t border-gray-300 mb-6" />

        <div className="relative max-w-md mb-6">
          <Search className="absolute left-3 top-1/2 -translate-y-1/2 text-gray-400 w-4 h-4" />
          <input
            type="text"
            placeholder="Pesquisar receita..."
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            className="w-full pl-10 pr-4 py-2 border border-orange-500 rounded-lg focus:ring-2 focus:ring-orange-500 focus:outline-none"
          />
        </div>

        <FiltersBar
          filters={[
            { key: "calories", label: "Calorias" },
            { key: "protein", label: "Proteína" },
            { key: "fat", label: "Gordura" },
            { key: "carbs", label: "Carboidrato" },
            { key: "preparationTime", label: "Tempo" },
            { key: "meal", label: "Refeição" },
          ]}
          filterRef={filterRef}
          filterOptions={filterOptions}
          selectedFilters={selectedFilters}
          openFilter={openFilter}
          setOpenFilter={setOpenFilter}
          onChange={handleFilterChange}
        />

        {filteredRecipes.length === 0 ? (
          <p className="text-center text-gray-600 mt-8">{noRecipesMessage}</p>
        ) : (
          <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6 mb-8">
            {filteredRecipes.map((recipe) => (
              <div key={recipe.id}>
                {mode === "nutritionist" ? (
                  <RecipeCardNutritionist
                    recipe={recipe as RecipeWithLinks}
                    onEdit={() => openEdit(recipe)}
                    onDetails={() => openDetails(recipe)}
                    onManagePatients={() => handleManagePatients(recipe)}
                    patientCount={
                      (recipe as RecipeWithLinks).links?.length || 0
                    }
                  />
                ) : (
                  <RecipeCard
                    recipe={recipe}
                    onEdit={() => openEdit(recipe)}
                    onDetails={() => openDetails(recipe)}
                    hideEdit={activeTab === "plano"}
                  />
                )}
              </div>
            ))}
          </div>
        )}

        {activeTab === "receitas" && (
          <div className="mt-10">
            <button
              className="bg-orange-500 hover:bg-orange-600 text-white px-8 py-3 rounded-xl text-lg font-medium shadow"
              onClick={openCreate}
            >
              Cadastrar Receita
            </button>
          </div>
        )}
      </div>

      {selectedRecipe && (
        <RecipeDetailsPopup
          recipe={selectedRecipe}
          onClose={closeDetails}
          onSuccess={() => fetchData()}
        />
      )}

      {showCreate && (
        <CreateRecipePopup
          onClose={closeCreate}
          onSuccess={() => {
            fetchData();
            closeCreate();
          }}
          filterOptions={filterOptions}
        />
      )}

      {editingRecipe && (
        <EditRecipePopup
          key={`edit-${editingRecipe.id}-${editingRecipe._force ?? 0}`}
          recipe={editingRecipe}
          onClose={closeEdit}
          onSuccess={(updatedRecipe) => {
            if (updatedRecipe) {
              setRecipes((prev: (Recipe | RecipeWithLinks)[]) =>
                prev.map((r) =>
                  r.id === updatedRecipe.id
                    ? {
                        ...r,
                        ...updatedRecipe,
                        links: (r as RecipeWithLinks).links ?? undefined,
                      }
                    : r
                )
              );
            } else {
              fetchData();
            }

            closeEdit();
          }}
          filterOptions={filterOptions}
        />
      )}

      {recipeForManagement && openManagePatients && (
        <ManageRecipePatientsPopup
          recipe={recipeForManagement}
          onClose={closeManage}
          onUpdate={fetchData}
        />
      )}
    </Layout>
  );
}
