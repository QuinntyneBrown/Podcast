using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Podcast.Core
{
    public static class ShowExtensions
    {
        public static ShowDto ToDto(this Show show)
        {
            return new ()
            {
                ShowId = show.ShowId.Value,
                Title = show.Title,
                Author = show.Author,
                Description = show.Description,
                Image = show.Image,
                Updated = show.Updated,
                Link = show.Link,
                Email = show.Email,
                Language = show.Language,
                FeedId = show.FeedId,
                Feed = show.Feed,
                Episodes = show.Episodes,
            };
        }
        
        public static async Task<List<ShowDto>> ToDtosAsync(this IQueryable<Show> shows, CancellationToken cancellationToken)
        {
            return await shows.Select(x => x.ToDto()).ToListAsync(cancellationToken);
        }
        
        public static List<ShowDto> ToDtos(this IEnumerable<Show> shows)
        {
            return shows.Select(x => x.ToDto()).ToList();
        }
        
    }
}
