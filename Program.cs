using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using DrinksApp;

AnsiConsole.MarkupLine("[bold green]Welcome to the drinks menu![/]");

// Setup Dependency Injection (DI)
var serviceProvider = new ServiceCollection()
    .AddScoped<Menu>()         // Register Menu
    .BuildServiceProvider();

// Get Required Services from DI container
var menu = serviceProvider.GetRequiredService<Menu>();

// Run the async method of Menu to fetch and display drinks
await menu.DisplayMenuAsync();
