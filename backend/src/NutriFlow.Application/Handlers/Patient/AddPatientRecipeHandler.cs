using FluentResults;
using MediatR;
using NutriFlow.Application.Requests.Patient;
using NutriFlow.Domain.Entities;
using NutriFlow.Domain.Repositories;

namespace NutriFlow.Application.Handlers.Patient
{
    public class AddPatientRecipeHandler : IRequestHandler<AddPatientRecipeRequest, Result>
    {
        private readonly IPatientRepository _patientRepository;
        public AddPatientRecipeHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public async Task<Result> Handle(AddPatientRecipeRequest request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.PatientId);

            if (patient == null)
            {
                return Result.Fail("Paciente não encontrado.");
            }

            var recipe = new PatientRecipe(
                request.Name,
                request.Description,
                request.PreparationTime,
                request.Calories,
                request.Protein,
                request.Fat,
                request.Carbs,
                request.Meal,
                patient);

            await _patientRepository.SaveRecipeAsync(recipe);

            return Result.Ok();
        }
    }
}
