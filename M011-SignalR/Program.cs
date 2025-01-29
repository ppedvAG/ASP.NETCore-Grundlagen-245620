using M011_SignalR;

var builder = WebApplication.CreateBuilder(args);

//SignalR
//Bi-direktionale Kommunikation zw. Client und Server
//Basiert auf WebSockets
//Verbindungsaufbau:
//- Client sendet einen Request an den Server
//- Server antwortet mit dem Öffnen eines WebSockets
//- Verbindung bleibt geöffnet, Server kann jetzt selbst Requests senden
//SignalR ist die .NET Implementation von WebSockets
//Wird in Blazor als Basis von aller Kommunikation verwendet


//Um SignalR benutzen zu können, wird C# und JS Code benötigt
//builder.Services.AddSignalR();

//Hub
//Funktioniert wie ein Controller, aber im SignalR Kontext
//app.MapHub<Typ>("route");


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

var app = builder.Build();

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

app.MapHub<ChatHub>("/chat");

app.Run();
