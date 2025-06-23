using FluentResults;
using MediatR;
using NutriFlow.Application.Requests.NutritionistPatient;
using NutriFlow.Domain.Entities;
using NutriFlow.Domain.Repositories;

namespace NutriFlow.Application.Handlers.NutritionistPatient
{
    public class AddRecipeLinkHandler : IRequestHandler<AddRecipeLinkRequest, Result>
    {
        private readonly INutritionistRepository _nutritionistRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly INutritionistPatientRepository _nutritionistPatientRepository;

        public AddRecipeLinkHandler(
            INutritionistRepository nutritionistRepository,
            IPatientRepository patientRepository,
            INutritionistPatientRepository nutritionistPatientRepository)
        {
            _nutritionistRepository = nutritionistRepository;
            _patientRepository = patientRepository;
            _nutritionistPatientRepository = nutritionistPatientRepository;
        }

        public async Task<Result> Handle(AddRecipeLinkRequest request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.PatientId);
            if (patient == null)
                return Result.Fail("Paciente não encontrado.");

            var recipe = await _nutritionistRepository.GetRecipeByIdAsync(request.NutritionistId, request.NutritionistRecipeId);
            if (recipe == null)
                return Result.Fail("Receita não encontrada.");

            var recipeLink = new NutritionistPatientRecipe(recipe, patient);

            await _nutritionistPatientRepository.SaveRecipeAsync(recipeLink);

            return Result.Ok();
        }
    }
}
