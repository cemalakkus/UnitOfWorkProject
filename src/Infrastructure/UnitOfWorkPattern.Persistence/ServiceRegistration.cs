using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnitOfWorkPattern.Application.Interfaces.Repositories;
using UnitOfWorkPattern.Persistence.Context;
using UnitOfWorkPattern.Persistence.Repositories;
using UnitOfWorkPattern.Application.Interfaces.UnitOfWork;
using UnitOfWorkPattern.Persistence.Repositories;

namespace UnitOfWorkPattern.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistence(this IServiceCollection service, IConfiguration configuration)
        {
            if (Convert.ToBoolean(configuration.GetSection("UsePostgreSqlDatabase").Value))
                service.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSql"),
                                                         b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
            else
                service.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MsSql"),
                                                         b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            service.AddTransient<IUnitOfWork, UnitOfWork>();
            service.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            service.AddTransient<IProductRepository, ProductRepository>();
        }
    }
}
