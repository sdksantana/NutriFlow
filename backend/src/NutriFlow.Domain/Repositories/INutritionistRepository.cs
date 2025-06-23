using NutriFlow.Domain.Entities;
using NutriFlow.Domain.ValueObjects;

namespace NutriFlow.Domain.Repositories
{
    public interface INutritionistRepository
    {
        Task<Guid> SaveAsync(Nutritionist nutritionist);
        Task<Nutritionist?> GetByIdAsync(Guid id);
        Task<Nutritionist?> GetByEmailAsync(Email email);
        Task<Guid> SaveRecipeAsync(NutritionistRecipe nutritionistRecipe);
        Task<NutritionistRecipe?> GetRecipeByIdAsync(Guid nutritionistId, Guid id);
        Task DeleteRecipeAsync(NutritionistRecipe nutritionistRecipe);
    }
}
