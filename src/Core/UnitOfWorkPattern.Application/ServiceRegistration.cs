using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace UnitOfWorkPattern.Application;

public static class ServiceRegistration
{
    public static void AddApplication(this IServiceCollection service)
    {
        service.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}
