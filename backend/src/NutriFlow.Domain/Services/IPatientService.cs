using NutriFlow.Application.Requests.Patient;
using NutriFlow.Domain.Entities;

namespace NutriFlow.Domain.Services
{
    public interface IPatientService
    {
        Task<BodyMeasurement> MergeBodyMeasurementAsync(UpdateBodyMeasurementRequest request, Patient patient);
    }
}
