using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NutriFlow.Infrastructure.Tables.Mapping
{
    public class BodyMeasurementMapping : IEntityTypeConfiguration<BodyMeasurement>
    {
        public void Configure(EntityTypeBuilder<BodyMeasurement> builder)
        {
            builder.ToTable("BodyMeasurements");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Age).IsRequired();
            builder.Property(p => p.Height).IsRequired();
            builder.Property(p => p.Weight).IsRequired();
            builder.Property(p => p.Neck).IsRequired(false);
            builder.Property(p => p.Chest).IsRequired(false);
            builder.Property(p => p.Waist).IsRequired(false);
            builder.Property(p => p.Abdomen).IsRequired(false);
            builder.Property(p => p.Hip).IsRequired(false);
            builder.Property(p => p.RightArm).IsRequired(false);
            builder.Property(p => p.LeftArm).IsRequired(false);
            builder.Property(p => p.RightForearm).IsRequired(false);
            builder.Property(p => p.LeftForearm).IsRequired(false);
            builder.Property(p => p.RightThigh).IsRequired(false);
            builder.Property(p => p.LeftThigh).IsRequired(false);
            builder.Property(p => p.RightCalf).IsRequired(false);
            builder.Property(p => p.LeftCalf).IsRequired(false);

            builder.HasOne(p => p.Patient)
              .WithMany(p => p.BodyMeasurements)
              .HasForeignKey(p => p.PatientId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
