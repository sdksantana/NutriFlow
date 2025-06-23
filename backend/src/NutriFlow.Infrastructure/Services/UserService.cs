using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using NutriFlow.Domain.Enums;
using NutriFlow.Domain.Repositories;
using NutriFlow.Domain.Requests.Users;
using NutriFlow.Domain.Services;
using NutriFlow.Domain.ValueObjects;

namespace NutriFlow.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly INutritionistRepository _nutritionistRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IConfiguration _configuration;
        private readonly IJwtTokenService _jwtTokenService;

        public UserService(
            UserManager<User> userManager,
            INutritionistRepository nutritionistRepository,
            IPatientRepository patientRepository,
            IConfiguration configuration,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _nutritionistRepository = nutritionistRepository;
            _patientRepository = patientRepository;
            _configuration = configuration;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<Result> AddUserAsync(AddUserRequest request)
        {
            bool hasMeasurement = false;
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return Result.Fail("E-mail já cadastrado.");

            var userType = request.UserType;

            if (request.Patient == null && request.Nutritionist == null)
                return Result.Fail("É necessário preencher os dados de cadastro.");

            var user = new User
            {
                Email = request.Email,
                UserName = request.Email,
                UserType = userType
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
                return Result.Fail(string.Join("; ", createResult.Errors.Select(e => e.Description)));

            if (userType == EUserType.PATIENT && request.Patient != null)
            {
                var patient = new Domain.Entities.Patient(
                    id: user.Id,
                    firstName: request.Patient.FirstName,
                    lastName: request.Patient.LastName,
                    gender: request.Patient.Gender,
                    email: new Email(request.Email),
                    isMeasurementComplete: hasMeasurement
                );

                await _patientRepository.SaveAsync(patient);
                user.PatientId = user.Id;
                await _userManager.UpdateAsync(user);
            }

            if (userType == EUserType.NUTRITIONIST && request.Nutritionist != null)
            {
                var nutritionist = new Domain.Entities.Nutritionist(
                    id: user.Id,
                    firstName: request.Nutritionist.FirstName,
                    lastName: request.Nutritionist.LastName,
                    gender: request.Nutritionist.Gender,
                    crn: new Crn(request.Nutritionist.Crn),
                    email: new Email(request.Email)
                );

                await _nutritionistRepository.SaveAsync(nutritionist);
                user.NutritionistId = user.Id;
                await _userManager.UpdateAsync(user);
            }

            return Result.Ok();
        }

        public async Task<Result<string>> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Result.Fail("Usuário ou senha inválidos.");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!isPasswordValid)
                return Result.Fail("Usuário ou senha inválidos.");

            var token = _jwtTokenService.GenerateToken(
                user.Id,
                (int)user.UserType,
                user.PatientId,
                user.NutritionistId
            );

            return Result.Ok(token);
        }
    }
}
