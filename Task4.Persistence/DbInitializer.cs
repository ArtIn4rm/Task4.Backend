using Microsoft.EntityFrameworkCore;

namespace Task4.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(DbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
