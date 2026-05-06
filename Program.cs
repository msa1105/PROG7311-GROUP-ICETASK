using FinanceTrack.Data;
using FinanceTrack.Interfaces;
using FinanceTrack.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Use /app/data when running in Docker, otherwise fall back to the local app folder
var isDocker = Directory.Exists("/app/data");
var dbFolder = isDocker ? "/app/data" : builder.Environment.ContentRootPath;
var dbPath = Path.Combine(dbFolder, "Finance.db");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// Scoped because DbContext is scoped
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

var app = builder.Build();

// Ensure Database is Created and Seeded on every startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DbInitializer.Initialize(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Skip HTTPS redirection in Docker (HTTP only on port 8080)
if (!isDocker)
{
    app.UseHttpsRedirection();
}

// IMPORTANT: UseStaticFiles MUST come before UseRouting so that
// CSS/JS files are served correctly in both Visual Studio (IIS Express)
// and dotnet run (Kestrel) on any machine.
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); // required for attribute-routed API controllers

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();


