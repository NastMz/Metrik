namespace Metrik.Application.Abstractions.Interfaces.Localization
{
    /// <summary>
    /// Service for localizing text strings.
    /// </summary>
    public interface ILocalizationService
    {
        /// <summary>
        /// Retrieves a localized string based on the provided key.
        /// </summary>
        /// <param name="key">Localization key.</param>
        /// <param name="defaultValue">Default value if the key is not found.</param>
        /// <returns>Localized string.</returns>
        string GetLocalizedString(string key, string defaultValue);

        /// <summary>
        /// Sets the language for the current context.
        /// </summary>
        /// <param name="languageCode">Language code (e.g., "es", "en").</param>
        /// <returns>True if the language was successfully set.</returns>
        bool SetLanguage(string languageCode);
    }
}
