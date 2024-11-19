// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tk.Somnia.Application.Extensions;
using Tk.Somnia.DevConsole;

// Setup configuration
var configManager = new ConfigurationManager();

Console.WriteLine(configManager["Environment"]);

configManager.AddEnvironmentVariables();
configManager.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Setup services
var serviceProvider = new ServiceCollection()
    .AddApplication(configManager)
    .BuildServiceProvider();

// Run the app
var app = new App(serviceProvider);
await app.InitAsync();
await app.RunAsync();