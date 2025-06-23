namespace NutriFlow.Domain.Queries.Responses.Patient
{
    public class DashboardResponse
    {
        public string? FirstName { get; set; } = null!;
        public decimal LastWeightRegistered { get; set; }
        public int? TotalMealsRegistered { get; set; }
        //public int? MealsRegisteredToday { get; set; }
        public string? Nutritionist { get; set; }
        public string? NutritionistEmail { get; set; }
        public string? NutritionistCrn { get; set; }
        public string? NutritionistLinkStartDate { get; set; }
    }
}
