using NHSD.BuyingCatalogue.Solutions.Application.Commands.Validation;

namespace NHSD.BuyingCatalogue.Solutions.Application.Commands.UpdateSolutionBrowserMobileFirst
{
    internal sealed class UpdateSolutionBrowserMobileFirstValidator : IValidator<UpdateSolutionBrowserMobileFirstCommand, RequiredResult>
    {
        public RequiredResult Validate(UpdateSolutionBrowserMobileFirstCommand updateSolutionBrowserMobileFirstCommand)
            => new RequiredValidator()
                .Validate(updateSolutionBrowserMobileFirstCommand.UpdateSolutionBrowserMobileFirstViewModel.MobileFirstDesign, "mobile-first-design")
                .Result();
    }
}
