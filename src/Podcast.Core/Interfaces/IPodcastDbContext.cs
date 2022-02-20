using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace Podcast.Core.Interfaces
{
    public interface IPodcastDbContext
    {
        DbSet<Show> Shows { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
    }
}
