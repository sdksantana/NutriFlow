using NutriFlow.Domain.Queries.Responses.Nutritionist;
using NutriFlow.Domain.Queries.Responses.Patient;

namespace NutriFlow.Domain.Queries.Interfaces
{
    public interface INutritionistPatientQuery
    {
        Task<NutritionistResponse?> GetNutritionistByIdAsync(Guid patientId);
        Task<List<RecipeFilterResponse>> GetRecipeFiltersAsync();
    }
}
