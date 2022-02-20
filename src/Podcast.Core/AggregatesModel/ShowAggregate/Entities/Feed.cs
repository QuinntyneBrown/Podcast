using Microsoft.EntityFrameworkCore;

namespace Podcast.Core
{
    [Owned]
    public class Feed
    {
        public string Url { get; private set; }
        public bool IsFeatured { get; private set; }
        public ICollection<FeedCategory> Categories { get; private set; } = new List<FeedCategory>();
    }
}
