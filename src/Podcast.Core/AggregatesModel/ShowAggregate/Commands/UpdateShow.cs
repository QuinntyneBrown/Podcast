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

    public class UpdateShowValidator: AbstractValidator<UpdateShowRequest>
    {
        public UpdateShowValidator()
        {
            RuleFor(request => request.Show).NotNull();
            RuleFor(request => request.Show).SetValidator(new ShowValidator());
        }
    
    }
    public class UpdateShowRequest: IRequest<UpdateShowResponse>
    {
        public ShowDto Show { get; set; }
    }
    public class UpdateShowResponse: ResponseBase
    {
        public ShowDto Show { get; set; }
    }
    public class UpdateShowHandler: IRequestHandler<UpdateShowRequest, UpdateShowResponse>
    {
        private readonly IPodcastDbContext _context;
        private readonly ILogger<UpdateShowHandler> _logger;
    
        public UpdateShowHandler(IPodcastDbContext context, ILogger<UpdateShowHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<UpdateShowResponse> Handle(UpdateShowRequest request, CancellationToken cancellationToken)
        {
            var show = await _context.Shows.SingleAsync(x => x.ShowId == new ShowId(request.Show.ShowId.Value));
            
            show.Title = request.Show.Title;
            show.Author = request.Show.Author;
            show.Description = request.Show.Description;
            show.Image = request.Show.Image;
            show.Updated = request.Show.Updated;
            show.Link = request.Show.Link;
            show.Email = request.Show.Email;
            show.Language = request.Show.Language;
            show.FeedId = request.Show.FeedId;
            show.Feed = request.Show.Feed;
            show.Episodes = request.Show.Episodes;
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return new ()
            {
                Show = show.ToDto()
            };
        }
        
    }

}
