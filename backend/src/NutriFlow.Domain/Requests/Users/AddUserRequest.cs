using FluentResults;
using MediatR;
using NutriFlow.Domain.Enums;

namespace NutriFlow.Domain.Requests.Users
{
    public class AddUserRequest : IRequest<Result>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public EUserType UserType { get; set; }
        public AddNutritionistRequest? Nutritionist { get; set; }
        public AddPatientRequest? Patient { get; set; }
    }

    public class AddNutritionistRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public EGender Gender { get; set; }
        public string Crn { get; set; } = null!;
    }

    public class AddPatientRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public EGender Gender { get; set; }
    }
}
