using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NHSD.BuyingCatalogue.API.IntegrationTests.Support;
using TechTalk.SpecFlow;

namespace NHSD.BuyingCatalogue.API.IntegrationTests.Steps
{
    [Binding]
    internal sealed class SolutionDescriptionSectionStepsValidation
    {
        private const string SolutionDescriptionUrl = "http://localhost:8080/api/v1/Solutions/{0}/sections/solution-description";

        private readonly ScenarioContext _context;

        private readonly Response _response;

        public SolutionDescriptionSectionStepsValidation(ScenarioContext context, Response response)
        {
            _context = context;
            _response = response;
        }

        [Given(@"a request where the (summary|description|link) is a string of (.*) characters")]
        public void GivenARequestWhereTheFieldIsAStringOfCharacters(string field, int length)
        {
            _context[field] = GenerateStringOfLength(length);
        }

        [When(@"the update solution description request is made for (.*)")]
        public async Task WhenTheRequestIsMade(string solutionId)
        {
            var content = new
            {
                summary = _context.ContainsKey("summary") ? _context["summary"] : "DUMMY",
                description = _context.ContainsKey("description") ? _context["description"] : "DUMMY",
                link = _context.ContainsKey("link") ? _context["link"] : "DUMMY"
            };

            _response.Result = await Client.PutAsJsonAsync(string.Format(SolutionDescriptionUrl, solutionId), content);
        }

        [Then(@"the response required field contains summary")]
        public async Task ThenTheResponseRequiredFieldContainsSummary()
        {
            var content = await _response.ReadBody();
            content.SelectToken("required").ToString().Should().Contain("summary");
        }

        [Then(@"the response maxlength field contains (summary|description|link)")]
        public async Task ThenTheResponseMaxlengthFieldContainsSummary(string field)
        {
            var content = await _response.ReadBody();
            content.SelectToken("maxLength").ToString().Should().Contain(field);
        }


        private string GenerateStringOfLength(int chars)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < chars; i++)
            {
                builder.Append("a");
            }

            return builder.ToString();
        }
    }
}