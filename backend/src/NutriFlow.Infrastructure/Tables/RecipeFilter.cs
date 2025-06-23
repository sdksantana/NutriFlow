namespace NutriFlow.Infrastructure.Tables
{
    public class RecipeFilter
    {
        public Guid Id { get; set; }
        public string Category { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}
