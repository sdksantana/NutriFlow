namespace NutriFlow.Domain.Queries.Responses.NutritionistPatient
{
    public class NutritionistRecipeResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PreparationTime { get; set; } = null!;
        public string Calories { get; set; } = null!;
        public string Protein { get; set; } = null!;
        public string Fat { get; set; } = null!;
        public string Carbs { get; set; } = null!;
        public string Meal { get; set; } = null!;
        public IEnumerable<RecipeLinkItem> Links { get; set; } = null!;
    }

    public class RecipeLinkItem
    {
        public Guid PatientId { get; set; }
        public string PatientName { get; set; } = null!;
        public Guid RecipeLinkId { get; set; }
    }

}
