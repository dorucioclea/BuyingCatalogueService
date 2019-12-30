using NHSD.BuyingCatalogue.Solutions.Contracts;

namespace NHSD.BuyingCatalogue.Solutions.API.ViewModels.Solution.NativeMobile
{
    public class NativeMobileFirstSection
    {
        public NativeMobileFirstSectionAnswers Answers { get; }

        public NativeMobileFirstSection(IClientApplication clientApplication) =>
            Answers = new NativeMobileFirstSectionAnswers(clientApplication);
    }
}