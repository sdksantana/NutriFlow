using FluentResults;
using MediatR;
using NutriFlow.Application.Requests.Patient;
using NutriFlow.Domain.Repositories;

namespace NutriFlow.Application.Handlers.Patient
{
    public class UpdatePatientRecipeHandler : IRequestHandler<UpdatePatientRecipeRequest, Result>
    {
        private readonly IPatientRepository _patientRepository;
        public UpdatePatientRecipeHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public async Task<Result> Handle(UpdatePatientRecipeRequest request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.PatientId);
            if (patient == null)
                return Result.Fail("Paciente não encontrado.");

            var recipe = await _patientRepository.GetRecipeByIdAsync(request.PatientId, request.Id);
            if (recipe == null)
                return Result.Fail("Receita não encontrada.");

            recipe.Update(
                request.Name,
                request.Description,
                request.PreparationTime,
                request.Calories,
                request.Protein,
                request.Fat,
                request.Carbs,
                request.Meal);

            await _patientRepository.SaveRecipeAsync(recipe);

            return Result.Ok();
        }
    }
}
