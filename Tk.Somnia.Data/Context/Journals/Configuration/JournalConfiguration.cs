using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tk.Somnia.Model.Entities;

namespace Tk.Somnia.Data.Context.Journals.Configuration;

public class JournalConfiguration : IEntityTypeConfiguration<Journal>
{
    public void Configure(EntityTypeBuilder<Journal> builder)
    {
        builder.Property(x => x.Name).HasColumnType("nvarchar(100)");
        builder.HasIndex(x => x.Name).IsUnique();
        
        builder.Property(x => x.Description).HasColumnType("nvarchar(max)");

        builder.HasMany(x => x.Entries).WithOne(x => x.Parent);
        
        builder.ToTable("Journals");
    }
}