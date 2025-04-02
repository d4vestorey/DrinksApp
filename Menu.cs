using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrinksApp
{
    public class Menu
    {
        private readonly ConnectionService _connectionService;

        // Constructor injection (through DI)
        public Menu(ConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        // Async method to display the menu
        public async Task DisplayMenuAsync()
        {
            List<string> categories = await ConnectionService.FetchDrinkCategoriesAsync();

            if (categories == null || categories.Count == 0)
            {
                Console.WriteLine("No drink categories available at the moment.");
                return;
            }

            var selection = AnsiConsole.Prompt(
                      new SelectionPrompt<string>()
                      .Title("Please select a drink category:")
                      .AddChoices(categories)
                      );

            switch (selection)
            {
                case "Ordinary Drink":
                    AnsiConsole.MarkupLine("[bold green]You selected Ordinary Drink![/]");
                    Dictionary<string, string> drinks = await ConnectionService.FetchDrinksByCategoryAsync();
                    if (drinks == null || drinks.Count == 0)
                    {
                        Console.WriteLine("No drinks available in this category.");
                        return;
                    }
                    var drinkSelection = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Please select a drink:")
                            .AddChoices(drinks.Keys)
                    );

                    // Retrieve the ID of the selected drink
                    string selectedDrinkId = drinks[drinkSelection];
                    Console.WriteLine($"You selected drink ID: {selectedDrinkId}");


                    break;
                    
                case "Cocktail":
                    AnsiConsole.MarkupLine("[bold green]You selected Cocktail![/]");
                    break;
                case "Shake":
                    AnsiConsole.MarkupLine("[bold green]You selected Shake![/]");
                    break;
                default:
                    AnsiConsole.MarkupLine("[bold red]Invalid selection![/]");
                    break;
            }
        }
    }
}
