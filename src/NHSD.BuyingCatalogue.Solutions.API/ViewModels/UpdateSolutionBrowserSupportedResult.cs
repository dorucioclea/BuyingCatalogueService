using System.Collections.Generic;
using NHSD.BuyingCatalogue.Solutions.Application.Commands.UpdateSolutionBrowsersSupported;

namespace NHSD.BuyingCatalogue.Solutions.API.ViewModels
{
    public class UpdateSolutionBrowserSupportedResult
    {
        public UpdateSolutionBrowserSupportedResult(UpdateSolutionBrowserSupportedValidationResult updateSolutionBrowserSupportedValidationResult)
        {
            Required = updateSolutionBrowserSupportedValidationResult.Required;
        }

        public HashSet<string> Required { get; }
    }
}