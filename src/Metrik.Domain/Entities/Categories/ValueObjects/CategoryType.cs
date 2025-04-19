namespace Metrik.Domain.Entities.Categories.ValueObjects
{
    /// <summary>
    /// Represents the type of a category. (e.g., income, expense).
    /// </summary>
    /// <param name="Value">The type of the category.</param>
    public record class CategoryType(string Value);
}
