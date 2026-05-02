using FinanceTrack.Data;
using FinanceTrack.Interfaces;
using FinanceTrack.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Fix for "lost connection on another PC":
// Build an absolute path for the SQLite DB using the app's ContentRootPath.
// This ensures the DB file is always found regardless of which directory
// the app is launched from (Visual Studio, dotnet run, another PC, etc.)
var dbPath = Path.Combine(builder.Environment.ContentRootPath, "Finance.db");
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
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// IMPORTANT: UseStaticFiles MUST come before UseRouting so that
// CSS/JS files are served correctly in both Visual Studio (IIS Express)
// and dotnet run (Kestrel) on any machine.
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
