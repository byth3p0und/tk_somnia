using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Tk.Somnia.Interface.Interceptors;
using Tk.Somnia.Interface.Providers;
using Tk.Somnia.Model.Base;

namespace Tk.Somnia.Data.Context.Journals.Interceptors;

public class PersistenceInterceptor : SaveChangesInterceptor, IPersistenceInterceptor
{
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public PersistenceInterceptor(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }
    
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            _updateEntities(eventData.Context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void _updateEntities(DbContext context)
    {
        var changes = context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged);
        DateTimeOffset now = _dateTimeProvider.Now;
        
        foreach (var entry in changes)
        {
            if (entry.Entity is not Entity)
                continue;

            switch (entry.State)
            {
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Property("DeletedOn").CurrentValue = now;
                    break;
                case EntityState.Modified:
                    var metadataProperties = entry.Metadata.GetProperties().Where(p => !p.IsNullable && p.IsIndex() && p.IsForeignKey());
                    
                    foreach (var metadataProperty in metadataProperties)
                    {
                        if (Nullable.GetUnderlyingType(metadataProperty.ClrType) == null || !metadataProperty.ClrType.IsValueType)
                            continue;

                        var oldValue = entry.Property(metadataProperty.Name).OriginalValue;
                        var newValue = entry.Property(metadataProperty.Name).CurrentValue;
                        var check = entry.Property(metadataProperty.Name).CurrentValue is null;

                        if (oldValue == null || oldValue.Equals(newValue) || !check)
                            continue;

                        entry.Property(metadataProperty.Name).CurrentValue = oldValue;
                        entry.Property("DeletedOn").CurrentValue = now;
                    }

                    entry.Property("ModifiedOn").CurrentValue = now;
                    break;
                case EntityState.Added:
                    entry.Property("CreatedOn").CurrentValue = now;
                    entry.Property("ModifiedOn").CurrentValue = now;
                    break;
                case EntityState.Detached:
                case EntityState.Unchanged:
                default:
                    break;
            }
        }
    }
}