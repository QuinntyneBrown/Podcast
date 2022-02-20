using Podcast.Core;
using Podcast.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Podcast.Infrastructure.Data
{
    public class PodcastDbContext: DbContext, IPodcastDbContext
    {
        public DbSet<Show> Shows { get; private set; }
        public PodcastDbContext(DbContextOptions options)
            :base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PodcastDbContext).Assembly);
        }
        
    }
}
