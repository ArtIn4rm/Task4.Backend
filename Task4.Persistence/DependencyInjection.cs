using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Task4.Application.Interfaces;

namespace Task4.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, 
            IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<RegisteredUserDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<IRegisteredUserDbContext>(provider =>
                provider.GetService<RegisteredUserDbContext>()!);
            return services;
        }
    }
}
