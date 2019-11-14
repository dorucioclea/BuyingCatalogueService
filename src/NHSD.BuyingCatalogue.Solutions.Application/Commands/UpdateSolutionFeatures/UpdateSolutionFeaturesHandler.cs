using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NHSD.BuyingCatalogue.Solutions.Application.Domain;
using NHSD.BuyingCatalogue.Solutions.Application.Persistence;

namespace NHSD.BuyingCatalogue.Solutions.Application.Commands.UpdateSolutionFeatures
{
    internal sealed class UpdateSolutionFeaturesHandler : IRequestHandler<UpdateSolutionFeaturesCommand, UpdateSolutionFeaturesValidationResult>
    {
        private readonly SolutionReader _solutionReader;
        private readonly SolutionFeaturesUpdater _solutionFeaturesUpdater;
        private readonly IMapper _mapper;
        private readonly UpdateSolutionFeaturesValidator _updateSolutionFeaturesValidator;

        /// <summary>
        /// Initialises a new instance of the <see cref="UpdateSolutionHandler"/> class.
        /// </summary>
        public UpdateSolutionFeaturesHandler(SolutionReader solutionReader, SolutionFeaturesUpdater solutionFeaturesUpdater, IMapper mapper, UpdateSolutionFeaturesValidator updateSolutionFeaturesValidator)
        {
            _solutionReader = solutionReader;
            _solutionFeaturesUpdater = solutionFeaturesUpdater;
            _mapper = mapper;
            _updateSolutionFeaturesValidator = updateSolutionFeaturesValidator;
        }

        /// <summary>
        /// Executes the action of this command.
        /// </summary>
        /// <param name="request">The command parameters.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>A task representing an operation to get the result of this command.</returns>
        public async Task<UpdateSolutionFeaturesValidationResult> Handle(UpdateSolutionFeaturesCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _updateSolutionFeaturesValidator.Validate(request.UpdateSolutionFeaturesViewModel);

            if (validationResult.IsValid)
            {
                Solution solution = await _solutionReader.ByIdAsync(request.SolutionId, cancellationToken);
                await _solutionFeaturesUpdater.UpdateAsync(
                    _mapper.Map(request.UpdateSolutionFeaturesViewModel, solution), cancellationToken);
            }

            return validationResult;
        }
    }
}