using NutriFlow.Domain.Enums;
using NutriFlow.Domain.Tables;
using NutriFlow.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace NutriFlow.Infrastructure.Tables
{
    public class Nutritionist
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public EGender Gender { get; set; }
        public Crn Crn { get; set; } = null!;
        public Email Email { get; set; } = null!;

        public ICollection<NutritionistPatient> NutritionistPatients { get; set; } = new List<NutritionistPatient>();
        public ICollection<NutritionistRecipe> NutritionistRecipes { get; set; } = new List<NutritionistRecipe>();
    }
}
