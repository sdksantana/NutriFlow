namespace NutriFlow.Infrastructure.Tables
{
    public class BodyMeasurement
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }

        public int Age { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public decimal? Neck { get; set; }
        public decimal? Chest { get; set; }
        public decimal? Waist { get; set; }
        public decimal? Abdomen { get; set; }
        public decimal? Hip { get; set; }
        public decimal? RightArm { get; set; }
        public decimal? LeftArm { get; set; }
        public decimal? RightForearm { get; set; }
        public decimal? LeftForearm { get; set; }
        public decimal? RightThigh { get; set; }
        public decimal? LeftThigh { get; set; }
        public decimal? RightCalf { get; set; }
        public decimal? LeftCalf { get; set; }
        public Patient Patient { get; set; } = null!;

        public DateTime CreationTime { get; set; }
        public string? CreatorUserName { get; set; }
    }
}
