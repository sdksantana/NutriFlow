using FluentResults;
using MediatR;
using NutriFlow.Application.Requests.Patient;
using NutriFlow.Domain.Repositories;

namespace NutriFlow.Application.Handlers.Patient
{
    public class DeletePatientRecipeHandler : IRequestHandler<DeletePatientRecipeRequest, Result>
    {
        private readonly IPatientRepository _patientRepository;
        public DeletePatientRecipeHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<Result> Handle(DeletePatientRecipeRequest request, CancellationToken cancellationToken)
        {
            var recipe = await _patientRepository.GetRecipeByIdAsync(request.PatientId, request.Id);
            if (recipe == null)
                return Result.Fail("Receita não encontrada.");

            await _patientRepository.DeleteRecipeAsync(recipe);

            return Result.Ok();
        }
    }
}
