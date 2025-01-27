using M002_Einführung;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
//Startup.ConfigureServices(builder.Services);

//Dependency Injection
//Vor Erstellung der Application kann per DI die Applikation konfiguriert werden
//builder.Configuration: Allgemeine Konfigurationen
//builder.Environment: Umgebungsvariablen setzen
//builder.Services: Klassen registrieren, welche per Konstruktor in die Page hinzugefügt werden
builder.Environment.EnvironmentName = Environments.Development;
if (builder.Environment.IsDevelopment())
{
	//...

	//AddSingleton(): Erzeugt ein einziges Objekt für ALLE User
	//AddTransient(): Erzeugt ein Objekt pro User
	//AddScoped(): Erzeugt ein Objekt pro HTTP Request

	//Beispiel Logger: AddTransient, User kann mitgeloggt werden bei Logeinträgen
	//Beispiel DbContext (EF): AddTransient, pro User eine DB Verbindung
	builder.Services.AddSingleton<DITest>();
}

////////////////////////////////////////////////////////////////////////////////

//Middleware
//Konfiguriert die HTTP-Request Pipeline -> Wie ein Request abgehandelt wird
//Bei einem Request wird die Pipeline in der Reihenfolge abgearbeitet, in welcher die Use-Methoden angelegt wurden
//WICHTIG: Reihenfolge beachten (z.B.: UseStaticFiles() vor UseMvc(), UseAuth vor UseMvc())
WebApplication app = builder.Build();
//Startup.ConfigureMiddleware(app, app.Environment);

app.MapGet("/", () => "Hello World!");
app.MapGet("/key", (string x) => $"Du hast {x} eingegeben"); //HTTP Parameter verarbeiten (Parametername x)
app.MapGet("/key2", Key); //Methodenzeiger auf eine dedizierte Methode (nicht anonym)

//Delegate
//Methodenzeiger, welcher eine fixe Struktur hat
//Die Struktur wird von dem Delegatetypen selbst vorgegeben
//MapGet verwendet den Typen "Delegate", d.h. die Struktur kann beliebig sein
//In Zeile 33 ist die Struktur anonym (Lambda-Expression mit Pfeil)
//Ein Delegate kann immer als separate Methode definiert werden

app.Run();

string Key(string x)
{
	return $"Du hast {x} eingegeben";
}