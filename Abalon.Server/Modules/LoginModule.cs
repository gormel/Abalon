using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abalon.Server
{
	public class LoginModule : NancyModule
	{
		public LoginModule()
		{
			Get["/login/{login}"] = p =>
			{
				var player = ConnectionRequest(p.login);
				if (player == null)
					return "Fail";
				SiteController.Instance.Value.ConnectedPlayers.Add(player);
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
			while (SiteController.Instance.Value.ConnectedPlayers.Any(p => p.UID == uid));
			return uid;
		}

		public Player ConnectionRequest(string name)
		{
			if (SiteController.Instance.Value.ConnectedPlayers.Any(p => p.Name == name))
				return null;
			return new Player() { Name = name, UID = GenerateUID() };
		}

		public void LogoutRequest(string uid)
		{
			Player requesting = SiteController.Instance.Value.ConnectedPlayers.FirstOrDefault(p => p.UID == uid);
			if (requesting != null)
				SiteController.Instance.Value.ConnectedPlayers.Remove(requesting);
		}
	}
}