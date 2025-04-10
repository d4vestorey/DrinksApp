using System.Text.Json.Serialization;

namespace DrinksApp
{
public record class Repository(
    [property: JsonPropertyName("strCategory")] string DrinkCategory,
    [property: JsonPropertyName("strDrink")] string DrinkName,
    [property: JsonPropertyName("idDrink")] string DrinkID,
    [property: JsonPropertyName("strInstructions")] string DrinkInstructions,
    [property: JsonPropertyName("strGlass")] string GlassType,
    [property: JsonPropertyName("strDrinkThumb")] string DrinkImage
);
}