using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M004_RazorPages.Pages;

public class IndexModel : PageModel
{
	private readonly ILogger<IndexModel> _logger;

	public IndexModel(ILogger<IndexModel> logger) => _logger = logger;

	/// <summary>
	/// OnGet
	/// �quivalent zu Index()
	/// Diese Methode wird bei einem GET-Request ausgef�hrt
	/// </summary>
	public void OnGet()
	{
		//return View(); //Hier wird automatisch impliziert, dass wir die Page besuchen wollen
	}
}
