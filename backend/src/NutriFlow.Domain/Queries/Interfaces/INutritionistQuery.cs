using NutriFlow.Domain.Queries.Requests;
using NutriFlow.Domain.Queries.Responses.Nutritionist;
using NutriFlow.Domain.Queries.Responses.NutritionistPatient;

namespace NutriFlow.Domain.Queries.Interfaces
{
    public interface INutritionistQuery
    {
        Task<NutritionistDashboardResponse> GetDashBoardAsync(Guid nutritionistId);
        Task<NutritionistResponse?> GetByIdAsync(Guid nutritionistId);
        Task<NutritionistProfileResponse?> GetProfileAsync(Guid nutritionistId);
        Task<NutritionistRecipeResponse?> GetRecipeAsync(Guid id, Guid nutritionistId, GetRecipeRequest request);
        Task<IEnumerable<NutritionistRecipeResponse>> GetRecipesAsync(Guid nutritionistId, GetRecipeRequest request);
    }
}
