using NutriFlow.Domain.Entities;
using NutriFlow.Domain.ValueObjects;

namespace NutriFlow.Domain.Repositories
{
    public interface IPatientRepository
    {
        Task<Guid> SaveAsync(Patient patient);
        Task<Patient?> GetByIdAsync(Guid id);
        Task<Patient?> GetByEmailAsync(Email email);
        Task<Guid> SaveBodyMeasurementAsync(BodyMeasurement bodyMeasurement);
        Task<BodyMeasurement?> GetBodyMeasurementByRecentDateAsync(Guid patientId);
        Task<bool> HasMeasurementsAsync(Guid patientId);
        Task<Guid> SaveRecipeAsync(PatientRecipe patientRecipe);
        Task<PatientRecipe?> GetRecipeByIdAsync(Guid patientId, Guid id);
        Task DeleteRecipeAsync(PatientRecipe patientRecipe);
    }
}
