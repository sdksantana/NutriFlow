namespace NutriFlow.Domain.Queries.Responses.Patient
{
    public class BodyMeasurementHistoricResponse
    {
        public string Label { get; set; } = null!;
        public decimal Initial { get; set; }
        public decimal Current { get; set; }
        public decimal Diff { get; set; }
    }
}
