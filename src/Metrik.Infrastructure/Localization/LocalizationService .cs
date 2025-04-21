using Metrik.Application.Abstractions.Interfaces.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace Metrik.Infrastructure.Localization
{
    /// <summary>
    /// Configuration options for localization.
    /// </summary>
    public class LocalizationOptions
    {
        /// <summary>
        /// Default language.
        /// </summary>
        public string DefaultLanguage { get; set; } = "en";

        /// <summary>
        /// Supported languages.
        /// </summary>
        public List<string> SupportedLanguages { get; set; } = new List<string> { "en" };
    }

    /// <summary>
    /// Service for managing localization and retrieving localized strings.
    /// </summary>
    public class LocalizationService : ILocalizationService
    {
        private readonly IStringLocalizerFactory _localizerFactory;
        private readonly ILogger<LocalizationService> _logger;
        private readonly Dictionary<string, IStringLocalizer> _localizers = new();
        private readonly LocalizationOptions _options;
        private CultureInfo _currentCulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationService"/> class.
        /// </summary>
        /// <param name="localizerFactory">Factory for creating string localizers.</param>
        /// <param name="options">Localization options.</param>
        /// <param name="logger">Logger instance.</param>
        public LocalizationService(
            IStringLocalizerFactory localizerFactory,
            IOptions<LocalizationOptions> options,
            ILogger<LocalizationService> logger)
        {
            _localizerFactory = localizerFactory;
            _logger = logger;
            _options = options.Value;
            _currentCulture = new CultureInfo(_options.DefaultLanguage);
        }

        /// <summary>
        /// Retrieves a localized string based on the provided key.
        /// </summary>
        /// <param name="key">The localization key.</param>
        /// <param name="defaultValue">The default value to return if the key is not found.</param>
        /// <returns>The localized string or the default value if not found.</returns>
        public string GetLocalizedString(string key, string defaultValue)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    _logger.LogWarning("Empty key provided to GetLocalizedString");
                    return defaultValue;
                }

                _logger.LogDebug("Looking for resource with key: {Key}", key);

                // Attempt to determine the resource location based on the key prefix
                string resourcePath = GetResourcePath(key);
                _logger.LogDebug("Using resource path: {ResourcePath}", resourcePath);

                // Get or create the localizer
                var localizer = GetOrCreateLocalizer(resourcePath);
                if (localizer == null)
                {
                    _logger.LogWarning("Failed to create localizer for path: {ResourcePath}", resourcePath);
                    return defaultValue;
                }

                // Save the current culture
                var originalCulture = CultureInfo.CurrentUICulture;

                try
                {
                    // Temporarily set the user's culture
                    CultureInfo.CurrentUICulture = _currentCulture;

                    // Look for the text directly by the full key
                    var localizedString = localizer[key];

                    // Log whether the resource was found or not
                    if (localizedString.ResourceNotFound)
                    {
                        _logger.LogWarning("Resource not found for key: {Key} in {Culture}",
                            key, _currentCulture);
                        return defaultValue;
                    }

                    _logger.LogDebug("Found localized string for key: {Key}", key);
                    return localizedString.Value;
                }
                finally
                {
                    // Restore the original culture
                    CultureInfo.CurrentUICulture = originalCulture;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error localizing string for key: {Key}", key);
                return defaultValue;
            }
        }

        /// <summary>
        /// Sets the language for the current context.
        /// </summary>
        /// <param name="languageCode">The language code (e.g., "en", "es").</param>
        /// <returns>True if the language was successfully set; otherwise, false.</returns>
        public bool SetLanguage(string languageCode)
        {
            if (!_options.SupportedLanguages.Contains(languageCode))
            {
                _logger.LogWarning("Attempted to set unsupported language: {Language}", languageCode);
                return false;
            }

            try
            {
                _currentCulture = new CultureInfo(languageCode);
                _logger.LogInformation("Language set to: {Language}", languageCode);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to set language to: {Language}", languageCode);
                _currentCulture = new CultureInfo(_options.DefaultLanguage);
                return false;
            }
        }

        /// <summary>
        /// Determines the resource path based on the provided key.
        /// </summary>
        /// <param name="key">The localization key.</param>
        /// <returns>The resource path.</returns>
        private string GetResourcePath(string key)
        {
            // Example: "Errors.Transaction.NotFound" becomes "Errors.Transactions"
            var parts = key.Split('.');

            if (parts.Length >= 2 && parts[0] == "Errors")
            {
                // Get the domain (singular or plural)
                string domain = parts[1];

                // Default to using as-is
                return $"Errors.{domain}";
            }

            // If the pattern is not as expected, use Common
            return "Errors.Common";
        }

        /// <summary>
        /// Retrieves or creates a string localizer for the specified resource path.
        /// </summary>
        /// <param name="resourcePath">The resource path.</param>
        /// <returns>The string localizer instance.</returns>
        private IStringLocalizer GetOrCreateLocalizer(string resourcePath)
        {
            // Check if this localizer is already cached
            if (_localizers.TryGetValue(resourcePath, out var cachedLocalizer))
            {
                return cachedLocalizer;
            }

            try
            {
                _logger.LogDebug("Creating localizer for {ResourcePath}", resourcePath);

                var resourceAssemblyName = typeof(Metrik.Resources.ResourceMarker).Assembly.GetName().Name;
                _logger.LogDebug("Using assembly: {AssemblyName}", resourceAssemblyName);

                // Create the localizer with the full resource path
                var localizer = _localizerFactory.Create(resourcePath, resourceAssemblyName);

                // Cache for future use
                _localizers[resourcePath] = localizer;

                return localizer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create localizer for {ResourcePath}", resourcePath);

                // Attempt fallback to common resources
                try
                {
                    if (resourcePath != "Errors.Common")
                    {
                        _logger.LogDebug("Trying fallback to Common resources");
                        return GetOrCreateLocalizer("Errors.Common");
                    }
                }
                catch
                {
                    // Fail silently
                }

                return null;
            }
        }
    }
}
