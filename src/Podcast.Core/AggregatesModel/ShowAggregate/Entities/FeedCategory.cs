namespace Podcast.Core
{
    
    public class FeedCategory
    {
        public Guid FeedId { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category? Category { get; private set; }

        public FeedCategory(Guid categoryId, Guid feedId)
        {
            CategoryId = categoryId;
            FeedId = feedId;
        }
    }
}