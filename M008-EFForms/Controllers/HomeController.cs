using M000_DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace M008_EFForms.Controllers;

public class HomeController : Controller
{
	//[BindProperty(SupportsGet = true)]
	//public string Test { get; set; }

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

	public async Task<IActionResult> BestellungenAnzeigen(string id)
	{
		IQueryable<Order> orders = _db.Orders.Where(e => e.CustomerId == id);
		return View(orders);
	}

	public async Task<IActionResult> KundeLoeschen(string id)
	{
		Customer? toDelete = _db.Customers.FirstOrDefault(e => e.CustomerId == id);
		if (toDelete != null)
		{
			_db.Customers.Remove(toDelete);
		}
		await _db.SaveChangesAsync();
		return View("ShowAllCustomers");
	}

	public IActionResult KundeBearbeiten(string id)
	{
		return View(_db.Customers.FirstOrDefault(e => e.CustomerId == id));
	}

	public IActionResult Speichern(Customer customer)
	{
		//Customer old = _db.Customers.FirstOrDefault(e => e.CustomerId == customer.CustomerId);

		//Prüft, ob die Validierung stattgefunden hat
		//z.B. Wenn der User das JS deaktiviert, wird im Frontend nicht validiert
		if (!ModelState.IsValid)
		{
			return BadRequest();
		}

		_db.Customers.Update(customer);
		_db.SaveChanges();
		return View("ShowAllCustomers");
	}
}
