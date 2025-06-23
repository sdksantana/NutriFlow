using FluentResults;
using MediatR;
using NutriFlow.Application.Requests.Patient;
using NutriFlow.Domain.Repositories;
using NutriFlow.Domain.Services;

namespace NutriFlow.Application.Handlers.Patient
{
    public class UpdateBodyMeasurementHandler : IRequestHandler<UpdateBodyMeasurementRequest, Result>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientService _patientService;

        public UpdateBodyMeasurementHandler(IPatientRepository patientRepository, IPatientService patientService)
        {
            _patientRepository = patientRepository;
            _patientService = patientService;
        }

        public async Task<Result> Handle(UpdateBodyMeasurementRequest request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.PatientId);
            if (patient == null)
                return Result.Fail("Paciente não encontrado.");

            var merged = await _patientService.MergeBodyMeasurementAsync(request, patient);
            merged.SetPatient(patient);

            var lastMeasurement = await _patientRepository.GetBodyMeasurementByRecentDateAsync(request.PatientId);

            if (lastMeasurement != null && merged.Equals(lastMeasurement))
            {
                await _patientRepository.SaveBodyMeasurementAsync(lastMeasurement);
                return Result.Ok();
            }

            await _patientRepository.SaveBodyMeasurementAsync(merged);

            return Result.Ok();
        }
    }
}
