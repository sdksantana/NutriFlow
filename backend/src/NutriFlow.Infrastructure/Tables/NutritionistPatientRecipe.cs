namespace NutriFlow.Infrastructure.Tables
{
    public class NutritionistPatientRecipe
    {
        public Guid Id { get; set; }

        public Guid NutritionistRecipeId { get; set; }
        public NutritionistRecipe NutritionistRecipe { get; set; } = null!;

        public Guid PatientId { get; set; }
        public Patient Patient { get; set; } = null!;
    }
}
