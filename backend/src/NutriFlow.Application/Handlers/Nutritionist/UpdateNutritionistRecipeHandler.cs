using FluentResults;
using MediatR;
using NutriFlow.Application.Requests.Nutritionist;
using NutriFlow.Domain.Repositories;

namespace NutriFlow.Application.Handlers.Nutritionist
{
    public class UpdateNutritionistRecipeHandler : IRequestHandler<UpdateNutritionistRecipeRequest, Result>
    {
        private readonly INutritionistRepository _nutritionistRepository;
        public UpdateNutritionistRecipeHandler(INutritionistRepository nutritionistRepository)
        {
            _nutritionistRepository = nutritionistRepository;
        }

        public async Task<Result> Handle(UpdateNutritionistRecipeRequest request, CancellationToken cancellationToken)
        {
            var nutritionist = await _nutritionistRepository.GetByIdAsync(request.NutritionistId);
            if (nutritionist == null)
                return Result.Fail("Nutricionista não encontrado.");

            var recipe = await _nutritionistRepository.GetRecipeByIdAsync(request.NutritionistId, request.Id);
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

            await _nutritionistRepository.SaveRecipeAsync(recipe);

            return Result.Ok();
        }
    }
}
