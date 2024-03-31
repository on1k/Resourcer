using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Resourcer.Data.DB;
using Resourcer.Data.DB.Models;

namespace Resourcer.Extentions;

public static class ServiceCollectionExtensions
{
    public static void AddDb<TContext>(this IServiceCollection services) where TContext : DbContext
    {
        services.AddDbContext<TContext>(o =>
        {
            o.UseInMemoryDatabase("DataDb", x => 
                x.EnableNullChecks());

        });
    }

    public static async Task PopulateData(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();
        var db = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
        await db.Resources.AddAsync(new Resource { Decision = "Grant", Name = "res1" });
        await db.Resources.AddAsync(new Resource { Decision = "Grant", Name = "res2" });
        await db.Resources.AddAsync(new Resource { Decision = "Deny", Name = "res3" });
        await db.Resources.AddAsync(new Resource { Decision = "Deny", Name = "res4" });
        await db.SaveChangesAsync();
    }

    public static void AddPolly(this IServiceCollection services)
    {
        services.AddResiliencePipeline("my", builder =>
        {
            builder.AddTimeout(TimeSpan.FromSeconds(3));
        });
    }
}