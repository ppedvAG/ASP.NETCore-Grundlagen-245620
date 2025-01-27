using Microsoft.AspNetCore.Mvc;

namespace M002_Einführung;

public class HomeController : Controller
{
	private DITest _test;

	/// <summary>
	/// Wenn der User die Page aufruft, bekommen wir hier im C# Code Zugriff auf das in Program.cs registrierte Objekt
	/// Hier wird per Parameter das Objekt verfügbar
	/// Es müssen nicht alle Objekt, welche registriert sind, hier als Parameter angelegt werden
	/// </summary>
    public HomeController(DITest test)
    {
        _test = test;
    }

    public IActionResult Index()
	{
		return View();
	}
}
