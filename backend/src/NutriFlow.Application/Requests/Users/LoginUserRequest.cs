using FluentResults;
using MediatR;

namespace NutriFlow.Application.Requests.Users
{
    public class LoginUserRequest : IRequest<Result<string>>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
