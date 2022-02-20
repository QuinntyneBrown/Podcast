using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Podcast.Core;

namespace Podcast.Infrastructure.EntityConfigurations
{
    public class ShowConfiguration : IEntityTypeConfiguration<Show>
    {
        public void Configure(EntityTypeBuilder<Show> builder)
        {
            builder.Property(e => e.ShowId).HasConversion(new ShowId.EfCoreValueConverter());
        }
    }
}
