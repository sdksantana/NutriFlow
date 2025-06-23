
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutriFlow.Domain.ValueObjects;
using NutriFlow.Infrastructure.Tables;

public class NutritionistMapping : IEntityTypeConfiguration<Nutritionist>
{
    public void Configure(EntityTypeBuilder<Nutritionist> builder)
    {
        builder.ToTable("Nutritionists");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(p => p.FirstName).IsRequired().HasMaxLength(110);
        builder.Property(p => p.LastName).IsRequired().HasMaxLength(110);
        builder.Property(p => p.Gender).IsRequired();

        builder.Property(p => p.Crn)
             .HasConversion(vo => vo.Value, v => new Crn(v))
             .HasColumnName("Crn")
             .IsRequired()
             .HasMaxLength(Crn.RequiredLength);

        builder.Property(p => p.Email)
            .HasConversion(vo => vo.Adress, v => new Email(v))
            .HasColumnName("Email")
            .IsRequired()
            .HasMaxLength(Email.MaxLenghtAdress);
    }
}