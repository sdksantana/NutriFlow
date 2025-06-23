using NutriFlow.Domain.Entities;
using NutriFlow.Domain.ValueObjects;

namespace NutriFlow.Domain.Repositories
{
    public interface INutritionistPatientRepository
    {
        Task SaveAsync(NutritionistPatient nutritionistPatient);
        Task<NutritionistPatient?> GetByPairAsync(Guid id, Email email);
        Task SaveRecipeAsync(NutritionistPatientRecipe nutritionistPatientRecipe);
        Task DeleteRecipeAsync(NutritionistPatientRecipe nutritionistPatientRecipe);
        Task<NutritionistPatientRecipe?> GetLinkByIdAsync(Guid id);
    }
}
