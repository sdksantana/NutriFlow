using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutriFlow.Domain.Tables;

namespace NutriFlow.Infrastructure.Tables.Mapping
{
    public class NutritionistPatientMapping : IEntityTypeConfiguration<NutritionistPatient>
    {
        public void Configure(EntityTypeBuilder<NutritionistPatient> builder)
        {
            builder.ToTable("NutritionistPatients");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();

            builder
              .HasOne(o => o.Nutritionist)
              .WithMany(np => np.NutritionistPatients)
              .HasForeignKey(fk => fk.NutritionistId);

            builder
                .HasOne(o => o.Patient)
                .WithMany(np => np.NutritionistPatients)
                .HasForeignKey(np => np.PatientId);
        }
    }
}
