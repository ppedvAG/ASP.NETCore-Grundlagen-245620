using M006_EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//In der appsettings.json Datei sollte der ConnectionString abgelegt werden
//Dieser ConnectionString hat einen Schlüssel und den Value als den string selbst
//Jetzt kann die Methode OnConfiguring im Context entfernt werden
string connString = builder.Configuration.GetConnectionString("Northwind"); //string aus appsettings.json auslesen
builder.Services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(connString)); //DI über AddDbContext
//builder.Services.AddTransient<NorthwindContext>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
