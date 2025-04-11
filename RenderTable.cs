using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;

namespace DrinksApp
{
    public class RenderTable
    {
        internal void CreateTable(List<object> drinkDetails)
        {
            // Create a new table
            var table = new Table();

            // Add columns to the table
            table.AddColumn(new TableColumn("").Centered());
            table.AddColumn(new TableColumn($"Drink Info").Centered());

            // Add rows to the table
            foreach (var item in drinkDetails)
            {
                var key = item.GetType().GetProperty("Key")?.GetValue(item)?.ToString();
                var value = item.GetType().GetProperty("Value")?.GetValue(item)?.ToString();

                table.AddRow(key ?? "Unknown", value ?? "Unknown");
            }

            // Set the border and title of the table
            table.Border(TableBorder.Rounded);
            table.Title("Drink Details");

            // Add the table to the console
            AnsiConsole.Write(table);
        }
        
    }
}