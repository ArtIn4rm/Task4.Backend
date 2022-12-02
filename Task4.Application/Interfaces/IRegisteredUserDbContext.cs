using Microsoft.EntityFrameworkCore;
using Task4.Domain;

namespace Task4.Application.Interfaces
{
    public interface IRegisteredUserDbContext
    {
        DbSet<RegisteredUser> RegisteredUsers { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
