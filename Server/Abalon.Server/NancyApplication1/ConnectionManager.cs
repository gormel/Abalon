using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyApplication1
{
	public class LoginManager
	{
		MainClass main;
		public LoginManager(MainClass main)
		{
			this.main = main;
		}

		Random r = new Random();
		private string GenerateUID()
		{
			string uid = "";
			do
			{
				uid = r.Next().ToString();
			}
			while(main.ConnectedPlayers.Any(p => p.UID == uid));
			return uid;
		}

		public Player ConnectionRequest(string name)
		{
			if (main.ConnectedPlayers.Any(p => p.Name == name))
				return null;
			return new Player() { Name = name, UID = GenerateUID() };
		}

		public void LogoutRequest(string uid)
		{
			Player requesting = main.ConnectedPlayers.FirstOrDefault(p => p.UID == uid);
			if (requesting != null)
				main.ConnectedPlayers.Remove(requesting);
		}
	}
}