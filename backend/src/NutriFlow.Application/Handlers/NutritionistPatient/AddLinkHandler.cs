using FluentResults;
using MediatR;
using NutriFlow.Domain.Enums;
using NutriFlow.Domain.Repositories;
using NutriFlow.Domain.Requests.NutritionistPatient;
using NutriFlow.Domain.ValueObjects;

namespace NutriFlow.Application.Handlers.NutritionistPatient
{
    public class AddLinkHandler : IRequestHandler<AddLinkRequest, Result>
    {
        private readonly INutritionistRepository _nutritionistRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly INutritionistPatientRepository _nutritionistPatientRepository;

        public AddLinkHandler(
            INutritionistRepository nutritionistRepository,
            IPatientRepository patientRepository,
            INutritionistPatientRepository nutritionistPatientRepository)
        {
            _nutritionistRepository = nutritionistRepository;
            _patientRepository = patientRepository;
            _nutritionistPatientRepository = nutritionistPatientRepository;
        }

        public async Task<Result> Handle(AddLinkRequest request, CancellationToken cancellationToken)
        {
            var email = new Email(request.Email);

            if (request.UserType == EUserType.NUTRITIONIST)
            {
                var nutritionist = await _nutritionistRepository.GetByIdAsync(request.UserId);
                if (nutritionist == null)
                    return Result.Fail("Nutricionista não encontrado.");

                var patient = await _patientRepository.GetByEmailAsync(email);
                if (patient == null)
                    return Result.Fail("E-mail não localizado no sistema.");

                var hasMeasurementComplete = await _patientRepository.HasMeasurementsAsync(patient.Id);
                if (!hasMeasurementComplete)
                    return Result.Fail("O paciente ainda não concluiu o cadastro das medidas corporais. Aguarde a conclusão do mesmo e tente novamente.");

                var existingLink = await _nutritionistPatientRepository.GetByPairAsync(nutritionist.Id, patient.Email);

                if (existingLink != null)
                {
                    if (request.isActive)
                        existingLink.Reactivate();
                    else
                        existingLink.Deactivate();

                    await _nutritionistPatientRepository.SaveAsync(existingLink);
                }
                else if (request.isActive)
                {
                    var link = new Domain.Entities.NutritionistPatient(nutritionist, patient);
                    await _nutritionistPatientRepository.SaveAsync(link);
                }

                return Result.Ok();
            }
            else
            {
                var patient = await _patientRepository.GetByIdAsync(request.UserId);
                if (patient == null)
                    return Result.Fail("Paciente não encontrado.");

                var nutritionist = await _nutritionistRepository.GetByEmailAsync(email);
                if (nutritionist == null)
                    return Result.Fail("E-mail não localizado no sistema.");

                var existingLink = await _nutritionistPatientRepository.GetByPairAsync(patient.Id, nutritionist.Email);

                if (existingLink?.IsActive == true)
                {
                    existingLink.Deactivate();
                    await _nutritionistPatientRepository.SaveAsync(existingLink);
                }

                return Result.Ok();
            }
        }
    }
}
