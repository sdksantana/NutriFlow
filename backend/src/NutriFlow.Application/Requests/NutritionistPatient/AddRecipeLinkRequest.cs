using FluentResults;
using MediatR;
using System.Text.Json.Serialization;

namespace NutriFlow.Application.Requests.NutritionistPatient
{
    public class AddRecipeLinkRequest : IRequest<Result>
    {
        public Guid NutritionistRecipeId { get; set; }
        public Guid PatientId { get; set; }
        [JsonIgnore]
        public Guid NutritionistId { get; set; }
    }
}
