using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tk.Somnia.Model.Base;

namespace Tk.Somnia.Data.Context.DefaultConfiguration;

public class EntityConfiguration : IEntityTypeConfiguration<Entity>
{
    public void Configure(EntityTypeBuilder<Entity> builder)
    {
        builder.Property<Guid>("Id").HasColumnType("uniqueidentifier").ValueGeneratedOnAdd();
        builder.HasKey("Id");
        
        builder.Property<DateTimeOffset>("CreatedOn").IsRequired().HasColumnType("datetimeoffset");
        builder.Property<DateTimeOffset>("ModifiedOn").IsRequired().HasColumnType("datetimeoffset");

        builder.Property<DateTimeOffset?>("DeletedOn").HasColumnType("datetimeoffset");
        
        builder.HasQueryFilter(x => EF.Property<DateTimeOffset?>(x, "DeletedOn") == null);

        builder.ToTable("Entities");
    }
}