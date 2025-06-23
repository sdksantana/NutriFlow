using FluentResults;
using MediatR;
using System.Text.Json.Serialization;

namespace NutriFlow.Application.Requests.Nutritionist
{
    public class UpdateNutritionistRecipeRequest : IRequest<Result>
    {
        [JsonIgnore]
        public Guid NutritionistId { get; set; }
        [JsonIgnore]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PreparationTime { get; set; }
        public string? Calories { get; set; }
        public string? Protein { get; set; }
        public string? Fat { get; set; }
        public string? Carbs { get; set; }
        public string? Meal { get; set; }
    }
}
