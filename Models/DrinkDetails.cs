using Newtonsoft.Json;

namespace DrinksApp.Models
{
    public class DrinkDetail
    {
        public string? strDrink { get; set; }
        public string? strInstructions { get; set; }
        public string? strGlass { get; set; }
        public string? strCategory { get; set; }
        public string? strIngredient1 { get; set; }
        public string? strIngredient2 { get; set; }
        public string? strIngredient3 { get; set; }
        public string? strIngredient4 { get; set; }
        public string? strIngredient5 { get; set; }
        public string? strIngredient6 { get; set; }
        public string? strIngredient7 { get; set; }
        public string? strIngredient8 { get; set; }
        public string? strIngredient9 { get; set; }
        public string? strIngredient10 { get; set; }
        public string? strIngredient11 { get; set; }
        public string? strIngredient12 { get; set; }
        public string? strIngredient13 { get; set; }
        public string? strIngredient14 { get; set; }
        public string? strIngredient15 { get; set; }
    }

    public class DrinkDetails
    {
        [JsonProperty("drinks")]
        public List<DrinkDetail>? DrinkInfo { get; set; }
    }
}