using FluentResults;
using MediatR;
using NutriFlow.Domain.Requests.Users;
using NutriFlow.Domain.Services;

namespace NutriFlow.Application.Handlers.Users
{
    public class AddUserHandler : IRequestHandler<AddUserRequest, Result>
    {
        private readonly IUserService _userService;

        public AddUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            return await _userService.AddUserAsync(request);
        }
    }
}
