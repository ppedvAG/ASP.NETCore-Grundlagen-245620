using M000_DataAccess.Models;
using M009_LayoutDesign.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace M009_LayoutDesign.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	private readonly NorthwindContext _db;

	public HomeController(ILogger<HomeController> logger, NorthwindContext db)
	{
		_logger = logger;
		_db = db;
	}

	public IActionResult Index()
	{
		return View();
	}

	public IActionResult Privacy()
	{
		return View();
	}

	[HttpGet]
	public async Task<IActionResult> ShowAllCustomers()
	{
		List<Customer> customers = await _db.Customers.ToListAsync();

		//ViewBag: Beliebige Daten an die View weitergeben
		int max = 10;
		ViewBag.Max = max;

		return View(customers);
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
