using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004_RazorPages.Pages.Login;

public class EinloggenModel : PageModel
{
    private List<User> _users;

    public EinloggenModel(List<User> users) => _users = users;

    public void OnGet() { }

    /// <summary>
    /// IActionResult kann auch in Razor Pages verwendet werden
    /// </summary>
    public IActionResult OnPostLoginButton(string Username, string Password)
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

        //Es können bei einem Redirect nicht einfach Objekte mitgegeben werden
        //Hier müssen anonyme Objekte verwendet werden
        return RedirectToPage("Login", foundUser);
        //return RedirectToPage("Login", new { foundUser.Username, foundUser.Password });
    }
}