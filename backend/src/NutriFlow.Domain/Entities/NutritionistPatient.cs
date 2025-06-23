namespace NutriFlow.Domain.Entities
{
    public class NutritionistPatient
    {
        public Guid Id { get; private set; }
        public Patient Patient { get; private set; }
        public Nutritionist Nutritionist { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime StartDate { get; private set; }

        public NutritionistPatient(Nutritionist nutritionist,
                                   Patient patient)
        {
            Patient = patient;
            Nutritionist = nutritionist;
            IsActive = true;
            StartDate = DateTime.UtcNow;
        }

        public NutritionistPatient(Nutritionist nutritionist,
                                   Patient patient,
                                   DateTime? startDate = null,
                                   bool? isActive = null)
        {
            Patient = patient;
            Nutritionist = nutritionist;
        }

        public NutritionistPatient(Guid id,
                                   Nutritionist nutritionist,
                                   Patient patient,
                                   bool isActive,
                                   DateTime startDate) : this(nutritionist,
                                                              patient)
        {
            Id = id;
        }

        public NutritionistPatient(Guid id, Nutritionist nutritionist,
                               Patient patient) : this(nutritionist, patient)
        {
            Id = id;
        }

        public void Deactivate() => IsActive = false;

        public void Reactivate() => IsActive = true;
    }
}
