namespace NutriFlow.Domain.Entities
{
    public class NutritionistPatientRecipe
    {
        public Guid Id { get; private set; }
        public Patient Patient { get; private set; }
        public NutritionistRecipe NutritionistRecipe { get; private set; }

        public NutritionistPatientRecipe(NutritionistRecipe nutritionistRecipe, Patient patient)
        {
            Patient = patient;
            NutritionistRecipe = nutritionistRecipe;
        }

        public NutritionistPatientRecipe(Guid id, NutritionistRecipe nutritionistRecipe, Patient patient)
            : this(nutritionistRecipe, patient)
        {
            Id = id;
        }
    }
}