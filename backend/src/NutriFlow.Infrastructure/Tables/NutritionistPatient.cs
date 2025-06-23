
using NutriFlow.Infrastructure.Tables;

namespace NutriFlow.Domain.Tables
{
    public class NutritionistPatient
    {
        public Guid Id { get; set; }

        public Guid NutritionistId { get; set; }
        public Nutritionist Nutritionist { get; set; } = null!;

        public Guid PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
