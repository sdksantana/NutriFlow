using NutriFlow.Domain.Queries.Requests;
using NutriFlow.Domain.Queries.Responses;
using NutriFlow.Domain.Queries.Responses.Patient;

namespace NutriFlow.Domain.Queries.Interfaces
{
    public interface IPatientQuery
    {
        Task<PatientResponse?> GetPatientByIdAsync(Guid id);
        Task<BodyMeasurementResponse?> GetBodyMeasurementByRecentDateAsync(Guid id);
        Task<List<BodyMeasurementHistoricResponse?>> GetBodyMeasurementHistoricAsync(Guid id);
        Task<int?> GetTmbAsync(Guid id);
        Task<DashboardResponse> GetDashBoardAsync(Guid patientId);
        Task<RecipeResponse?> GetRecipeAsync(Guid id, Guid patientId, GetRecipeRequest request);
        Task<IEnumerable<RecipeResponse>> GetRecipesAsync(Guid patientId, GetRecipeRequest request);
        Task<List<BodyMeasurementEvolutionResponse>> GetBodyMeasurementEvolutionAsync(Guid patientId);
        Task<PatientResponse?> GetPatientByEmailAsync(string email);
        Task<IEnumerable<RecipeResponse?>> GetPlanRecipesAsync(Guid patientId, GetRecipeRequest request);
        Task<RecipeResponse?> GetPlanRecipesByIdAsync(Guid id, Guid patientId, GetRecipeRequest request);
    }
}
