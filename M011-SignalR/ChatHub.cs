using Microsoft.AspNetCore.SignalR;

namespace M011_SignalR;

/// <summary>
/// In dem Hub und im Client muss jetzt Code zum senden/empfangen von Nachrichten implementiert werden
/// </summary>
public class ChatHub : Hub
{
	//Wird von JS (Client) aufgerufen, um eine Nachricht an den Server zu senden
	public async Task NachrichtSenden(string user, string msg)
	{
		//SendAsync: Sendet einen Befehl an alle Clients, eine JS Methode auszuführen
		//Die Methode hier heißt "NachrichtEmpfangen", und komsumiert zwei Parameter: den Absender der Chatnachricht, die Chatnachricht selbst
		await Clients.All.SendAsync("NachrichtEmpfangen", user, msg);
	}
}