using M003_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace M003_MVC.Controllers;

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
	public IActionResult Index() => View();

	public IActionResult Einloggen() => View(new User());

	public IActionResult Registrieren() => View(new User());

	public IActionResult LoginButton(string Username, string Password)
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
