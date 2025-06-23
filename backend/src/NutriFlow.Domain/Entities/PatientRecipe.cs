namespace NutriFlow.Domain.Entities
{

    public class PatientRecipe
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

        public Patient Patient { get; set; } = null!;

        public PatientRecipe(
            string name,
            string description,
            string preparationTime,
            string calories,
            string protein,
            string fat,
            string carbs,
            string meal,
            Patient patient)
        {
            Name = name;
            Description = description;
            PreparationTime = preparationTime;
            Calories = calories;
            Protein = protein;
            Fat = fat;
            Carbs = carbs;
            Meal = meal;
            Patient = patient;
        }

        public PatientRecipe(
            Guid id,
            string name,
            string description,
            string preparationTime,
            string calories,
            string protein,
            string fat,
            string carbs,
            string meal,
            Patient patient) : this(
                name,
                description,
                preparationTime,
                calories,
                protein,
                fat,
                carbs,
                meal,
                patient)
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
