using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tk.Somnia.Model.Entities;

namespace Tk.Somnia.Data.Context.Journals.Configuration;

public class JournalEntryConfiguration : IEntityTypeConfiguration<JournalEntry>
{
    public void Configure(EntityTypeBuilder<JournalEntry> builder)
    {
        builder.Property(x => x.Date).IsRequired().HasColumnType("date");
        builder.HasIndex(x => x.Date).IsUnique();
        
        builder.Property(x => x.Title).HasColumnType("nvarchar(100)");
        builder.Property(x => x.Contents).HasColumnType("nvarchar(max)");
        
        builder.ToTable("JournalEntries");
    }
}