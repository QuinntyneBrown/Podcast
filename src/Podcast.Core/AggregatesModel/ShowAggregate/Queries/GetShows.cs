using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging;
using Podcast.Core;
using Podcast.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Podcast.Core
{

    public class GetShowsRequest: IRequest<GetShowsResponse> { }
    public class GetShowsResponse: ResponseBase
    {
        public List<ShowDto> Shows { get; set; }
    }
    public class GetShowsHandler: IRequestHandler<GetShowsRequest, GetShowsResponse>
    {
        private readonly IPodcastDbContext _context;
        private readonly ILogger<GetShowsHandler> _logger;
    
        public GetShowsHandler(IPodcastDbContext context, ILogger<GetShowsHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<GetShowsResponse> Handle(GetShowsRequest request, CancellationToken cancellationToken)
        {
            return new () {
                Shows = await _context.Shows.AsNoTracking().ToDtosAsync(cancellationToken)
            };
        }
        
    }

}
