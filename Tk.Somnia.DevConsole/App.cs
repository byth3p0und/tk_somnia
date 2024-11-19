using Microsoft.Extensions.DependencyInjection;
using Tk.Somnia.Interface.Repositories;
using Tk.Somnia.Model.Entities;

namespace Tk.Somnia.DevConsole;

public class App
{
    private readonly IServiceProvider _serviceProvider;
    
    public App(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task InitAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IJournalRepository>();
        await repository.MigrateAsync();
    }
    
    public async Task RunAsync()
    {
        // DO YOUR THING HONEY
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IJournalRepository>();

        var journal = Journal.Create("test_journal");
        journal.SetDescription("Journal for testing.");

        var entry1 = JournalEntry.Create(new DateOnly(2024, 11, 18));
        entry1.SetTitle("Best day of my life !!");
        entry1.SetContents("I got a new job today, I'm so happy !!");
        journal.AddEntry(entry1);
        
        var entry2 = JournalEntry.Create(new DateOnly(2024, 11, 19));
        entry2.SetTitle("Worst day of my life !!");
        entry2.SetContents("I lost my job today, I'm so sad !!");
        journal.AddEntry(entry2);
        
        await repository.AddAsync(journal);
    }
}