using M000_DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace M007_EFListen.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	private readonly NorthwindContext _db;

	public HomeController(ILogger<HomeController> logger, NorthwindContext db)
	{
		_logger = logger;
		_db = db;
	}

	public IActionResult Index() => View();

	public IActionResult Privacy() => View();

	[HttpGet]
	public async Task<IActionResult> ShowAllCustomers()
	{
		List<Customer> customers = await _db.Customers.ToListAsync();
		return View(customers);
	}

	[HttpPost]
	public async Task<IActionResult> FilterByCountry(string country)
	{
		IQueryable<Customer> c;
		if (string.IsNullOrEmpty(country))
			c = _db.Customers;
		else
			c = _db.Customers.Where(e => e.Country.ToLower() == country.ToLower());

		return View("ShowAllCustomers", c);
	}

	[HttpPost]
	public async Task<IActionResult> BestellungenAnzeigen(string id)
	{
		IQueryable<Order> orders = _db.Orders.Where(e => e.CustomerId == id);
		return View(orders);
	}
}
