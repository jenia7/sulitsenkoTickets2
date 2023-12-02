using Microsoft.EntityFrameworkCore;
using RemoteApi.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<MainDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("LocalDb");
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<IRepository, EFRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5001";
        options.TokenValidationParameters = new()
        {
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AuthUser", options =>
    {
        options.RequireAuthenticatedUser();
        options.RequireClaim("scope", "remote_api");
    });
    options.AddPolicy("Admin", options =>
    {
        options.RequireRole("Admin");
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using var scope = app.Services.CreateScope();
    SeedDatabase.EnsureDbContainsData(scope.ServiceProvider);
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization("AuthUser");

app.Run();
