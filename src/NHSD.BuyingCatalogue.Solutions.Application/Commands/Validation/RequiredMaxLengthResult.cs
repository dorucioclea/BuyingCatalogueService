using System.Collections.Generic;

namespace NHSD.BuyingCatalogue.Solutions.Application.Commands.Validation
{
    public class RequiredMaxLengthResult
    {
        private readonly RequiredResult _requiredResult;
        private readonly MaxLengthResult _maxLengthResult;

        public RequiredMaxLengthResult()
        {
            _requiredResult = new RequiredResult();
            _maxLengthResult = new MaxLengthResult();
        }

        public HashSet<string> Required => _requiredResult.Required;

        public HashSet<string> MaxLength => _maxLengthResult.MaxLength;

        public bool IsValid => _requiredResult.IsValid && _maxLengthResult.IsValid;
    }
}
