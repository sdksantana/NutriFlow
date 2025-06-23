using FluentResults;
using MediatR;
using NutriFlow.Application.Requests.Users;
using NutriFlow.Domain.Services;

public class LoginUserHandler : IRequestHandler<LoginUserRequest, Result<string>>
{
    private readonly IUserService _userService;

    public LoginUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result<string>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        return await _userService.LoginAsync(request.Email, request.Password);
    }
}
