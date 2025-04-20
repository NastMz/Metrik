using FluentValidation;

namespace Metrik.Application.Features.Transactions.CreateTransaction
{
    /// <summary>
    /// Validator for the <see cref="CreateTransactionCommand"/> class.
    /// </summary>
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTransactionCommandValidator"/> class and sets up the validation rules.
        /// </summary>
        public CreateTransactionCommandValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.");
        }
    }
}
