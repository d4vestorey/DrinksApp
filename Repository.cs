using System.Text.Json.Serialization;

namespace DrinksApp
{
public record class Repository(
    [property: JsonPropertyName("strCategory")] string DrinkCategory,
    [property: JsonPropertyName("strDrink")] string DrinkName,
    [property: JsonPropertyName("idDrink")] string DrinkID
);
}