namespace NutriFlow.Domain.Queries.Responses.Nutritionist
{
    public class NutritionistProfileResponse
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Crn { get; set; } = null!;
        public IEnumerable<LinkedPatientResponse> LinkedPatients { get; set; } = null!;
    }

    public class LinkedPatientResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Weight { get; set; }
        public int Height { get; set; }
        public string StartDate { get; set; } = null!;
    }
}
