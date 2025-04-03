using Spectre.Console;

namespace DrinksApp
{
    public class Menu
    {

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


            AnsiConsole.MarkupLine("[bold green]You selected Ordinary Drink![/]");

            Dictionary<string, string> drinks = await ConnectionService.FetchDrinksByCategoryAsync(selection);

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
            int.TryParse(drinks[drinkSelection], out int selectedDrinkId);
            Console.WriteLine($"You selected drink ID: {selectedDrinkId}");

            // Fetch drink details
            var drinkDetails = await ConnectionService.FetchDrinkDetailsAsync(selectedDrinkId);

            if (drinkDetails == null)
            {
                Console.WriteLine("No details available for this drink.");
                return;
            }

            // Create a table to display drink details
            var table = new Table();

            // Add columns to the table
            table.AddColumn(new TableColumn("[yellow][/]").Centered());
            table.AddColumn(new TableColumn($"[yellow]{drinkDetails[0]}[/]").Centered());

            // Add rows with drink details
            table.AddRow("Instructions", drinkDetails[1]);
            table.AddRow("Category", drinkDetails[2]);
            table.AddRow("Recommended glass", drinkDetails[3]);

            // Render the table
            AnsiConsole.Write(table);


        }
    }
}
