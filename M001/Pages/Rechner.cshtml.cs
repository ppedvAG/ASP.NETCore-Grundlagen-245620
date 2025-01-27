using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M001.Pages;

public class RechnerModel : PageModel
{
	public int AnzahlZahlen { get; set; } = 2;

	public void OnGet() { }

	public void OnPost(string anzahl)
	{
		if (int.TryParse(anzahl, out int anz))
		{
			AnzahlZahlen = anz;
		}
	}

	public IActionResult OnPostBerechnen(string[] zahlen, Rechenoperation op)
	{
		if (op == Rechenoperation.Keine)
			return Page();

		List<int> parsed = [];
		foreach (string z in zahlen)
		{
			if (int.TryParse(z, out int x))
				parsed.Add(x);
			else
				return BadRequest();
		}

		int ergebnis = parsed[0];
		foreach (int z in parsed.Skip(1))
		{
			switch (op)
			{
				case Rechenoperation.Addition:
					ergebnis += z;
					break;
				case Rechenoperation.Subtraktion:
					ergebnis -= z;
					break;
				case Rechenoperation.Multiplikation:
					ergebnis *= z;
					break;
				case Rechenoperation.Division:
					if (z == 0)
						return BadRequest();
					ergebnis /= z;
					break;
			}
		}

		return RedirectToPage("Ergebnis", routeValues: new { ergebnis });
	}
}