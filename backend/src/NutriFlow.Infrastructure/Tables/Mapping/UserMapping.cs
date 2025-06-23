using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserTableConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(u => u.Patient)
            .WithOne()
            .HasForeignKey<User>(u => u.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u => u.Nutritionist)
            .WithOne()
            .HasForeignKey<User>(u => u.NutritionistId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
