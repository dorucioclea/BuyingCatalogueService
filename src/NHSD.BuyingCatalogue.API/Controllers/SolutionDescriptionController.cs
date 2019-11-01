using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHSD.BuyingCatalogue.API.ViewModels;
using NHSD.BuyingCatalogue.Application.Solutions.Commands.UpdateSolution;
using NHSD.BuyingCatalogue.Application.Solutions.Queries.GetSolutionById;
using NHSD.BuyingCatalogue.Domain.Entities.Solutions;

namespace NHSD.BuyingCatalogue.API.Controllers
{
    /// <summary>
    /// Provides the endpoint to manage the solution description section of the solution marketing data.
    /// </summary>
    [Route("api/v1/solutions")]
    [ApiController]
    [Produces("application/json")]
    [AllowAnonymous]
    public class SolutionDescriptionController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initialises a new instance of the <see cref="SolutionDescriptionController"/> class.
        /// </summary>
        public SolutionDescriptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the solution description section of a solution matching the supplied ID.
        /// </summary>
        /// <param name="id">A value to uniquely identify a solution.</param>
        /// <returns>A task representing an operation to retrieve the details of the solution description section of a solution.</returns>
        [HttpGet]
        [Route("{id}/sections/solution-description")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetSolutionDescriptionAsync([FromRoute][Required]string id)
        {
            var solution = await _mediator.Send(new GetSolutionByIdQuery(id));
            return solution == null ? (ActionResult)new NotFoundResult() : Ok(new SolutionDescriptionResult(solution.Summary, solution.Description, solution.AboutUrl));
        }

        /// <summary>
        /// Updates the solution description section of a solution matching the supplied ID.
        /// </summary>
        /// <param name="id">A value to uniquely identify a solution.</param>
        /// <param name="updateSolutionSummaryViewModel">The details of a solution description section that includes any updated information.</param>
        /// <returns>A task representing an operation to update the details of the solution description section of a solution.</returns>
        [HttpPut]
        [Route("{id}/sections/solution-description")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> UpdateAsync([FromRoute][Required]string id, [FromBody][Required]UpdateSolutionSummaryViewModel updateSolutionSummaryViewModel)
        {
            await _mediator.Send(new UpdateSolutionSummaryCommand(id, updateSolutionSummaryViewModel));

            return NoContent();
        }
    }
}
