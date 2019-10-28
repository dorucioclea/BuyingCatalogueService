using NHSD.BuyingCatalogue.Domain.Entities.Solutions;

namespace NHSD.BuyingCatalogue.API.ViewModels
{
    public class SolutionDescriptionSection : Section
    {
        internal SolutionDescriptionSection(Solution solution)
        {
            Data = new SolutionDescription(solution);
            _isComplete = !string.IsNullOrWhiteSpace(solution.Summary);
            Mandatory.Add("summary");
        }

        public override string Id => "solution-description";

        public SolutionDescription Data { get; }
    }
}