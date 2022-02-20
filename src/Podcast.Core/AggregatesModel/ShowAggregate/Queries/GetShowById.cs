using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Podcast.Core;
using Podcast.Core.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Podcast.Core
{

    public class GetShowByIdRequest: IRequest<GetShowByIdResponse>
    {
        public Guid ShowId { get; set; }
    }
    public class GetShowByIdResponse: ResponseBase
    {
        public ShowDto Show { get; set; }
    }
    public class GetShowByIdHandler: IRequestHandler<GetShowByIdRequest, GetShowByIdResponse>
    {
        private readonly IPodcastDbContext _context;
        private readonly ILogger<GetShowByIdHandler> _logger;
    
        public GetShowByIdHandler(IPodcastDbContext context, ILogger<GetShowByIdHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<GetShowByIdResponse> Handle(GetShowByIdRequest request, CancellationToken cancellationToken)
        {
            return new () {
                Show = (await _context.Shows.AsNoTracking().SingleOrDefaultAsync(x => x.ShowId == new ShowId(request.ShowId))).ToDto()
            };
        }
        
    }

}
