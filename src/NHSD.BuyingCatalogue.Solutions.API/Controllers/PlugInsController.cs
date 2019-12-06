using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHSD.BuyingCatalogue.Solutions.API.ViewModels;
using NHSD.BuyingCatalogue.Solutions.Application.Commands.UpdateSolutionPlugins;
using NHSD.BuyingCatalogue.Solutions.Contracts.Queries;

namespace NHSD.BuyingCatalogue.Solutions.API.Controllers
{
    [Route("api/v1/solutions")]
    [ApiController]
    [Produces("application/json")]
    [AllowAnonymous]
    public class PlugInsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initialises a new instance of the <see cref="PlugInsController"/> class.
        /// </summary>
        public PlugInsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the plug ins for the client application types of a solution matching the supplied ID.
        /// </summary>
        /// <param name="id">A value to uniquely identify a solution.</param>
        /// <returns>A task representing an operation to retrieve the details of the plug ins section.</returns>
        [HttpGet]
        [Route("{id}/sections/plug-ins-or-extensions")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetPlugInsAsync([FromRoute][Required]string id)
        {
            var solution = await _mediator.Send(new GetSolutionByIdQuery(id)).ConfigureAwait(false);
            return solution == null ? (ActionResult)new NotFoundResult() : Ok(new GetPlugInsResult(solution.ClientApplication.Plugins));
        }

        /// <summary>
        /// Updates the plug ins of a solution matching the supplied ID.
        /// </summary>
        /// <param name="id">A value to uniquely identify a solution.</param>
        /// <param name="updateSolutionPlugInsViewModel">The details of the plug ins.</param>
        /// <returns>A task representing an operation to update the details of the plug ins section.</returns>
        [HttpPut]
        [Route("{id}/sections/plug-ins-or-extensions")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> UpdatePlugInsAsync([FromRoute][Required]string id, [FromBody][Required]UpdateSolutionPluginsViewModel updateSolutionPlugInsViewModel)
        {
            var validationResult =
                await _mediator.Send(new UpdateSolutionPluginsCommand(id, updateSolutionPlugInsViewModel)).ConfigureAwait(false);

            return validationResult.IsValid
                ? (ActionResult)new NoContentResult()
                : BadRequest(new UpdateSolutionPluginsResult(validationResult));
        }
    }
}
