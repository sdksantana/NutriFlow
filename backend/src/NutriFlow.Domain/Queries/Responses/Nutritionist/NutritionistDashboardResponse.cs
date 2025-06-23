namespace NutriFlow.Domain.Queries.Responses.Nutritionist
{
    public class NutritionistDashboardResponse
    {
        public string? FirstName { get; set; } = null!;
        public string? TopUpdatedPatient { get; set; }
        public int? TotalMealsRegistered { get; set; }
        public int? TotalLinkedPatients { get; set; }
        public string? PatientEmail { get; set; }
        public string? PatientGender { get; set; }
        public string? PatientLinkStartDate { get; set; }
    }
}
