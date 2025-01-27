namespace M002_Einführung;

public class Startup
{
	public static void ConfigureServices(IServiceCollection services)
	{
		services.AddSingleton<DITest>();
	}

	public static void ConfigureMiddleware(WebApplication app, IWebHostEnvironment env)
	{
		app.MapGet("/", () => "Hello World!");
		app.MapGet("/key", (string x) => $"Du hast {x} eingegeben"); //HTTP Parameter verarbeiten (Parametername x)
		app.MapGet("/key2", Key); //Methodenzeiger auf eine dedizierte Methode (nicht anonym)
	}

	public static string Key(string x)
	{
		return $"Du hast {x} eingegeben";
	}
}
