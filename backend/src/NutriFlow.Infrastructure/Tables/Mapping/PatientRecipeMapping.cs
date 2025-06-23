using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NutriFlow.Infrastructure.Tables.Mapping
{
    public class PatientRecipeMapping : IEntityTypeConfiguration<PatientRecipe>
    {
        public void Configure(EntityTypeBuilder<PatientRecipe> builder)
        {
            builder.ToTable("PatientRecipes");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.PreparationTime).IsRequired();
            builder.Property(x => x.Calories).IsRequired();
            builder.Property(x => x.Protein).IsRequired();
            builder.Property(x => x.Fat).IsRequired();
            builder.Property(x => x.Carbs).IsRequired();
            builder.Property(x => x.Meal).IsRequired();
            builder.Property(x => x.CreationTime).IsRequired();

            builder
                .HasOne(o => o.Patient)
                .WithMany(np => np.PatientRecipes)
                .HasForeignKey(np => np.PatientId);
        }
    }
}
