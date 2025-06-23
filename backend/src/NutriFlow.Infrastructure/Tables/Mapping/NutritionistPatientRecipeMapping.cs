using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NutriFlow.Infrastructure.Tables.Mapping
{
    public class NutritionistPatientRecipeMapping : IEntityTypeConfiguration<NutritionistPatientRecipe>
    {
        public void Configure(EntityTypeBuilder<NutritionistPatientRecipe> builder)
        {
            builder.ToTable("NutritionistPatientRecipes");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder
              .HasOne(o => o.NutritionistRecipe)
              .WithMany(np => np.NutritionistPatientRecipes)
              .HasForeignKey(fk => fk.NutritionistRecipeId);

            builder
                .HasOne(o => o.Patient)
                .WithMany(np => np.NutritionistPatientRecipes)
                .HasForeignKey(np => np.PatientId);
        }
    }
}
