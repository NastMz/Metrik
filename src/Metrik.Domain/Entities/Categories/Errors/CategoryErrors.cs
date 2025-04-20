using Metrik.Domain.Abstractions.Models;

namespace Metrik.Domain.Entities.Categories.Errors
{
    public static class CategoryErrors
    {
        /// <summary>
        /// Error indicating that a category was not found.
        /// </summary>
        public static readonly Error NotFound = new(
            "Category.NotFound",
            "The category with the specified identifier was not found.",
            ErrorType.NotFound,
            "Errors.Category.NotFound"
        );
    }
}
