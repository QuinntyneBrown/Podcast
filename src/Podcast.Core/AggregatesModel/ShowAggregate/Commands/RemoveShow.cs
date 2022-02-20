using FluentValidation;
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

    public class RemoveShowRequest: IRequest<RemoveShowResponse>
    {
        public Guid ShowId { get; set; }
    }
    public class RemoveShowResponse: ResponseBase
    {
        public ShowDto Show { get; set; }
    }
    public class RemoveShowHandler: IRequestHandler<RemoveShowRequest, RemoveShowResponse>
    {
        private readonly IPodcastDbContext _context;
        private readonly ILogger<RemoveShowHandler> _logger;
    
        public RemoveShowHandler(IPodcastDbContext context, ILogger<RemoveShowHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<RemoveShowResponse> Handle(RemoveShowRequest request, CancellationToken cancellationToken)
        {
            var show = await _context.Shows.FindAsync(new ShowId(request.ShowId));
            
            _context.Shows.Remove(show);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return new ()
            {
                Show = show.ToDto()
            };
        }
        
    }

}
