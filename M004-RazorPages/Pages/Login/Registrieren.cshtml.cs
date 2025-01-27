using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004_RazorPages.Pages.Login;

public class RegistrierenModel : PageModel
{
    private List<User> _users;

    public RegistrierenModel(List<User> users) => _users = users;

    public void OnGet() { }

    public IActionResult OnPostRegistrierenButton(string Username, string Password)
    {
        if (!_users.Any(e => e.Username == Username))
        {
            //Gibt es den User noch nicht?
            _users.Add(new User() { Username = Username, Password = Password });
            return RedirectToPage("Login");
        }

        return BadRequest();
    }
}
