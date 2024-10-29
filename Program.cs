using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Load FedEx settings from appsettings.json.
builder.Services.Configure<FedExSettings>(builder.Configuration.GetSection("FedExApi"));

// Register the DbContext with Dependency Injection.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register HttpClient and FedEx services.
builder.Services.AddHttpClient<FedExAuthService>();
builder.Services.AddHttpClient<FedExShippingService>();

var app = builder.Build();

//  Database Seeding - Commented out for now.
/*
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        // Retrieve the AppDbContext instance.
        var context = services.GetRequiredService<AppDbContext>();

        // Call the DbInitializer to seed the database.
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        // Retrieve the logger instance.
        var logger = services.GetRequiredService<ILogger<Program>>();

        // Log the exception.
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}
*/

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Configure default route.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();