using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M001.Pages;

public class ErgebnisModel : PageModel
{
	public int Ergebnis;

	public void OnGet(int ergebnis)
	{
		Ergebnis = ergebnis;
	}
}