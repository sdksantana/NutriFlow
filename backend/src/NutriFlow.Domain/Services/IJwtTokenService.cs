namespace NutriFlow.Domain.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(Guid userId, int userType, Guid? patientId, Guid? nutritionistId);
    }
}
