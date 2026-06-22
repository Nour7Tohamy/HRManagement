using EmployeeDepartment.API.Middleware;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Application;
using Infrastructure;
var builder = WebApplication.CreateBuilder(args);

// ── Services ─────────────────────────────────────────────
builder.Services.AddControllers();

// Application Layer
builder.Services.AddApplication();

// Infrastructure Layer (DbContext + Repos)
IServiceCollection serviceCollection = builder.Services.AddInfrastructure(builder.Configuration);

// Swagger
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Employee Department API",
        Version = "v1"
    });
});

var app = builder.Build();

// ── Safe Migration (ONLY DEV) ────────────────────────────
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"DB Migration Error: {ex.Message}");
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}

// ── Middleware ───────────────────────────────────────────
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();