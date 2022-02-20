using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Podcast.Core;
using Podcast.Core.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Podcast.Core
{

    public class CreateShowValidator: AbstractValidator<CreateShowRequest>
    {
        public CreateShowValidator()
        {
            RuleFor(request => request.Show).NotNull();
            RuleFor(request => request.Show).SetValidator(new ShowValidator());
        }
    
    }
    public class CreateShowRequest: IRequest<CreateShowResponse>
    {
        public ShowDto Show { get; set; }
    }
    public class CreateShowResponse: ResponseBase
    {
        public ShowDto Show { get; set; }
    }
    public class CreateShowHandler: IRequestHandler<CreateShowRequest, CreateShowResponse>
    {
        private readonly IPodcastDbContext _context;
        private readonly ILogger<CreateShowHandler> _logger;
    
        public CreateShowHandler(IPodcastDbContext context, ILogger<CreateShowHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    
        public async Task<CreateShowResponse> Handle(CreateShowRequest request, CancellationToken cancellationToken)
        {
            var show = new Show();
            
            _context.Shows.Add(show);
            
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
