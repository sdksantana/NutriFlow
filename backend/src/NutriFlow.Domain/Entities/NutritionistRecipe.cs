namespace NutriFlow.Domain.Entities
{
    public class NutritionistRecipe
    {
        public Guid Id { get; }
        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public string PreparationTime { get; private set; } = null!;
        public string Calories { get; private set; } = null!;
        public string Protein { get; private set; } = null!;
        public string Fat { get; private set; } = null!;
        public string Carbs { get; private set; } = null!;
        public string Meal { get; private set; } = null!;

        public Nutritionist Nutritionist { get; set; } = null!;

        public NutritionistRecipe(
            string name,
            string description,
            string preparationTime,
            string calories,
            string protein,
            string fat,
            string carbs,
            string meal,
            Nutritionist nutritionist)
        {
            Name = name;
            Description = description;
            PreparationTime = preparationTime;
            Calories = calories;
            Protein = protein;
            Fat = fat;
            Carbs = carbs;
            Meal = meal;
            Nutritionist = nutritionist;
        }

        public NutritionistRecipe(
            Guid id,
            string name,
            string description,
            string preparationTime,
            string calories,
            string protein,
            string fat,
            string carbs,
            string meal,
            Nutritionist nutritionist) : this(
                name,
                description,
                preparationTime,
                calories,
                protein,
                fat,
                carbs,
                meal,
                nutritionist)
        {
            Id = id;
        }

        public void Update(
            string? name,
            string? description,
            string? preparationTime,
            string? calories,
            string? protein,
            string? fat,
            string? carbs,
            string? meal)
        {
            if (!string.IsNullOrWhiteSpace(name)) Name = name;
            if (!string.IsNullOrWhiteSpace(description)) Description = description;
            if (!string.IsNullOrWhiteSpace(preparationTime)) PreparationTime = preparationTime;
            if (!string.IsNullOrWhiteSpace(calories)) Calories = calories;
            if (!string.IsNullOrWhiteSpace(protein)) Protein = protein;
            if (!string.IsNullOrWhiteSpace(fat)) Fat = fat;
            if (!string.IsNullOrWhiteSpace(carbs)) Carbs = carbs;
            if (!string.IsNullOrWhiteSpace(meal)) Meal = meal;
        }
    }
}
