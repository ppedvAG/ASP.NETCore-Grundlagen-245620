using M005_WeitereThemen.Models;
using Microsoft.AspNetCore.Mvc;

namespace M005_WeitereThemen.Controllers;

/// <summary>
/// Simples Loginportal
/// 
/// User muss sich registrieren (Username, Passwort)
/// Nach Registrierung kann er sich anmelden (mit Username, Passwort)
/// 
/// Technische Umsetzung:
/// - Liste von Usern per DI hinzufügen
/// - HTML für Login/Registrieren schreiben (HTML Form)
/// - C# Login für Einloggen/Registrierung
/// 
/// 1. User Klasse im Models Ordner erstellen
/// 2. Liste von Users per DI registrieren
/// </summary>
public class LoginController : Controller
{
	private List<User> _users;

	public LoginController(List<User> users) => _users = users;

	/// <summary>
	/// Rechtsklick auf Methodenname -> Add View
	/// </summary>
	public IActionResult Index()
	{
		User u = new User();
		if (HttpContext.Request.Cookies["EingeloggtBleiben"] != null)
		{
			u = _users.First(e => e.Username == HttpContext.Request.Cookies["LoginToken"].Split(',')[0]);
		}

		return View(u);
	}

	public IActionResult Einloggen() => View(new User());

	public IActionResult Registrieren() => View(new User());

	public IActionResult LoginButton(string Username, string Password, string eingeloggtBleiben)
	{
		if (!_users.Any(e => e.Username == Username))
		{
			//Gibt es den User noch nicht?
			return NotFound();
		}

		User foundUser = _users.First(e => e.Username == Username);
		if (foundUser.Password != Password)
		{
			//Ist das Passwort falsch?
			return Forbid();
		}

		//Wenn Username und Passwort korrekt sind, leite den User auf die Startview zurück
		if (eingeloggtBleiben == "on")
		{
			//In der Realität muss hier noch ein zusätzlicher Token gespeichert werden, welcher die Logininfos enthält
			//Dieser Token wird aus Username + Passwort erzeugt, und kann für den Login verwendet werden
			HttpContext.Response.Cookies.Append("EingeloggtBleiben", "true");
			HttpContext.Response.Cookies.Append("LoginToken", $"{Username},{Password}");
		}

		return View("Index", foundUser);
	}

	public IActionResult RegistrierenButton(string Username, string Password)
	{
		if (!_users.Any(e => e.Username == Username))
		{
			//Gibt es den User noch nicht?
			_users.Add(new User() { Username = Username, Password = Password });
			return View("Index");
		}

		return BadRequest();
	}
}
