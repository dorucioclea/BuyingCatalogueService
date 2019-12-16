using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NHSD.BuyingCatalogue.Solutions.Application.Commands.Validation;
using NHSD.BuyingCatalogue.Solutions.Application.Persistence;

namespace NHSD.BuyingCatalogue.Solutions.Application.Commands.UpdateSolutionClientApplicationTypes
{
    internal sealed class UpdateSolutionClientApplicationTypesHandler : IRequestHandler<UpdateSolutionClientApplicationTypesCommand, RequiredResult>
    {
        private readonly ClientApplicationPartialUpdater _clientApplicationPartialUpdater;

        private readonly UpdateSolutionClientApplicationTypesValidator _updateSolutionClientApplicationTypesValidator;

        /// <summary>
        /// Initialises a new instance of the <see cref="UpdateSolutionClientApplicationTypesHandler"/> class.
        /// </summary>
        public UpdateSolutionClientApplicationTypesHandler(ClientApplicationPartialUpdater clientApplicationPartialUpdater,
            UpdateSolutionClientApplicationTypesValidator updateSolutionClientApplicationTypesValidator)
        {
            _clientApplicationPartialUpdater = clientApplicationPartialUpdater;
            _updateSolutionClientApplicationTypesValidator = updateSolutionClientApplicationTypesValidator;
        }

        /// <summary>
        /// Executes the action of this command.
        /// </summary>
        /// <param name="request">The command parameters.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>A task representing an operation to get the result of this command.</returns>
        public async Task<RequiredResult> Handle(UpdateSolutionClientApplicationTypesCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _updateSolutionClientApplicationTypesValidator.Validate(request.UpdateSolutionClientApplicationTypesViewModel);
            if (validationResult.IsValid)
            {
                await _clientApplicationPartialUpdater.UpdateAsync(request.SolutionId,
                    clientApplication => clientApplication.ClientApplicationTypes = new HashSet<string>(request.UpdateSolutionClientApplicationTypesViewModel.FilteredClientApplicationTypes),
                    cancellationToken)
                    .ConfigureAwait(false);
            }

            return validationResult;
        }
    }
}
