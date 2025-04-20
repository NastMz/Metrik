using Metrik.Application.Abstractions.Interfaces.Localization;
using Metrik.Domain.Abstractions.Models;

namespace Metrik.Application.Extensions
{
    /// <summary>
    /// Extension methods for the <see cref="Error"/> class.
    /// </summary>
    public static class ErrorExtensions
    {
        /// <summary>
        /// Gets the localized message for an error.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="localizationService">The localization service.</param>
        /// <returns>The localized error message.</returns>
        public static string GetLocalizedMessage(this Error error, ILocalizationService localizationService)
        {
            if (error == Error.None)
                return string.Empty;

            return localizationService.GetLocalizedString(error.GetLocalizationKey(), error.Name);
        }
    }
}
