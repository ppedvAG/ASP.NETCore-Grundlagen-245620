using M012_Authentication.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//Verbindung zur StandardDB herstellen, welche die Benutzer enth�lt
//Kann auch in eine bestehende Datenbank gelegt werden
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
	throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//IdentityUser: Beschreibt, wie die User aussehen
//Diese Klasse kann vererbt werden, um eigene User zu definieren (eigene Felder)
//Die IdentityUser Klasse hat eine Id, diese kann einen beliebigen Typen annehmen (standardm��ig string)
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
	//Hier k�nnen Optionen f�r Username, Passwort, Email, ... gesetzt werden
	options.SignIn.RequireConfirmedAccount = true;
	options.Password.RequiredLength = 1;
})
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>(); //AddEntityFrameworkStores(): Zeigt an, wo die Tabellen mit den Usern abgelegt werden

//Mithilfe von Configure k�nnen im nachhinein noch weitere Optionen gesetzt werden
builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequiredLength = 6;
	options.Password.RequireLowercase = true;
	options.Password.RequireUppercase = true;
	options.Password.RequireDigit = true;

	options.User.RequireUniqueEmail = true;
	options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
});

//User verwenden (in den Pages einbinden)
//Klassen: HttpContext, SignInManager, UserManager, RoleManager

//SignInManager
//Erm�glicht allgemeine Userverwaltung (Login, Logout, 2FA, ...)

//UserManager
//Gibt Informationen �ber den User

//RoleManager
//Erm�glicht das Verwaltung von Rollen pro User (welche Rollen, was k�nnen die Rollen, ...)
//WICHTIG: Bei DI muss .AddRoles<IdentityRole>() verwendet

//Diese Klassen werden per DI hinzugef�gt (automatisch)

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseStatusCodePages();
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
