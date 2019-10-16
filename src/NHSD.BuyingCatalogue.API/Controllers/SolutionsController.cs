using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHSD.BuyingCatalogue.Application.Solutions.Commands.SubmitForReview;
using NHSD.BuyingCatalogue.Application.Solutions.Commands.UpdateSolution;
using NHSD.BuyingCatalogue.Application.Solutions.Queries.GetSolutionById;
using NHSD.BuyingCatalogue.Application.Solutions.Queries.ListSolutions;

namespace NHSD.BuyingCatalogue.API.Controllers
{
    /// <summary>
    /// Provides the endpoint for the summary of <see cref="Solution"/> information.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [AllowAnonymous]
    public sealed class SolutionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initialises a new instance of the <see cref="SolutionsController"/> class.
        /// </summary>
        public SolutionsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Gets a list of solutions that includes information about the organisation and the associated capabilities.
        /// </summary>
        /// <returns>A result containing a list of solutions that includes information about the organisation and the associated capabilities.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ListSolutionsResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ListSolutionsResult>> ListAsync()
        {
            return Ok(await _mediator.Send(new ListSolutionsQuery()));
        }

        /// <summary>
        /// Find a list of Solutions that match the specified list of Capabilities.
        /// </summary>
        /// <param name="filter">The filter criteria to apply to the list of Solutions.</param>
        /// <returns>A task representing an operation to retrieve a list of Solutions that match a set of Capabilities.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ListSolutionsResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ListSolutionsResult>> ListByFilterAsync([FromBody][Required]ListSolutionsFilter filter)
        {
            return Ok(await _mediator.Send(new ListSolutionsQuery(filter)));
        }

        /// <summary>
        /// Get a solution with specified unique identifier
        /// </summary>
        /// <param name="id">unique identifier of solution</param>
        /// <returns>Solution with specified unique identifier</returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(GetSolutionByIdResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetSolutionByIdResult>> ById([FromRoute][Required]string id)
        {
            GetSolutionByIdResult result = await _mediator.Send(new GetSolutionByIdQuery(id));
            return result.Solution == null ? (ActionResult)new NotFoundResult() : Ok(result);
        }

        /// <summary>
        /// Submits a solution for review.
        /// </summary>
        /// <param name="id">A value to uniquely identify a solution.</param>
        /// <returns>A task respresenting an operation to update the state of a solution to submitted for review.</returns>
        [HttpPut]
        [Route("{id}/SubmitForReview")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> SubmitForReviewAsync([FromRoute][Required] string id)
        {
            await _mediator.Send(new SubmitSolutionForReviewCommand(id));

            return NoContent();
        }
    }
}
