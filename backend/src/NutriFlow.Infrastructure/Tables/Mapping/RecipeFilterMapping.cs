using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NutriFlow.Infrastructure.Tables.Mapping
{
    public class RecipeFilterMapping : IEntityTypeConfiguration<RecipeFilter>
    {
        public void Configure(EntityTypeBuilder<RecipeFilter> builder)
        {
            builder.ToTable("RecipeFilters");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd();

            builder.Property(f => f.Category).IsRequired();
            builder.Property(f => f.Value).IsRequired();
        }
    }
}
