using GameService.Application.Interfaces.Repositories;
using GameService.Application.Interfaces.UnitOfWorks;
using GameService.Infrastructure.Context;
using GameService.Infrastructure.Interceptors;
using GameService.Infrastructure.Repositories;
using GameService.Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameService.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");

            services.AddScoped<AuditingInterceptor>();
            services.AddDbContext<GameServiceDbContext>((sp, options) => options
                .UseSqlServer(connectionString)
                .AddInterceptors(sp.GetRequiredService<AuditingInterceptor>()));

            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
