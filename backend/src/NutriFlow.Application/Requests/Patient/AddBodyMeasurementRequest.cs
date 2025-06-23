using FluentResults;
using FluentValidation;
using MediatR;
using System.Text.Json.Serialization;

namespace NutriFlow.Application.Requests.Patient
{
    public class AddBodyMeasurementRequest : IRequest<Result>
    {
        [JsonIgnore]
        public Guid PatientId { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public decimal? Neck { get; set; }
        public decimal? Chest { get; set; }
        public decimal? Waist { get; set; }
        public decimal? Abdomen { get; set; }
        public decimal? Hip { get; set; }
        public decimal? RightArm { get; set; }
        public decimal? LeftArm { get; set; }
        public decimal? RightForearm { get; set; }
        public decimal? LeftForearm { get; set; }
        public decimal? RightThigh { get; set; }
        public decimal? LeftThigh { get; set; }
        public decimal? RightCalf { get; set; }
        public decimal? LeftCalf { get; set; }
    }

    public class AddBodyMeasurementRequestValidator : AbstractValidator<AddBodyMeasurementRequest>
    {
        public AddBodyMeasurementRequestValidator()
        {
            RuleFor(x => x.Age).NotNull().WithMessage("Idade é obrigatória.");
            RuleFor(x => x.Height).NotNull().WithMessage("Altura é obrigatória.");
            RuleFor(x => x.Weight).NotNull().WithMessage("Peso é obrigatório.");
        }
    }
}
