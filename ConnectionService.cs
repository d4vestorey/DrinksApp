using System.Reflection;
using DrinksApp.Models;
using Newtonsoft.Json;

namespace DrinksApp
{
    public class ConnectionService
    {
        private static readonly HttpClient client = new();

        public static async Task<List<string>> FetchDrinkCategoriesAsync()
        {
            var repositories = await ProcessDrinkCategoriesAsync(client);

            List<string> drinkCategories = new List<string>();

            foreach (var repo in repositories.CategoriesList)
            {
                drinkCategories.Add(repo.strCategory);
            }

            return drinkCategories;
        }

        public static async Task<Dictionary<string, string>> FetchDrinksByCategoryAsync(string category)
        {
            var repositories = await ProcessDrinksByCategoriesAsync(client, category);

            Dictionary<string, string> drinksByCategory = new Dictionary<string, string>();

            foreach (var repo in repositories.DrinksList)
            {
                drinksByCategory.Add(repo.strDrink, repo.idDrink);
            }

            return drinksByCategory;
        }


        public static async Task<List<object>> FetchDrinkDetailsAsync(int id)
        {
            var repositories = await ProcessDrinkAsync(client, id);

            List<object> drinkById = new List<object>();

            // Make sure there's at least one drink in the list
            var drink = repositories.DrinkInfo?.FirstOrDefault();

            if (drink == null)
            {
                Console.WriteLine("No drink found.");
                return drinkById;
            }

            // Loop through the properties of the first DrinkDetail object
            foreach (PropertyInfo prop in drink.GetType().GetProperties())
            {
                var value = prop.GetValue(drink);
                if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
                {
                    string key = prop.Name.StartsWith("str") ? prop.Name.Substring(3) : prop.Name;
                    drinkById.Add(new
                    {
                        Key = key,
                        Value = value
                    });
                }
            }
            return drinkById;
        }


        static async Task<Categories> ProcessDrinkCategoriesAsync(HttpClient client)
        {
            var stream =
              await client.GetStringAsync("https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list");
            var repositories =
                JsonConvert.DeserializeObject<Categories>(stream);


            if (repositories == null || repositories.CategoriesList == null)
            {
                Console.WriteLine("Failed to deserialize Categories.");
                return new Categories { CategoriesList = new List<Category>() };
            }

            return repositories ?? new();
        }

        static async Task<Drinks> ProcessDrinksByCategoriesAsync(HttpClient client, string category)
        {
            var stream =
              await client.GetStringAsync($"https://www.thecocktaildb.com/api/json/v1/1/filter.php?c={category}");
            var repositories =
                JsonConvert.DeserializeObject<Drinks>(stream);
            return repositories ?? new();
        }

        static async Task<DrinkDetails> ProcessDrinkAsync(HttpClient client, int id)
        {
            // Example URL: https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i=11007
            // Replace 11007 with the actual drink ID you want to fetch
            // await using Stream stream =
            //   await client.GetStreamAsync($"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={id}");
            {
                var stream =
                  await client.GetStringAsync($"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={id}");
                var repositories =
                    JsonConvert.DeserializeObject<DrinkDetails>(stream);
                return repositories ?? new();
            }
        }
    }
}
