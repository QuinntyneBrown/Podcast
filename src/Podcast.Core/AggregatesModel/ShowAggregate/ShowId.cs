using StronglyTypedIds;

namespace Podcast.Core
{
    [StronglyTypedId(backingType: StronglyTypedIdBackingType.Guid, converters: StronglyTypedIdConverter.EfCoreValueConverter)]
    public partial struct ShowId { }
}
