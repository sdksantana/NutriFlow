using FluentResults;
using MediatR;
using NutriFlow.Application.Requests.Nutritionist;
using NutriFlow.Domain.Repositories;

namespace NutriFlow.Application.Handlers.Nutritionist
{
    internal class DeleteNutritionistRecipeHandler : IRequestHandler<DeleteNutritionistRecipeRequest, Result>
    {
        private readonly INutritionistRepository _nutritionistRepository;
        public DeleteNutritionistRecipeHandler(INutritionistRepository nutritionistRepository)
        {
            _nutritionistRepository = nutritionistRepository;
        }

        public async Task<Result> Handle(DeleteNutritionistRecipeRequest request, CancellationToken cancellationToken)
        {
            var recipe = await _nutritionistRepository.GetRecipeByIdAsync(request.NutritionistId, request.Id);
            if (recipe == null)
                return Result.Fail("Receita não encontrada.");

            await _nutritionistRepository.DeleteRecipeAsync(recipe);

            return Result.Ok();
        }
    }
}
