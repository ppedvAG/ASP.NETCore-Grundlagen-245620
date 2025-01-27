using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004_RazorPages.Pages.Login;

public class LoginModel : PageModel
{
    public User? FoundUser;

    private List<User> _users;

    public LoginModel(List<User> users) => _users = users;

    public void OnGet(string Username, string Password)
    {
        FoundUser = _users.FirstOrDefault(e => e.Username == Username);
    }
}
