namespace NutriFlow.Domain.Queries.Responses.Patient
{
    public class RecipeFilterResponse
    {
        public string Category { get; set; } = null!;
        public List<FilterItem> Filters { get; set; } = new();
    }

    public class FilterItem
    {
        public string Value { get; set; } = null!;
        public string Label { get; set; } = null!;
    }
}
