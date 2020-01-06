using NHSD.BuyingCatalogue.Solutions.Application.Commands.Execution;
using NHSD.BuyingCatalogue.Solutions.Application.Commands.Validation;

namespace NHSD.BuyingCatalogue.Solutions.Application.Commands.NativeMobile.UpdateSolutionMobileMemoryAndStorage
{
    internal sealed class UpdateSolutionMobileMemoryStorageHandler : Handler<UpdateSolutionMobileMemoryStorageCommand, ISimpleResult>
    {
        public UpdateSolutionMobileMemoryStorageHandler(IExecutor<UpdateSolutionMobileMemoryStorageCommand> updateSolutionMobileMemoryStorageExecutor,
            IValidator<UpdateSolutionMobileMemoryStorageCommand, ISimpleResult> updateSolutionMobileMemoryStorageValidator) : base(updateSolutionMobileMemoryStorageExecutor, updateSolutionMobileMemoryStorageValidator)
        {
        }
    }
}