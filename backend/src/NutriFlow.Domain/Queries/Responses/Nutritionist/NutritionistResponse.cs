using NutriFlow.Domain.Enums;

namespace NutriFlow.Domain.Queries.Responses.Nutritionist
{
    public class NutritionistResponse
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public EGender Gender { get; set; }
        public string Crn { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime StartDate { get; set; }
    }
}
