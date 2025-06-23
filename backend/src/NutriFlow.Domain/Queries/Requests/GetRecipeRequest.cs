using FluentResults;
using MediatR;

namespace NutriFlow.Domain.Queries.Requests
{
    public class GetRecipeRequest : IRequest<Result>
    {
        public string? Name { get; set; } = null!;
        public string? PreparationTime { get; set; } = null!;
        public string? Calories { get; set; } = null!;
        public string? Protein { get; set; } = null!;
        public string? Fat { get; set; } = null!;
        public string? Carbs { get; set; } = null!;
        public string? Meal { get; set; } = null!;
    }
}
