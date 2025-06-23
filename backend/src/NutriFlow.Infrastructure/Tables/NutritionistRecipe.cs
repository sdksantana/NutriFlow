namespace NutriFlow.Infrastructure.Tables
{
    public class NutritionistRecipe
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
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;

        public Guid NutritionistId { get; set; }
        public Nutritionist Nutritionist { get; set; } = null!;

        public ICollection<NutritionistPatientRecipe> NutritionistPatientRecipes { get; set; } = new List<NutritionistPatientRecipe>();
    }
}
