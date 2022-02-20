using System;

namespace Podcast.Core
{
    public class ShowDto
    {
        public Guid? ShowId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime Updated { get; set; }
        public string Link { get; set; }
        public string Email { get; set; }
        public string Language { get; set; }
        public Guid FeedId { get; set; }
        public Feed? Feed { get; set; }
        public ICollection<Episode> Episodes { get; set; }
    }
}
