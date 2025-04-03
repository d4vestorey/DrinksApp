using System.Text.Json;

namespace DrinksApp
{
    public class ConnectionService
    {
        private static readonly HttpClient client = new();

        public static async Task<List<string>> FetchDrinkCategoriesAsync()
        {
            var repositories = await ProcessDrinkCategoriesAsync(client);

            List<string> drinkCategories = new List<string>();

            foreach (var repo in repositories.drinks)
            {
                drinkCategories.Add(repo.DrinkCategory);
            }

            return drinkCategories;
        }

        public static async Task<Dictionary<string, string>> FetchDrinksByCategoryAsync(string category)
        {
            var repositories = await ProcessDrinksByCategoriesAsync(client, category);

            Dictionary<string, string> drinksByCategory = new Dictionary<string, string>();

            foreach (var repo in repositories.drinks)
            {
                drinksByCategory.Add(repo.DrinkName, repo.DrinkID);
            }

            return drinksByCategory;
        }


        public static async Task<List<string>> FetchDrinkDetailsAsync(int id)
        {
            var repositories = await ProcessDrinkAsync(client, id);

            List<string> drinkById = new List<string>();

            foreach (var repo in repositories.drinks)
            {
                drinkById.Add(repo.DrinkName);
                drinkById.Add(repo.DrinkInstructions);
                drinkById.Add(repo.DrinkCategory);
                drinkById.Add(repo.GlassType);
            }

            return drinkById;
        }


        static async Task<CategoryResponse> ProcessDrinkCategoriesAsync(HttpClient client)
        {
            await using Stream stream =
              await client.GetStreamAsync("https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list");
            var repositories =
                await JsonSerializer.DeserializeAsync<CategoryResponse>(stream);
            return repositories ?? new();
        }

        static async Task<CategoryResponse> ProcessDrinksByCategoriesAsync(HttpClient client, string category)
        {
            await using Stream stream =
              await client.GetStreamAsync($"https://www.thecocktaildb.com/api/json/v1/1/filter.php?c={category}");
            var repositories =
                await JsonSerializer.DeserializeAsync<CategoryResponse>(stream);
            return repositories ?? new();
        }

        static async Task<CategoryResponse> ProcessDrinkAsync(HttpClient client, int id)
        {
            // Example URL: https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i=11007
            // Replace 11007 with the actual drink ID you want to fetch
            // await using Stream stream =
            //   await client.GetStreamAsync($"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={id}");
            {
                await using Stream stream =
                  await client.GetStreamAsync($"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={id}");
                var repositories =
                    await JsonSerializer.DeserializeAsync<CategoryResponse>(stream);
                return repositories ?? new();
            }
        }
    }
}
