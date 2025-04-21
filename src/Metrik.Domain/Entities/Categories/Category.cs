using Metrik.Domain.Abstractions.Models;
using Metrik.Domain.Entities.Categories.ValueObjects;

namespace Metrik.Domain.Entities.Categories
{
    /// <summary>
    /// Represents a category in the system.
    /// </summary>
    public class Category : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class with the specified parameters.
        /// </summary>
        /// <param name="id">The unique identifier for the category.</param>
        /// <param name="userId">The unique identifier for the user who owns the category.</param>
        /// <param name="categoryName">The name of the category.</param>
        /// <param name="categoryType">The type of the category.</param>
        public Category(Guid id, Guid userId, CategoryName categoryName, CategoryType categoryType) : base(id)
        {
            UserId = userId;
            Name = categoryName;
            Type = categoryType;
        }

        /// <summary>
        /// Default constructor for the Category class.
        /// </summary>
        private Category()
        {
        }

        /// <summary>
        /// The unique identifier for the user who owns the category.
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Name of the category.
        /// </summary>
        public CategoryName Name { get; private set; }

        /// <summary>
        /// Type of the category.
        /// </summary>
        public CategoryType Type { get; private set; }
    }
}
