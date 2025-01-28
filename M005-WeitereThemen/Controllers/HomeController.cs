using M005_WeitereThemen.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M005_WeitereThemen.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	public HomeController(ILogger<HomeController> logger)
	{
		_logger = logger;
	}

	public IActionResult Index()
	{
		//HttpContext
		//Bietet Informationen zu der Verbindung des Clients
		//u.a. Request, Response, User, Connection

		//Cookies
		//Können bei Request ausgelesen und bei Response geschrieben werden
		HttpContext.Response.Cookies.Append("TestCookie", "123", new CookieOptions() { Expires = DateTime.Now + TimeSpan.FromSeconds(10) });

		if (HttpContext.Request.Cookies["TestCookie"] != null)
		{
			//...
		}

		return View();
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

	/// <summary>
	/// Für einen File Upload wird das IFormFile Interface benötigt
	/// </summary>
	public IActionResult FileUpload(IFormFile f)
	{
		using Stream s = f.OpenReadStream();
		using FileStream fs = new FileStream("Test.txt", FileMode.Create);
		s.CopyTo(fs);
		fs.Flush();
		fs.Close();
		return View("Index");
	}
}