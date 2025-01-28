using M006_EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace M006_EntityFramework.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	private readonly NorthwindContext _db;

	public HomeController(ILogger<HomeController> logger, NorthwindContext db)
	{
		_logger = logger;
		_db = db;
	}

	/// <summary>
	/// Die Northwind Datenbank
	/// Demo Datenbank von MS aus dem Jahr 2002
	/// Enthält Daten zu einem Unternehmen, welches Produkte quer durch die Welt versendet
	/// 
	/// Pakete:
	/// - Microsoft.EntityFrameworkCore
	/// - Microsoft.EntityFrameworkCore.Design
	/// - Microsoft.EntityFrameworkCore.SqlServer
	/// - Microsoft.EntityFrameworkCore.Tools
	/// 
	/// Extension:
	/// - EF Core Power Tools
	/// 
	/// Rechtsklick aufs Projekt -> EF Core Power Tools -> Reverse Engineer
	/// Verbindungsdaten angeben -> Tabellen auswählen -> Weitere Einstellungen (Models & Context erzeugen)
	/// 
	/// Context Klasse: Bietet Zugriff auf die Datenbank über die DbSets
	/// DbSets müssen per Linq angegriffen werden, und geben uns eine Liste von Objekten
	/// Jedes Objekt repräsentiert einen Datensatz in der Datenbank
	/// Hier muss jetzt von der Context Klasse ein Objekt existeren, um die Datenbankzugriffe auszuführen
	/// -> Dependency Injection, pro User ein Context (Transient)
	/// </summary>
	public async Task<IActionResult> Index()
	{
		//IEnumerable
		//Wenn eine Methode ein Objekt vom Typ IEnumerable zurückgibt, ist dies nur eine Anleitung zum Erstellen der fertigen Daten
		DbSet<Customer> c = _db.Customers; //Nur eine Anleitung

		//Hier wurden noch keine Daten geladen
		//Die Daten werden erst bei Verwendung geladen (ToList(), ToArray(), foreach, ...)
		IQueryable<Customer> filtered = c.Where(e => e.Country == "Germany"); //Nur eine Anleitung

		//Actions in MVC können auch async sein
		List<Customer> customers = await filtered.ToListAsync();

		//Auch hier wurden keine Daten geladen
		return View(customers); //Hier werden die Daten geladen
	}

	public IActionResult Privacy()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
