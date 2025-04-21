using Metrik.Application.Abstractions.Interfaces.Localization;
using Microsoft.Extensions.Options;

namespace Metrik.Infrastructure.Localization
{
    /// <summary>
    /// Middleware to set the request language based on HTTP headers.
    /// </summary>
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly LocalizationOptions _options;

        /// <summary>
        /// Initializes a new instance of the middleware.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="options">Localization options.</param>
        public LocalizationMiddleware(
            RequestDelegate next,
            IOptions<LocalizationOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        /// <summary>
        /// Processes the HTTP request.
        /// </summary>
        /// <param name="context">HTTP context.</param>
        /// <param name="localizationService">Localization service.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task InvokeAsync(
            HttpContext context,
            ILocalizationService localizationService)
        {
            var acceptLanguage = context.Request.Headers["Accept-Language"].ToString();
            var userLanguage = _options.DefaultLanguage;

            if (!string.IsNullOrEmpty(acceptLanguage))
            {
                var languages = acceptLanguage.Split(',')
                    .Select(lang => lang.Split(';').FirstOrDefault()?.Trim())
                    .Where(lang => !string.IsNullOrEmpty(lang))
                    .ToList();

                foreach (var lang in languages)
                {
                    // Handle cases like "en-US" or "es-ES"
                    var shortLang = lang!.Split('-').FirstOrDefault();
                    if (!string.IsNullOrEmpty(shortLang) && _options.SupportedLanguages.Contains(shortLang))
                    {
                        userLanguage = shortLang;
                        break;
                    }
                }
            }

            localizationService.SetLanguage(userLanguage);

            await _next(context);
        }
    }
}
