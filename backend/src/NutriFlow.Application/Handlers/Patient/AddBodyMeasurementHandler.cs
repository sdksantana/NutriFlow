using FluentResults;
using MediatR;
using NutriFlow.Application.Requests.Patient;
using NutriFlow.Domain.Entities;
using NutriFlow.Domain.Repositories;

namespace NutriFlow.Application.Handlers.Patient
{
    public class AddBodyMeasurementHandler : IRequestHandler<AddBodyMeasurementRequest, Result>
    {
        private readonly IPatientRepository _patientRepository;
        public AddBodyMeasurementHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public async Task<Result> Handle(AddBodyMeasurementRequest request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.PatientId);

            if (patient == null)
            {
                return Result.Fail("Paciente não encontrado.");
            }

            var bodyMeasurement = new BodyMeasurement(request.Age,
                                                      request.Height,
                                                      request.Weight,
                                                      request.Neck,
                                                      request.Chest,
                                                      request.Waist,
                                                      request.Abdomen,
                                                      request.Hip,
                                                      request.RightArm,
                                                      request.LeftArm,
                                                      request.RightForearm,
                                                      request.LeftForearm,
                                                      request.RightThigh,
                                                      request.LeftThigh,
                                                      request.RightCalf,
                                                      request.LeftCalf,
                                                      patient!);

            await _patientRepository.SaveBodyMeasurementAsync(bodyMeasurement);

            patient.MarkMeasurementAsComplete();
            await _patientRepository.SaveAsync(patient);

            return Result.Ok();
        }
    }
}
