using FluentResults;
using MediatR;
using System.Text.Json.Serialization;

namespace NutriFlow.Application.Requests.NutritionistPatient
{
    public class DeleteRecipeLinkRequest : IRequest<Result>
    {
        [JsonIgnore]
        public Guid NutritionistId { get; set; }
        public Guid Id { get; set; }
    }
}
