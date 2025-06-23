using FluentResults;
using MediatR;
using NutriFlow.Application.Requests.NutritionistPatient;
using NutriFlow.Domain.Repositories;

namespace NutriFlow.Application.Handlers.NutritionistPatient
{
    public class DeleteRecipeLinkHandler : IRequestHandler<DeleteRecipeLinkRequest, Result>
    {
        private readonly INutritionistPatientRepository _nutritionistPatientRepository;

        public DeleteRecipeLinkHandler(
            INutritionistPatientRepository nutritionistPatientRepository)
        {
            _nutritionistPatientRepository = nutritionistPatientRepository;
        }

        public async Task<Result> Handle(DeleteRecipeLinkRequest request, CancellationToken cancellationToken)
        {
            var link = await _nutritionistPatientRepository.GetLinkByIdAsync(request.Id);
            if (link == null)
                return Result.Fail("Vínculo não encontrado.");

            await _nutritionistPatientRepository.DeleteRecipeAsync(link);

            return Result.Ok();
        }
    }
}

