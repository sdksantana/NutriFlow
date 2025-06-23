using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NutriFlow.Domain.Tables;
using NutriFlow.Infrastructure.Tables;

namespace NutriFlow.Infrastructure
{
    public class NutriFlowDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public NutriFlowDbContext(DbContextOptions<NutriFlowDbContext> options)
            : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Nutritionist> Nutritionists { get; set; }
        public DbSet<BodyMeasurement> BodyMeasurements { get; set; }
        public DbSet<NutritionistPatient> NutritionistsPatients { get; set; }
        public DbSet<PatientRecipe> PatientRecipes { get; set; }
        public DbSet<RecipeFilter> ReciperFilters { get; set; }
        public DbSet<NutritionistRecipe> NutritionistRecipes { get; set; }
        public DbSet<NutritionistPatientRecipe> NutritionistPatientRecipes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(NutriFlowDbContext).Assembly);
        }
    }
}
