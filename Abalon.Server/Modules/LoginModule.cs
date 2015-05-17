using Abalon.Server.Services;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abalon.Server
{
	public class LoginModule : NancyModule
	{
        readonly ISiteController siteController;

		public LoginModule(ISiteController siteController)
		{
            this.siteController = siteController;
			Get["/login/{login}"] = p =>
			{
				var player = ConnectionRequest(p.login);
				if (player == null)
					return "Fail";
				siteController.AddConnectedPlayer(player);
				return player.UID;
			};
		}

		Random r = new Random();
		private string GenerateUID()
		{
			string uid = "";
			do
			{
				uid = r.Next().ToString();
			}
			while (siteController.ConnectedPlayers.Any(p => p.UID == uid));
			return uid;
		}

		public Player ConnectionRequest(string name)
		{
            if (siteController.ConnectedPlayers.Any(p => p.Name == name))
				return null;
			return new Player() { Name = name, UID = GenerateUID() };
		}

		public void LogoutRequest(string uid)
		{
            Player requesting = siteController.ConnectedPlayers.FirstOrDefault(p => p.UID == uid);
			if (requesting != null)
                siteController.RemoveDisconnectedPlayer(requesting);
		}
	}
}