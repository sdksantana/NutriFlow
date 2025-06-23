using FluentResults;
using MediatR;
using NutriFlow.Application.Requests.Nutritionist;
using NutriFlow.Domain.Entities;
using NutriFlow.Domain.Repositories;

namespace NutriFlow.Application.Handlers.Nutritionist
{
    internal class AddNutritionistRecipeHandler : IRequestHandler<AddNutritionistRecipeRequest, Result>
    {
        private readonly INutritionistRepository _nutritionistRepository;
        public AddNutritionistRecipeHandler(INutritionistRepository nutritionistRepository)
        {
            _nutritionistRepository = nutritionistRepository;
        }
        public async Task<Result> Handle(AddNutritionistRecipeRequest request, CancellationToken cancellationToken)
        {
            var nutritionist = await _nutritionistRepository.GetByIdAsync(request.NutritionistId);

            if (nutritionist == null)
            {
                return Result.Fail("Nutricionista não encontrado.");
            }

            var recipe = new NutritionistRecipe(
                request.Name,
                request.Description,
                request.PreparationTime,
                request.Calories,
                request.Protein,
                request.Fat,
                request.Carbs,
                request.Meal,
                nutritionist);

            await _nutritionistRepository.SaveRecipeAsync(recipe);

            return Result.Ok();
        }
    }
}
