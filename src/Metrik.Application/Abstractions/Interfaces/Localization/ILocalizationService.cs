namespace Metrik.Application.Abstractions.Interfaces.Localization
{
    /// <summary>
    /// Provides localization services for the application.
    /// </summary>
    public interface ILocalizationService
    {
        /// <summary>
        /// Gets a localized string for the specified key.
        /// </summary>
        /// <param name="key">The localization key.</param>
        /// <param name="defaultValue">The default value if the key is not found.</param>
        /// <returns>The localized string.</returns>
        string GetLocalizedString(string key, string defaultValue);
    }
}
