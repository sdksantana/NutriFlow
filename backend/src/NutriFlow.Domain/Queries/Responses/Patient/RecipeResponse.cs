namespace NutriFlow.Domain.Queries.Responses
{
    public class RecipeResponse
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
    }
}
