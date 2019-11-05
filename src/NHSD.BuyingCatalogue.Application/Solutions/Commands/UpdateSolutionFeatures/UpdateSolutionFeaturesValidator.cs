using System.Linq;

namespace NHSD.BuyingCatalogue.Application.Solutions.Commands.UpdateSolutionFeatures
{
    internal sealed class UpdateSolutionFeaturesValidator
    {
        public UpdateSolutionFeaturesValidatorResult Validate(
            UpdateSolutionFeaturesViewModel updateSolutionFeaturesViewModel)
        {
            var listing = updateSolutionFeaturesViewModel.Listing.ToList();
            var validationResult = new UpdateSolutionFeaturesValidatorResult();

            for (int i = 0; i < listing.Count(); i++)
            {
                if ((listing[i]?.Length ?? 0) > 100)
                {
                    validationResult.MaxLength.Add($"listing-{i+1}");
                }
            }

            return validationResult;
        }
    }
}