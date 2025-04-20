namespace Metrik.Application.Exceptions
{
    /// <summary>
    /// Represents a validation error.
    /// </summary>
    /// <param name="PropertyName">The name of the property that caused the validation error.</param>
    /// <param name="ErrorMessage">The error message describing the validation error.</param>
    public sealed record ValidationError(string PropertyName, string ErrorMessage);
}
