using Task4.Domain;
using Task4.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task4.Persistence.EntityTypeConfigurations;

namespace Task4.Persistence
{
    public class RegisteredUserDbContext : DbContext, IRegisteredUserDbContext
    {
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
        
        public RegisteredUserDbContext(DbContextOptions<RegisteredUserDbContext> options) 
            : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RegisteredUserConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
