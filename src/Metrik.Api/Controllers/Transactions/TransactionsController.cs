using Metrik.Application.Abstractions.Interfaces.Localization;
using Metrik.Application.Extensions;
using Metrik.Application.Features.Transactions.CreateTransaction;
using Metrik.Application.Features.Transactions.GetTransaction;
using Metrik.Mediator.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Metrik.Api.Controllers.Transactions
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILocalizationService _localizationService;

        /// <summary>
        /// Constructor for <see cref="TransactionsController"/>.
        /// </summary>
        /// <param name="sender">The sender for handling commands and queries.</param>
        /// <param name="localizationService">The localization service for handling localization.</param>
        public TransactionsController(ISender sender, ILocalizationService localizationService)
        {
            _localizationService = localizationService;
            _sender = sender;
        }

        /// <summary>
        /// Retrieves a transaction by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the transaction to retrieve.</param>
        /// <returns>The transaction details.</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTransaction(Guid id)
        {
            var query = new GetTransactionQuery(id);

            var result = await _sender.Send(query);

            return result.ToActionResult(_localizationService, this);
        }

        /// <summary>
        /// Creates a new transaction.
        /// </summary>
        /// <param name="request">The request containing the transaction details.</param>
        /// <returns>The created transaction details.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateTransaction(CreateTransactionRequest request)
        {
            var command = new CreateTransactionCommand(
                request.UserId,
                request.AccountId,
                request.CategoryId,
                request.Amount,
                request.Type,
                request.Description
            );

            var result = await _sender.Send(command);

            return result.ToActionResult(_localizationService, this);
        }
    }
}
