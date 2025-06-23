using NutriFlow.Application.Requests.Patient;
using NutriFlow.Domain.Entities;
using NutriFlow.Domain.Repositories;
using NutriFlow.Domain.Services;

namespace NutriFlow.Infrastructure.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public async Task<BodyMeasurement> MergeBodyMeasurementAsync(UpdateBodyMeasurementRequest request, Patient patient)
        {
            var last = await _patientRepository.GetBodyMeasurementByRecentDateAsync(patient.Id);

            var merged = new BodyMeasurement(
                age: request.Age ?? last?.Age ?? 0,
                height: request.Height ?? last?.Height ?? 0,
                weight: request.Weight ?? last?.Weight ?? 0,
                neck: request.Neck ?? last?.Neck,
                chest: request.Chest ?? last?.Chest,
                waist: request.Waist ?? last?.Waist,
                abdomen: request.Abdomen ?? last?.Abdomen,
                hip: request.Hip ?? last?.Hip,
                rightArm: request.RightArm ?? last?.RightArm,
                leftArm: request.LeftArm ?? last?.LeftArm,
                rightForearm: request.RightForearm ?? last?.RightForearm,
                leftForearm: request.LeftForearm ?? last?.LeftForearm,
                rightThigh: request.RightThigh ?? last?.RightThigh,
                leftThigh: request.LeftThigh ?? last?.LeftThigh,
                rightCalf: request.RightCalf ?? last?.RightCalf,
                leftCalf: request.LeftCalf ?? last?.LeftCalf,
                patient: patient
            );

            return merged;
        }
    }
}
