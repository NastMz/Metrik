namespace Metrik.Domain.Entities.Accounts.ValueObjects
{
    /// <summary>
    /// Represents the type of an account (e.g., checking, savings).
    /// </summary>
    /// <param name="Value">The type of the account.</param>
    public record AccountType(string Value);
}
