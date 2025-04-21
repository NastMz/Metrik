using FluentValidation;
using Metrik.Application.Abstractions.Interfaces.Localization;
using Metrik.Domain.Entities.Transactions.Enums;
using Metrik.Domain.Entities.Transactions.Errors;

namespace Metrik.Application.Features.Transactions.CreateTransaction
{
    /// <summary>
    /// Validator for the <see cref="CreateTransactionCommand"/> class.
    /// </summary>
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        private readonly ILocalizationService _localizationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTransactionCommandValidator"/> class and sets up the validation rules.
        /// </summary>
        public CreateTransactionCommandValidator(ILocalizationService localizationService)
        {
            _localizationService = localizationService;

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage(_ => _localizationService.GetLocalizedString(
                    TransactionErrors.InvalidAmount.GetLocalizationKey(),
                    TransactionErrors.InvalidAmount.Name));

            RuleFor(x => x.Description)
                .Must(x => x.Length <= 200 && x.Length > 0)
                .WithMessage(_ => _localizationService.GetLocalizedString(
                    TransactionErrors.InvalidDescription.GetLocalizationKey(),
                    TransactionErrors.InvalidDescription.Name));

            RuleFor(x => x.Type)
                .Must(type => Enum.IsDefined(typeof(TransactionType), type))
                .WithMessage(_ => _localizationService.GetLocalizedString(
                    TransactionErrors.InvalidTransactionType.GetLocalizationKey(),
                    TransactionErrors.InvalidTransactionType.Name));
        }
    }
}
