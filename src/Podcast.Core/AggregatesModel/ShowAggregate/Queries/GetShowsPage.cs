using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Podcast.Core;
using Podcast.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Podcast.Core
{

    public class GetShowsPageRequest: IRequest<GetShowsPageResponse>
    {
        public int PageSize { get; set; }
        public int Index { get; set; }
    }
    public class GetShowsPageResponse: ResponseBase
    {
        public int Length { get; set; }
        public List<ShowDto> Entities { get; set; }
    }
    public class GetShowsPageHandler: IRequestHandler<GetShowsPageRequest, GetShowsPageResponse>
    {
        private readonly IPodcastDbContext _context;
        private readonly ILogger<GetShowsPageHandler> _logger;
    
        public GetShowsPageHandler(IPodcastDbContext context, ILogger<GetShowsPageHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<GetShowsPageResponse> Handle(GetShowsPageRequest request, CancellationToken cancellationToken)
        {
            var query = from show in _context.Shows
                select show;
            
            var length = await _context.Shows.AsNoTracking().CountAsync();
            
            var shows = await query.Page(request.Index, request.PageSize).AsNoTracking()
                .Select(x => x.ToDto()).ToListAsync();
            
            return new ()
            {
                Length = length,
                Entities = shows
            };
        }
        
    }

}
