using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
namespace IdentityServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        string migrationsAssembly = typeof(Program).Assembly.GetName().Name;
        string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        string usersDbConnectionString = builder.Configuration.GetConnectionString("UsersConnection");
        builder.Services.AddRazorPages();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(usersDbConnectionString));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 3;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = true;
        });
        builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddAspNetIdentity<ApplicationUser>()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            });

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            using var scope = app.Services.CreateScope();
            InitializeDatabase(scope.ServiceProvider);
            SeedData.EnsureDbPopulatedWithUsers(scope.ServiceProvider);
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();

        app.MapControllers();
        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }

    private static void InitializeDatabase(IServiceProvider sp)
    {
        using var grantDb = sp.GetRequiredService<PersistedGrantDbContext>();
        using var confDb = sp.GetRequiredService<ConfigurationDbContext>();

        if (grantDb.Database.GetPendingMigrations().Any())
        {
            grantDb.Database.Migrate();
        }

        if (confDb.Database.GetPendingMigrations().Any())
        {
            confDb.Database.Migrate();
        }

        if (!confDb.Clients.Any())
        {
            foreach (var client in Config.Clients)
            {
                confDb.Clients.Add(client.ToEntity());
            }
            confDb.SaveChanges();
        }

        if (!confDb.IdentityResources.Any())
        {
            foreach (var resource in Config.IdentityResources)
            {
                confDb.IdentityResources.Add(resource.ToEntity());
            }
            confDb.SaveChanges();
        }

        if (!confDb.ApiScopes.Any())
        {
            foreach (var resource in Config.ApiScopes)
            {
                confDb.ApiScopes.Add(resource.ToEntity());
            }
            confDb.SaveChanges();
        }
    }
}
