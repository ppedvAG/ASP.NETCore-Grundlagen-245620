using M000_DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace M010_WebAPI.Controllers;

[ApiController] //Definiert, dass dieser Controller eine API darstellt
[Route("api/northwind")] //Basisroute definieren
[Produces("application/json")] //Outputformat der Daten (https://www.iana.org/assignments/media-types/media-types.xhtml)
public class NorthwindController : Controller
{
	private readonly ILogger<NorthwindController> _logger;

	private readonly NorthwindContext _db;

	public NorthwindController(ILogger<NorthwindController> logger, NorthwindContext db)
	{
		_logger = logger;
		_db = db;
	}

	[HttpGet("customer/all")]
	public IEnumerable<Customer> ShowAllCustomers()
	{
		return _db.Customers;
	}

	//Aufgabe: Endpunkt, welcher ein Land nimmt
	[HttpGet("customer/{country}")]
	public IEnumerable<Customer> ShowCustomersFromCountry(string country)
	{
		return _db.Customers.Where(e => e.Country == country);
	}

	//Aufgabe: Neuen Customer per Post speichern
	[HttpPost("customer/save")]
	public IActionResult SaveCustomer(Customer c)
	{
		try
		{
			_db.Add(c);
			_db.SaveChanges();
		}
		catch (DbUpdateException)
		{
			return BadRequest();
		}
		return Ok();
	}
}
