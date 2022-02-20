using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Podcast.Core;
using MediatR;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Podcast.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ShowController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ShowController> _logger;

        public ShowController(IMediator mediator, ILogger<ShowController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [SwaggerOperation(
            Summary = "Get Show by id.",
            Description = @"Get Show by id."
        )]
        [HttpGet("{showId:guid}", Name = "getShowById")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetShowByIdResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetShowByIdResponse>> GetById([FromRoute]Guid showId, CancellationToken cancellationToken)
        {
            var request = new GetShowByIdRequest() { ShowId = showId };
        
            var response = await _mediator.Send(request, cancellationToken);
        
            if (response.Show == null)
            {
                return new NotFoundObjectResult(request.ShowId);
            }
        
            return response;
        }
        
        [SwaggerOperation(
            Summary = "Get Shows.",
            Description = @"Get Shows."
        )]
        [HttpGet(Name = "getShows")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetShowsResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetShowsResponse>> Get(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetShowsRequest(), cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Create Show.",
            Description = @"Create Show."
        )]
        [HttpPost(Name = "createShow")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateShowResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateShowResponse>> Create([FromBody]CreateShowRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName}: ({@Command})",
                nameof(CreateShowRequest),
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Get Show Page.",
            Description = @"Get Show Page."
        )]
        [HttpGet("page/{pageSize}/{index}", Name = "getShowsPage")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetShowsPageResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetShowsPageResponse>> Page([FromRoute]int pageSize, [FromRoute]int index, CancellationToken cancellationToken)
        {
            var request = new GetShowsPageRequest { Index = index, PageSize = pageSize };
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Update Show.",
            Description = @"Update Show."
        )]
        [HttpPut(Name = "updateShow")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpdateShowResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpdateShowResponse>> Update([FromBody]UpdateShowRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                nameof(UpdateShowRequest),
                nameof(request.Show.ShowId),
                request.Show.ShowId,
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
        [SwaggerOperation(
            Summary = "Delete Show.",
            Description = @"Delete Show."
        )]
        [HttpDelete("{showId:guid}", Name = "removeShow")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(RemoveShowResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RemoveShowResponse>> Remove([FromRoute]Guid showId, CancellationToken cancellationToken)
        {
            var request = new RemoveShowRequest() { ShowId = showId };
        
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                nameof(RemoveShowRequest),
                nameof(request.ShowId),
                request.ShowId,
                request);
        
            return await _mediator.Send(request, cancellationToken);
        }
        
    }
}
