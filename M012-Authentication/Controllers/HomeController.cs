using M012_Authentication.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace M012_Authentication.Controllers;

public class HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> um, SignInManager<IdentityUser> sim, RoleManager<IdentityRole> rm, ApplicationDbContext db) : Controller
{
	public async Task<IActionResult> Index()
	{
		IdentityUser? admin = db.Users.FirstOrDefault(e => e.UserName == "Admin");
		if (admin != null)
			await um.DeleteAsync(admin);
		//await sim.SignInAsync(admin, false);

		return View();
	}

	/// <summary>
	/// Privacy darf nur betreten werden, wenn der User die Admin Rolle hat
	/// </summary>
	
	//[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Privacy()
	{
		//Über HttpContext den derzeitigen User herausfinden
        if (!HttpContext.User.IsInRole("Admin"))
        {
			return Forbid();
        }
        return View();
	}

	/// <summary>
	/// Hier wird die Rolle "Admin" erstellt + ein User mit der Rolle
	/// </summary>
	public async Task<IActionResult> CreateAdmin()
	{
		IdentityRole? role = await rm.FindByNameAsync("Admin");

		if (role == null)
		{
			IdentityRole admin = new IdentityRole("Admin");
			await rm.CreateAsync(admin);
		}

		////////////////////////////////////////////////////////////////////////////////

		IdentityUser user = new IdentityUser() { UserName = "Admin", Email = "admin@test.com" };
		IdentityResult result = await um.CreateAsync(user);
		IdentityResult pw = await um.AddPasswordAsync(user, "Admin123");

		if (!result.Succeeded)
			return BadRequest();

		if (!pw.Succeeded)
			return BadRequest();

		IdentityResult roleResult = await um.AddToRoleAsync(user, "Admin");
		if (roleResult.Succeeded)
		{
			//Admin User anmelden
			await sim.SignInAsync(user, false); //Benötigt User + Remember Login Boolean (hier false)
		}

		return View();
	}
}
