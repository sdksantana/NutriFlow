using Microsoft.AspNetCore.Identity;
using NutriFlow.Domain.Enums;
using NutriFlow.Infrastructure.Tables;

public class User : IdentityUser<Guid>
{
    public EUserType UserType { get; set; }

    public Guid? PatientId { get; set; }
    public Guid? NutritionistId { get; set; }

    public Patient? Patient { get; set; }
    public Nutritionist? Nutritionist { get; set; }
}
