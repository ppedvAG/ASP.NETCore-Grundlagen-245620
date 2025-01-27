using M003_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M003_MVC.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	public HomeController(ILogger<HomeController> logger) => _logger = logger;

	/// <summary>
	/// Jede Methode in einem Controller leitet auf eine Page weiter
	/// Der Name der Methode gibt den Namen der Page vor
	/// 
	/// IActionResult: Ergebnis eines Requests
	/// Korrespondiert immer mit einem HTTP StatusCode (z.B. 200, 400, 403, 404, ...)
	/// </summary>
	public IActionResult Index()
	{
		_logger.Log(LogLevel.Information, "Hallo Welt");

		//View(): Leitet den User auf die View weiter
		//Jede Methode in einem Controller hat eine dazugehörige View
		//Diese werden in Views/<Controllername> definiert
		//Diese Views enthalten HTML + C# Code
		return View(); //Hier wird weitergeleitet auf Views/Home/Index.cshtml
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
