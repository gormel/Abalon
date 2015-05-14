using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyApplication1
{
	internal enum Players
	{
		Black,
		White
	}

	public class MainClass : NancyModule
	{
		private Players currentMove = Players.Black;

		public List<Player> ConnectedPlayers { get; set; }
		private LoginManager connectionManager;

		public MainClass()
		{
			ConnectedPlayers = new List<Player>();
			connectionManager = new LoginManager(this);
			Get["/"] = p => "hello world";
			Get["/login/{login}"] = p =>
			{
				var player = connectionManager.ConnectionRequest(p.login);
				if (player == null)
					return "Fail";
				ConnectedPlayers.Add(player);
				return player.UID;
			};

			Get["/list"] = p =>
				{
					if (ConnectedPlayers.Count < 1)
						return "";
					return ConnectedPlayers.Select(pl => pl.Name).Aggregate((a, b) => a + Environment.NewLine + b);
				};
		}
	}
}