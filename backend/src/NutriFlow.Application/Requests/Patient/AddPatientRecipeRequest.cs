using FluentResults;
using FluentValidation;
using MediatR;
using System.Text.Json.Serialization;

namespace NutriFlow.Application.Requests.Patient
{
    public class AddPatientRecipeRequest : IRequest<Result>
    {
        [JsonIgnore]
        public Guid PatientId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PreparationTime { get; set; } = null!;
        public string Calories { get; set; } = null!;
        public string Protein { get; set; } = null!;
        public string Fat { get; set; } = null!;
        public string Carbs { get; set; } = null!;
        public string Meal { get; set; } = null!;

    }

    public class AddRecipeRequestRequestValidator : AbstractValidator<AddPatientRecipeRequest>
    {
        public AddRecipeRequestRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Informe o título da receita!");
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Informe a descrição da receita!");
            RuleFor(x => x.PreparationTime).NotNull().NotEmpty().WithMessage("Informe o tempo de preparo!");
            RuleFor(x => x.Calories).NotNull().NotEmpty().WithMessage("Informe a faixa de calorias!");
            RuleFor(x => x.Protein).NotNull().NotEmpty().WithMessage("Informe a faixa de proteínas!");
            RuleFor(x => x.Fat).NotNull().NotEmpty().WithMessage("Informe a faixa de gorduras!");
            RuleFor(x => x.Carbs).NotNull().NotEmpty().WithMessage("Informe a faixa de carboidratos!");
            RuleFor(x => x.Meal).NotNull().NotEmpty().WithMessage("Informe o tipo de refeição!");
        }
    }
}
