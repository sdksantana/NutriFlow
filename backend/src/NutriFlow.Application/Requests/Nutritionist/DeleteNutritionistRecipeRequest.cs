using FluentResults;
using MediatR;
using System.Text.Json.Serialization;

namespace NutriFlow.Application.Requests.Nutritionist
{
    public class DeleteNutritionistRecipeRequest : IRequest<Result>
    {
        [JsonIgnore]
        public Guid NutritionistId { get; set; }
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
