using NutriFlow.Domain.Enums;
using NutriFlow.Domain.Tables;
using NutriFlow.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace NutriFlow.Infrastructure.Tables
{
    public class Patient
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public EGender Gender { get; set; }
        public bool IsMeasurementComplete { get; set; }
        public Email Email { get; set; } = null!;

        public ICollection<NutritionistPatient> NutritionistPatients { get; set; } = new List<NutritionistPatient>();
        public ICollection<BodyMeasurement> BodyMeasurements { get; set; } = new List<BodyMeasurement>();
        public ICollection<PatientRecipe> PatientRecipes { get; set; } = new List<PatientRecipe>();
        public ICollection<NutritionistPatientRecipe> NutritionistPatientRecipes { get; set; } = new List<NutritionistPatientRecipe>();
    }
}
