using FluentResults;
using MediatR;
using System.Text.Json.Serialization;

namespace NutriFlow.Application.Requests.Patient
{
    public class DeletePatientRecipeRequest : IRequest<Result>
    {
        [JsonIgnore]
        public Guid PatientId { get; set; }
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
