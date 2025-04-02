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

        public static async Task<Dictionary<string,string>> FetchDrinksByCategoryAsync()
        {
            var repositories = await ProcessDrinksByCategoriesAsync(client);

            Dictionary<string, string> drinksByCategory = new Dictionary<string, string>();

            foreach (var repo in repositories.drinks)
            {
                drinksByCategory.Add(repo.DrinkName,repo.DrinkID);
            }

            return drinksByCategory;
        }


        static async Task<CategoryResponse> ProcessDrinkCategoriesAsync(HttpClient client)
        {
            await using Stream stream =
              await client.GetStreamAsync("https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list");
            var repositories =
                await JsonSerializer.DeserializeAsync<CategoryResponse>(stream);
            return repositories ?? new();
        }

        static async Task<CategoryResponse> ProcessDrinksByCategoriesAsync(HttpClient client)
        {
            await using Stream stream =
              await client.GetStreamAsync("https://www.thecocktaildb.com/api/json/v1/1/filter.php?c=Ordinary_Drink");
            var repositories =
                await JsonSerializer.DeserializeAsync<CategoryResponse>(stream);
            return repositories ?? new();
        }

        static async Task<CategoryResponse> ProcessDrinksAsync(HttpClient client)
        {
            await using Stream stream =
              await client.GetStreamAsync("https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i=11007");
            var repositories =
                await JsonSerializer.DeserializeAsync<CategoryResponse>(stream);
            return repositories ?? new();
        }
    }
}