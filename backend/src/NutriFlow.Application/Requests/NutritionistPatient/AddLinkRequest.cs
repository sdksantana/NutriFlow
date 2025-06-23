using FluentResults;
using FluentValidation;
using MediatR;
using NutriFlow.Domain.Enums;
using System.Text.Json.Serialization;

namespace NutriFlow.Domain.Requests.NutritionistPatient
{
    public class AddLinkRequest : IRequest<Result>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public string Email { get; set; } = null!;
        public EUserType UserType { get; set; }
        public bool isActive { get; set; }
    }

    public class AddLinkRequestValidator : AbstractValidator<AddLinkRequest>
    {
        public AddLinkRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O e-mail do paciente é obrigatório.");
        }
    }
}
