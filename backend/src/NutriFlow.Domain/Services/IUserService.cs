using FluentResults;
using NutriFlow.Domain.Requests.Users;

namespace NutriFlow.Domain.Services
{
    public interface IUserService
    {
        Task<Result> AddUserAsync(AddUserRequest request);

        Task<Result<string>> LoginAsync(string email, string password);
    }
}
